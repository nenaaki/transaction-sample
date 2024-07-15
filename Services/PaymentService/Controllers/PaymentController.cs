using Microsoft.AspNetCore.Mvc;
using PaymentService.Commands;
using PaymentService.CommandHandlers;
using PaymentService.QueryHandlers;
using PaymentService.ReadModels;
using System;
using System.Threading.Tasks;

namespace PaymentService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private readonly ICommandHandler<CreatePaymentCommand> _createPaymentCommandHandler;
    private readonly IPaymentQueryHandler _paymentQueryHandler;

    public PaymentsController(ICommandHandler<CreatePaymentCommand> createPaymentCommandHandler,
                              IPaymentQueryHandler paymentQueryHandler)
    {
        _createPaymentCommandHandler = createPaymentCommandHandler;
        _paymentQueryHandler = paymentQueryHandler;
    }

    // POST: api/Payments
    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
    {
        if (command == null)
            return BadRequest("Invalid payment data.");

        await _createPaymentCommandHandler.HandleAsync(command);

        return CreatedAtAction(nameof(GetPayment), new { id = command.PaymentId }, null);
    }

    // GET: api/Payments/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPayment(Guid id)
    {
        var payment = await _paymentQueryHandler.GetPaymentAsync(id);

        if (payment == null)
            return NotFound();

        return Ok(payment);
    }
}
