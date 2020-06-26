using BackendTest.Api.Controllers.RequestDtos;
using BackendTest.Command.Commands.ItemManagement;
using BackendTest.Query.ItemManagement.ItemDetails;
using BackendTest.Query.ItemManagement.ItemList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BackendTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var query = new GetItemListQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var query = new GetItemDetailsQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddItemRequestDto request)
        {
            var command = new AddItemCommand(request.Name);
            var result = await _mediator.Send(command);

            if (result.IsFailure)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }
    }
}
