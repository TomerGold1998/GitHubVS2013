using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Adminstration :  Worker
    {
        public Adminstration(string ID, string FirstName, string LastName, DateTime DateOfBirth, string Adress, string wCity 
            , double hourlySalry, int DirectorLvl, int DirectorEx) 
            :base(ID,FirstName, LastName,  DateOfBirth,  Adress,  wCity, hourlySalry, DirectorLvl, DirectorEx, EmployeesType.Director) 
        { }
        public Adminstration(System.Data.DataRow dataRow) 
            :base(dataRow)
        {
      
        }

        public override double CalcSalary(int workingHours, int WorkingDays)
        {
            if (this.employeeLevel >= 7 && WorkingDays >= 20)
            {
                return (1.5 * this.hourSalary) * workingHours;
            }
            else
            {
                return base.CalcSalary(workingHours, WorkingDays);
            }
        }
    }
}
