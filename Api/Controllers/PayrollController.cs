using Api.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PayrollController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PayrollController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Calculate(CalculatePayrollCommand request)
        {
            return Ok();
        }
    }
}
