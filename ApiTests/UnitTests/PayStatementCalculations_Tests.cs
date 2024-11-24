using Api.BusinessLogic.Calculations;
using Api.BusinessLogic.Calculations.Deductions;
using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Models;
using System.Threading.Tasks;
using Xunit;

namespace ApiTests.UnitTests
{
    public partial class PayStatementCalculations_Tests
    {
        [Theory]
        [InlineData(52000, 461.54, 1538.46)]
        [InlineData(12000, 461.54, 0.0)] // benefits = all pay
        [InlineData(6000, 230.77, 0.0)] // less than the amount, so it tales all
        public  async Task ItCalculatesStandardBenefitsDeductions(decimal salary, decimal expectedDeduction, decimal expectedNet)
        {
            // Arrange
            var sut = new StandardBenfitDeductionCalculator(CalcLibrary());

            // Act
            var result = await sut.CalculateDeduction(TestHelpers.TestPayStatement(salary));

            // Assert
            Assert.Equal(expectedDeduction, result.Deductions[Api.BusinessLogic.Models.DeductionTypes.BenefitsBase]);
            Assert.Equal(expectedNet, result.NetPay);
        }

        [Theory]
        [InlineData(52000.0, 0, 0.0, 2000.0)]
        [InlineData(52000, 1, 276.92, 1723.08)]
        [InlineData(52000, 2, 553.85, 1446.15)]
        [InlineData(52000, 3, 830.77, 1169.23)]
        [InlineData(52000, 10, 2000, 0)]
        public async Task ItCalculatesDependentDeductions(decimal salary, int numberOfDependents, decimal expectedDeduction, decimal expectedNet)
        {
            var sut = new DependentDeductionCalculator(CalcLibrary());

            var result = await sut.CalculateDeduction(TestHelpers.TestPayStatement(salary, numberOfDependents));

            Assert.Equal(expectedDeduction, result.Deductions[Api.BusinessLogic.Models.DeductionTypes.DependentBenefitsFee]);
            Assert.Equal(expectedNet, result.NetPay);
        }

        [Theory]
         [InlineData(52000, 0, 2000)]
        [InlineData(80000, 0, 3076.92)]
        [InlineData(120000, 92.31, 4523.07)]
        [InlineData(80000.01, 61.54, 3015.38)]
        public async Task ItCalculatesHighEarnerDeductions(decimal salary, decimal expectedDeduction, decimal expectedNet)
        {
            var sut = new CalculateHighEarnerDeductionCalculator(CalcLibrary());
            var result = await sut.CalculateDeduction(TestHelpers.TestPayStatement(salary));

            Assert.Equal(expectedDeduction, result.Deductions[Api.BusinessLogic.Models.DeductionTypes.HighEarnerBenefitsFee]);
            Assert.Equal(expectedNet, result.NetPay);
        }

        [Theory]
        [InlineData(52000, 50, 0, 2000)]
        [InlineData(52000, 51, 92.31, 1907.69)]
        [InlineData(52000, 100, 92.31, 1907.69)]
        [InlineData(2000, 100, 76.92, 0)]
        public async Task ItCalculatesSeniorDeductions(decimal salary, int age, decimal expectedDeduction, decimal expectedNet)
        {
            var sut = new SeniorBenefitsDeductionCalculator(CalcLibrary());
            var result = await sut.CalculateDeduction(TestHelpers.TestPayStatement(salary, 0, age));

            Assert.Equal(expectedDeduction, result.Deductions[Api.BusinessLogic.Models.DeductionTypes.SeniorBenefitsFee]);
            Assert.Equal(expectedNet, result.NetPay);
        }

        private ICalculationsLibrary CalcLibrary(bool statndard = true)
        {
            return new StandardCalculationLibrary();
        }
    }
}
