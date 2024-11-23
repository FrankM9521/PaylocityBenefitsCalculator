using Api.Commands;
using Api.Models;

namespace Api.Calculations
{
    /// <summary>
    ///  Creates the initial Pay Statement before any withholdings
    /// </summary>
    public class PayStatementFactory
    {
        public PayStatementFactory()
        { }

        public static PayStatement Create(Employee employee, CalculatePayrollCommand calculatePayrollCommand)
        {
            var grossPay = Math.Round(employee.Salary  /  Constants.PAY_PERIODS_PER_YEAR, 2);  
               
            return new PayStatement
            {
                Employee = employee,
                GrossPay = grossPay
            };
        }
    }
}
