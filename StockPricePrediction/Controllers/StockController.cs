using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using ServiceLayer;
using ServiceLayer.Models;

namespace StockPricePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StockController : ControllerBase
    {
        #region Property
        private const int InternalServerError = 500;
        private readonly IStockService _stockService;
        private readonly IUserService _userService;
        private readonly Uri _machineLearningApi = new("https://localhost:5002/");

        #endregion

        #region Constructor

        public StockController(IStockService stockService, IUserService userService)
        {
            _stockService = stockService;
            _userService = userService;
        }

        #endregion

        [HttpGet(nameof(GetStock))]
        public IActionResult GetStock([FromQuery] string stockSymbol)
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var token = header.Parameter;
            var response = _userService.ValidateUser(token).Data;
            if (response is null)
            {
                return Unauthorized();
            }

            var id = (int) response;
            var user = _userService.GetUser(id).Data;

            var stock = _stockService.GetStock(stockSymbol).Data;
            if (stock is null)
            {
                return NoContent();
            }

            var status = _userService.GetFavouriteStocks(user).Data.Contains(stock);
            var userStockModel = new UserStockModel(stock, status);
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(userStockModel));
        }

        [HttpGet(nameof(GetAllStocks))]
        public IActionResult GetAllStocks()
        {
            var result = _stockService.GetAllStocks().Data;
            return result is not null ? Ok(result) : NoContent();
        }

        [HttpPost(nameof(InsertStock))]
        public IActionResult InsertStock(Stock stock)
        {
            _stockService.InsertStock(stock);
            return Ok("Stock inserted");
        }

        [HttpPut(nameof(UpdateStock))]
        public IActionResult UpdateStock(Stock stock)
        {
            _stockService.UpdateStock(stock);
            return Ok("Stock updated");
        }

        [HttpDelete(nameof(DeleteStock))]
        public IActionResult DeleteStock(int id)
        {
            _stockService.DeleteStock(id);
            return Ok("Stock Deleted");
        }

        [HttpGet(nameof(GetStockData))]
        public IActionResult GetStockData([FromQuery] string name, [FromQuery] int days)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = _machineLearningApi;
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // List all Names.
                var query = new Dictionary<string, string>
                {
                    ["name"] = name,
                    ["days"] = days.ToString(),
                };
                var response =
                    client.GetAsync(QueryHelpers.AddQueryString("StockController/StockData", query))
                        .Result;
                var content = response.Content.ReadAsStream();
                if (response.IsSuccessStatusCode)
                {
                    return Ok(content);
                }

                Console.WriteLine(response.StatusCode.ToString());
                return StatusCode(int.Parse(response.StatusCode.ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(InternalServerError);
            }
        }

        [HttpGet(nameof(GetStockPrediction))]
        public IActionResult GetStockPrediction([FromQuery] string name, [FromQuery] int days)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = _machineLearningApi;
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // List all Names.
                var query = new Dictionary<string, string>
                {
                    ["name"] = name,
                    ["days"] = days.ToString(),
                };
                var response =
                    client.GetAsync(QueryHelpers.AddQueryString("StockController/StockPrediction", query))
                        .Result;
                var content = response.Content.ReadAsStream();
                if (response.IsSuccessStatusCode)
                {
                    return Ok(content);
                }

                return StatusCode(int.Parse(response.StatusCode.ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(InternalServerError);
            }
        }
    }
}