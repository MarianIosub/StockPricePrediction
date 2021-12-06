using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Cors;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using ServiceLayer;

namespace StockPricePrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StockController : ControllerBase
    {
        #region Property

        private readonly IStockService _stockService;

        #endregion

        #region Constructor

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        #endregion

        [HttpGet(nameof(GetStock))]
        public IActionResult GetStock([FromQuery] string stockSymbol)
        {
            var result = _stockService.GetStock(stockSymbol);
            if (result is not null)
            {
                return Ok(result);
            }

            return NoContent();
        }

        [HttpGet(nameof(GetAllStocks))]
        public IActionResult GetAllStocks()
        {
            var result = _stockService.GetAllStocks();
            if (result is not null)
            {
                return Ok(result);
            }

            return NoContent();
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
                client.BaseAddress = new Uri("https://localhost:5002/");
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // List all Names.
                var query = new Dictionary<string, string>
                {
                    ["name"] = name,
                    ["days"] = days.ToString(),
                };
                HttpResponseMessage response =
                    client.GetAsync(QueryHelpers.AddQueryString("StockController/StockData", query))
                        .Result;
                var content = response.Content.ReadAsStream();
                if (response.IsSuccessStatusCode)
                {
                    return Ok(response.Content.ReadAsStream());
                }

                return StatusCode(int.Parse(response.StatusCode.ToString()));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500);
            }
        }
    }
}