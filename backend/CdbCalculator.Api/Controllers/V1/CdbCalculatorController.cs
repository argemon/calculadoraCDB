using Asp.Versioning;
using CdbCalculator.Application.Investments.Contracts;
using CdbCalculator.Application.Investments.Services;
using Microsoft.AspNetCore.Mvc;

namespace CdbCalculator.Api.Controllers.V1;

[ApiController]
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/cdb-calculator")]
[Produces("application/json")]
public sealed class CdbCalculatorController(ICdbInvestmentService investmentService) : ControllerBase
{
    [HttpPost("calculate")]
    [ProducesResponseType(typeof(CdbInvestmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public ActionResult<CdbInvestmentResponse> Calculate([FromBody] CalculateCdbInvestmentRequest request)
    {
        var response = investmentService.Calculate(request);
        return Ok(response);
    }
}
