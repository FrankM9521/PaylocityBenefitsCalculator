using Api.BusinessLogic.Commands;
using Api.BusinessLogic.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PayrollController : BenefitsCalculatorAPIControllerBase
    {
        private readonly IMediator _mediator;

        public PayrollController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [SwaggerOperation(Summary = "Calculates an Employees Bi-Weekly Pay")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CalculatePayrollCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CalculatePayrollCommandResponse>), StatusCodes.Status404NotFound)]
        [Route("CalculatePayroll")]
        public async Task<IActionResult> Calculate(CalculatePayrollCommand request)
        {
            return MyOk(await _mediator.Send(request));
        }
    }
}
