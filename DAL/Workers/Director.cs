using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Director : Worker
    {
        public Director(string ID, string FirstName,  string LastName, DateTime DateOfBirth, string Adress, string wCity 
            , double hourlySalry, int DirectorLvl, int DirectorEx) 
            :base(ID,FirstName, LastName,  DateOfBirth,  Adress,  wCity, hourlySalry, DirectorLvl, DirectorEx, EmployeesType.Director) 
        { }

        public Director(System.Data.DataRow dataRow) 
            :base(dataRow)
        {
      
        }



        public override double CalcSalary(int workingHours, int WorkingDays)
        {
            if (this.employeeExperince >= 4 && WorkingDays > 10)
            {
                return (1.2 * this.hourSalary) * workingHours;
            }
            else
            {
                return base.CalcSalary(workingHours, WorkingDays);
            }
        }
    
    }
}
