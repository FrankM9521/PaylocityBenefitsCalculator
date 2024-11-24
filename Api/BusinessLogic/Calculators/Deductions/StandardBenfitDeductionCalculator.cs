﻿using Api.BusinessLogic.Calculations.Interfaces;
using Api.BusinessLogic.Models;

namespace Api.BusinessLogic.Calculations.Deductions
{
    public class StandardBenfitDeductionCalculator : BaseDeductionCalculator, IDeduction
    {
        public StandardBenfitDeductionCalculator(ICalculationsLibrary library) : base(library) { }
        public async Task<PayStatement> CalculateDeduction(PayStatement payStatement)
        {
            var baseDeduction = _calculationsLibrary.GetBaseDeduction(payStatement);

            if (baseDeduction > payStatement.NetPay)
            {
                baseDeduction = payStatement.NetPay;
            }

            await payStatement.AddDeduction(DeductionTypes.BenefitsBase, baseDeduction);

            return payStatement;
        }
    }
}