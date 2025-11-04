using Application.Abstractions.Messaging;
using Application.Dtos.Payments;
using Application.UseCases.Payments.Commands.CreatePayment;
using Application.UseCases.Payments.Queries.GetPaymentsByCustomer;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IDispatcher dispatcher) : ControllerBase
    {
        private readonly IDispatcher _dispatcher = dispatcher;

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            var response = await _dispatcher.Dispatch<CreatePaymentCommand, bool>
                (command, CancellationToken.None);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaymentsByCustomer([FromQuery] Guid customerId)
        {
            var query = new GetPaymentsByCustomerQuery { CustomerId = customerId };
            var response = await _dispatcher.Dispatch<GetPaymentsByCustomerQuery, List<PaymentResponseDto>>(query, CancellationToken.None);
            return response.IsSuccess ? Ok(response.Data) : BadRequest(response);
        }
    }
}
