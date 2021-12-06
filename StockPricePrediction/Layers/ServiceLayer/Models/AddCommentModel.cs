using System;

namespace ServiceLayer.Models
{
    public class AddCommentModel
    {
        public string Message { get; set; }
        public string StockSymbol { get; set; }
        public DateTime CreationDate { get; set; }
    }
}