using Microsoft.AspNetCore.Mvc;

namespace AvalphaTechnologies.CommissionCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommisionController : ControllerBase
    {
        private const decimal AvalphaLocalRate = 0.20m;
        private const decimal AvalphaForeignRate = 0.35m;

        private const decimal CompetitorLocalRate = 0.02m;
        private const decimal CompetitorForeignRate = 0.0755m;

        [ProducesResponseType(typeof(CommissionCalculationResponse), 200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public IActionResult Calculate(
            [FromBody] CommissionCalculationRequest calculationRequest)
        {
            if (!IsValid(calculationRequest, out var error))
            {
                return BadRequest(error);
            }

            var avalphaCommission =
                (AvalphaLocalRate * calculationRequest.LocalSalesCount +
                 AvalphaForeignRate * calculationRequest.ForeignSalesCount)
                * calculationRequest.AverageSaleAmount;

            var competitorCommission =
                (CompetitorLocalRate * calculationRequest.LocalSalesCount +
                 CompetitorForeignRate * calculationRequest.ForeignSalesCount)
                * calculationRequest.AverageSaleAmount;

            var response = new CommissionCalculationResponse
            {
                AvalphaTechnologiesCommissionAmount = avalphaCommission,
                CompetitorCommissionAmount = competitorCommission
            };

            return Ok(response);
        }

        private static bool IsValid(
            CommissionCalculationRequest request,
            out string error)
        {
            error = string.Empty;

            if (request.LocalSalesCount < 0 ||
                request.ForeignSalesCount < 0)
            {
                error = "Sales counts must be greater than or equal to zero.";
                return false;
            }

            if (request.AverageSaleAmount < 0)
            {
                error = "Average sale amount must be greater than or equal to zero.";
                return false;
            }

            if (request.LocalSalesCount > 1_000_000 ||
                request.ForeignSalesCount > 1_000_000)
            {
                error = "Sales count exceeds allowed limit.";
                return false;
            }

            return true;
        }
    }

    public class CommissionCalculationRequest
    {
        public int LocalSalesCount { get; set; }
        public int ForeignSalesCount { get; set; }
        public decimal AverageSaleAmount { get; set; }
    }

    public class CommissionCalculationResponse
    {
        public decimal AvalphaTechnologiesCommissionAmount { get; set; }
        public decimal CompetitorCommissionAmount { get; set; }
    }
}
