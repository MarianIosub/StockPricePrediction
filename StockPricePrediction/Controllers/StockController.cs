using System.Web.Http.Cors;
using DomainLayer;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetStock([FromQuery(Name = "id")] int id)
        {
            var result = _stockService.GetStock(id);
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest("No records found");
        }

        [HttpGet(nameof(GetAllStocks))]
        public IActionResult GetAllStocks()
        {
            var result = _stockService.GetAllStocks();
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest("No records found");
        }

        [HttpPost(nameof(InsertStock))]
        public IActionResult InsertStock(Stock stock)
        {
            _stockService.InsertStock(stock);
            return Ok("Data inserted");
        }

        [HttpPut(nameof(UpdateStock))]
        public IActionResult UpdateStock(Stock stock)
        {
            _stockService.UpdateStock(stock);
            return Ok("Update done");
        }

        [HttpDelete(nameof(DeleteStock))]
        public IActionResult DeleteStock(int id)
        {
            _stockService.DeleteStock(id);
            return Ok("Data Deleted");
        }
    }
}