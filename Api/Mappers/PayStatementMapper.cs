using Api.Entities;
using Api.Models;

namespace Api.Mappers
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
                Order = value.Order
            };

            foreach (var item in value.Deductions)
            {
                payStatement.AddDeduction((Entities.DeductionTypes) item.Key, item.Value);
            }
            
            return payStatement;
        }
    }
}
