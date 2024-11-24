using Api.BusinessLogic.Models;
using DeductionTypes = Api.Data.Entities.DeductionTypes;
using Entities = Api.Data.Entities;

namespace Api.BusinessLogic.Mappers
{
    public static class PayCheckMapper
    {
        public static Entities.PayCheckEntity ToEntity(this PayCheck value)
        {
            return new Entities.PayCheckEntity
            {
                EmployeeID = value.EmployeeID,
                GrossPay = value.GrossPay,
                ID = value.ID,
                NetPay = value.NetPay,
                Salary = value.Salary,
                Order = value.Order,
                NumberOfDependents = value.NumberOfDependents,
                Deductions =value.Deductions.Select(d => new { d.Key, d.Value }).ToDictionary(k => (DeductionTypes)k.Key, v => v.Value)
            };
        }

        public static PayCheck ToModel(this CalculatePayCheck value)
        {
            return new PayCheck
            {
                EmployeeID = value.Employee.Id,
                GrossPay = value.GrossPay,
                ID = value.ID,
                NetPay = value.NetPay,
                Salary = value.Salary,
                Order = value.Order,
                NumberOfDependents = value.Employee.Dependents.Count(),
                Deductions = value.Deductions.Select(d => new {  d.Key, d.Value}).ToDictionary(k => (Models.DeductionTypes)k.Key, v => v.Value)
            };
        }

        public static PayCheck ToModel(this Entities.PayCheckEntity value)
        {
            return new PayCheck()
            {
                EmployeeID = value.EmployeeID,
                GrossPay = value.GrossPay,
                ID = value.ID,
                NetPay = value.NetPay,
                Salary = value.Salary,  
                Order = value.Order,
                NumberOfDependents = value.NumberOfDependents,
                Deductions = value.Deductions.Select(d => new { d.Key, d.Value })
                    .ToDictionary(k => (Models.DeductionTypes)k.Key, v => v.Value)
            };
        }
    }
}
