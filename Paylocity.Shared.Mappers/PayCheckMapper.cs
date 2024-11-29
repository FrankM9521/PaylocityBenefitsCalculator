using Paylocity.PayChecks.Entities;
using Paylocity.PayChecks.Models;
using DeductionTypes = Paylocity.PayChecks.Entities.DeductionTypes;

namespace Paylocity.Shared.Mappers.EntityDomain
{
    /*
 * Bolier Plate - This or AutoMapper
 */
    public static class PayCheckMapper
    {
        public static PayCheckEntity ToEntity(this PayCheck value)
        {
            return new PayCheckEntity
            {
                EmployeeID = value.EmployeeID,
                GrossPay = value.GrossPay,
                ID = value.ID,
                NetPay = value.NetPay,
                Salary = value.Salary,
                Order = value.Order,
                NumberOfDependents = value.NumberOfDependents,
                Deductions = value.Deductions.Select(d => new { d.Key, d.Value }).ToDictionary(k => (DeductionTypes)k.Key, v => v.Value)
            };
        }

        public static PayCheck ToModel(this CalculatePayCheck value)
        {
            return new PayCheck
            {
                EmployeeID = value.EmployeeID,
                GrossPay = value.GrossPay,
                ID = value.ID,
                NetPay = value.NetPay,
                Salary = value.Salary,
                Order = value.Order,
                NumberOfDependents = value.NumberOfDependents,
                Deductions = value.Deductions
            };
        }

        public static PayCheck ToModel(this PayCheckEntity value)
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
                Deductions = value.Deductions.ToList().ToDictionary(k => (PayChecks.Models.DeductionTypes) k.Key, v => v.Value)
            };
        }
    }
}
