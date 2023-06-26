using Microsoft.AspNetCore.Mvc;
using Stocks.Hexagone.UseCases.PurchaseOrder.Commands;
using Stocks.Hexagone.UseCases.PurchaseOrder.Queries;

namespace Stocks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly CreatePurchaseOrderCommandHandler _createPurchaseOrderCommandHandler;
        private readonly GetAllPurchaseOrdersHandler _getAllPurchaseOrdersHandler;

        public OrdersController(
            CreatePurchaseOrderCommandHandler createPurchaseOrderCommandHandler,
            GetAllPurchaseOrdersHandler getAllPurchaseOrdersHandler
        )
        {
            _createPurchaseOrderCommandHandler = createPurchaseOrderCommandHandler;
            _getAllPurchaseOrdersHandler = getAllPurchaseOrdersHandler;
        }

        [HttpGet]
        public IActionResult GetAllOrders()
        {
            var result = _getAllPurchaseOrdersHandler.Handle();
            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CreatePurchaseOrderCommand command)
        {
            var result = _createPurchaseOrderCommandHandler.Handle(command);
            if(result.IsError())
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }
    }
}