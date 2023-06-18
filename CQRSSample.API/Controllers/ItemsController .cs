using CQRSSample.Features.Commands;
using CQRSSample.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSample.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var query = new GetItemsQuery();
            var items = await _mediator.Send(query);
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem(CreateItemCommand command)
        {
            var itemId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetItemById), new { id = itemId }, command);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var query = new GetItemByIdQuery { Id = id };
            var item = await _mediator.Send(query);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}