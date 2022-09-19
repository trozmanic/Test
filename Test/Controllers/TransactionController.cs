using Microsoft.AspNetCore.Mvc;
using Test.Service;
using Test.Models;

namespace Test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : Controller
    {
        IBookService bookService;

        public TransactionController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet(Name = "TransferCoins")]
        public List<Order> TransferCoins([FromQuery] double amount, [FromQuery] Models.Type type)
        {
            return type switch
            {
                Models.Type.Buy => bookService.PurchaseCoins(amount),
                Models.Type.Sell => bookService.SellCoins(amount),
                _ => throw new Exception("Type not supported!"),
            };

        }
    }
}
