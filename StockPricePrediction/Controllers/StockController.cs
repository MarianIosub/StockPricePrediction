using Microsoft.AspNetCore.Mvc;  
using ServiceLayer;
using DomainLayer;

namespace StockPricePrediction.Controllers
{
    [Route("api/[controller]")]  
    [ApiController]  
    public class StockController: ControllerBase  
    {
        #region Property  
        private readonly IStockService _StockService;  
        #endregion  
        
        #region Constructor  
        public StockController(IStockService StockService)  
        {  
            _StockService = StockService;  
        }  
        #endregion  
  
        [HttpGet(nameof(GetStock))]  
        public IActionResult GetStock([FromQuery(Name = "id")] int id)  
        {  
            var result = _StockService.GetStock(id);  
            if(result is not null)  
            {  
                return Ok(result);  
            }  
            return BadRequest("No records found");  
              
        }  
        [HttpGet(nameof(GetAllStocks))]  
        public IActionResult GetAllStocks()  
        {  
            var result = _StockService.GetAllStocks();  
            if (result is not null)  
            {  
                return Ok(result);  
            }  
            return BadRequest("No records found");  
  
        }  
        [HttpPost(nameof(InsertStock))]  
        public IActionResult InsertStock(Stock Stock)  
        {  
            _StockService.InsertStock(Stock);  
            return Ok("Data inserted");  
  
        }  
        [HttpPut(nameof(UpdateStock))]  
        public IActionResult UpdateStock(Stock Stock)  
        {  
            _StockService.UpdateStock(Stock);  
            return Ok("Update done");  
  
        }  
        [HttpDelete(nameof(DeleteStock))]  
        public IActionResult DeleteStock(int Id)  
        {  
            _StockService.DeleteStock(Id);  
            return Ok("Data Deleted");  
  
        }  
    }
}