using Api.BusinessLogic.Commands;
using Api.BusinessLogic.Models.Response;
using Api.BusinessLogic.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PayCheckController : BenefitsCalculatorAPIControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPayCheckService _payCheckService;

        public PayCheckController(IMediator mediator, IPayCheckService payCheck)
        {
            _mediator = mediator;
            _payCheckService = payCheck;
        }

        /// <summary>
        /// Gets all Pay Checks for the given employee
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("~/api/employees/{employeeId:int}/paycheck")]
        public async Task<IActionResult> GetByEmployeeID([FromRoute] int employeeId)
        {
            var result = await _payCheckService.GetByEmployeeID(employeeId);

            return MyOk(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [SwaggerOperation(Summary = "Calculates an Employees Pay Check. This is an MVP and can only hold the first year of paychecks. After all have been created, will return 400 Bad Request ")]
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CalculatePayCheckCommandResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<CalculatePayCheckCommandResponse>), StatusCodes.Status400BadRequest)]
        [Route("CalculatePayroll")]
        public async Task<IActionResult> Calculate([FromBody] CalculatePayCheckCommand request)
        {
            return MyOk(await _mediator.Send(request));
        }
    }
}
