using Api.BusinessLogic.Models;
using Api.BusinessLogic.Models.CalculatePayroll;
using Api.Data.Entities;
using DeductionTypes = Api.Data.Entities.DeductionTypes;

namespace Api.BusinessLogic.Mappers
{
    public static class PayStatementMapper
    {
        public static PayStatementEntity ToEntity(this PayStatement value)
        {
            var payStatement = new PayStatementEntity
            {
                EmployeeID = value.Employee.Id,
                GrossPay = value.GrossPay,
                ID = value.ID,
                NetPay = value.NetPay,
                Order = value.Order,
            };

            foreach (var item in value.Deductions)
            {
                payStatement.AddDeduction((DeductionTypes)item.Key, item.Value);
            }

            return payStatement;
        }

        public static PayStatement ToModel(this PayStatementEntity value)
        {
            var payStatement = new PayStatement
            {
                GrossPay = value.GrossPay,
                ID = value.ID,
                Order = value.Order,
                NumberOfDependents = value.NumberOfDependents
            };

            foreach (var item in value.Deductions)
            {
                payStatement.AddDeduction((Models.DeductionTypes) item.Key, item.Value);
            }

            return payStatement;
        }

        public static CalculatePayrollStatement ToCalculatePayrollStatement(this PayStatementEntity value, int numberOfDependents)
        {
            var payStatement = new CalculatePayrollStatement(value.ID, value.Order, value.GrossPay, value.NetPay, numberOfDependents, value.Deductions.ToModel());

            return payStatement;
        }

        public static Dictionary<Models.DeductionTypes, decimal> ToModel(this Dictionary<DeductionTypes, decimal> value)
        {
            return value.ToDictionary(k => (Models.DeductionTypes)k.Key, v => v.Value);
        }
    }
}
