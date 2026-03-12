using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDemo.Api.Contracts;
using MongoDemo.Application.Customers;
using MongoDemo.Application.Customers.Create;
using MongoDemo.Application.Customers.Delete;
using MongoDemo.Application.Customers.GetAll;
using MongoDemo.Application.Exceptions;
using MongoDemo.MongoDemo.Application.Customers.GetById;
using MongoDemo.MongoDemo.Application.Customers.Update;

namespace MongoDemo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomersController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<List<CustomerDto>>> GetAll(CancellationToken ct)
        => Ok(await _mediator.Send(new GetCustomersQuery(), ct));

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetById(string id, CancellationToken ct)
    {
        var customer = await _mediator.Send(new GetCustomerByIdQuery(id), ct);

        if (customer is null) return NotFound();
        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerCommand cmd, CancellationToken ct)
    {
        try
        {
            var created = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }
        catch (DuplicateEmailException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateCustomerRequest cmd, CancellationToken ct)
    {
        if (!ObjectId.TryParse(id, out _)) return BadRequest(new { message = "Invalid MongoDB ObjectId format." });

        try
        {
            var result = await _mediator.Send(new UpdateCustomerCommand(id,
            cmd.FirstName,
            cmd.LastName,
            cmd.Email), ct);

            if (!result) return NotFound();
            return NoContent();
        }
        catch (DuplicateEmailException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteById(string id, CancellationToken ct)
    {
        Console.WriteLine("Controller1");
        if (!ObjectId.TryParse(id, out _)) return BadRequest(new { message = "Invalid MongoDB ObjectId format." });
        Console.WriteLine("Controller2");
        var result = await _mediator.Send(new DeleteByIdCommand(id), ct);
        Console.WriteLine("Controller3");
        if (!result) return NotFound();
        Console.WriteLine("Controller4");
        return NoContent();
    }
}