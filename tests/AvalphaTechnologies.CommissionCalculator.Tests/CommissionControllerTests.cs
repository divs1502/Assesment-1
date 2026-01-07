using AvalphaTechnologies.CommissionCalculator.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AvalphaTechnologies.CommissionCalculator.Tests
{
    public class CommissionControllerTests
    {
        private readonly CommissionController _controller;

        public CommissionControllerTests()
        {
            _controller = new CommissionController();
        }

        [Fact]
        public void Calculate_WithValidRequest_ReturnsOkResult_WithCorrectCommission()
        {
            // Arrange
            var request = new CommissionCalculationRequest
            {
                LocalSalesCount = 10,
                ForeignSalesCount = 5,
                AverageSaleAmount = 100m
            };

            // Act
            var result = _controller.Calculate(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<CommissionCalculationResponse>(okResult.Value);

            // Expected calculations
            var expectedAvalpha =
                (0.20m * 10 + 0.35m * 5) * 100m;

            var expectedCompetitor =
                (0.02m * 10 + 0.0755m * 5) * 100m;

            Assert.Equal(expectedAvalpha, response.AvalphaTechnologiesCommissionAmount);
            Assert.Equal(expectedCompetitor, response.CompetitorCommissionAmount);
        }

        [Fact]
        public void Calculate_WithNegativeLocalSales_ReturnsBadRequest()
        {
            // Arrange
            var request = new CommissionCalculationRequest
            {
                LocalSalesCount = -1,
                ForeignSalesCount = 5,
                AverageSaleAmount = 100m
            };

            // Act
            var result = _controller.Calculate(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(
                "Sales counts must be greater than or equal to zero.",
                badRequest.Value);
        }

        [Fact]
        public void Calculate_WithNegativeForeignSales_ReturnsBadRequest()
        {
            // Arrange
            var request = new CommissionCalculationRequest
            {
                LocalSalesCount = 5,
                ForeignSalesCount = -2,
                AverageSaleAmount = 100m
            };

            // Act
            var result = _controller.Calculate(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(
                "Sales counts must be greater than or equal to zero.",
                badRequest.Value);
        }

        [Fact]
        public void Calculate_WithNegativeAverageSaleAmount_ReturnsBadRequest()
        {
            // Arrange
            var request = new CommissionCalculationRequest
            {
                LocalSalesCount = 5,
                ForeignSalesCount = 5,
                AverageSaleAmount = -10m
            };

            // Act
            var result = _controller.Calculate(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(
                "Average sale amount must be greater than or equal to zero.",
                badRequest.Value);
        }

        [Fact]
        public void Calculate_WithSalesCountExceedingLimit_ReturnsBadRequest()
        {
            // Arrange
            var request = new CommissionCalculationRequest
            {
                LocalSalesCount = 1_000_001,
                ForeignSalesCount = 10,
                AverageSaleAmount = 100m
            };

            // Act
            var result = _controller.Calculate(request);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(
                "Sales count exceeds allowed limit.",
                badRequest.Value);
        }
    }
}
