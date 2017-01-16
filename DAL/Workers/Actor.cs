using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Actor : Worker
    {
        public ActorLevel ActorLevel;
        private System.Data.DataRow dataRow;

        public Actor(string ID, string FirstName, string LastName, DateTime DateOfBirth, string Adress, string wCity 
            , double hourlySalry, int DirectorLvl, int DirectorEx, ActorLevel level) 
            :base(ID,FirstName, LastName,  DateOfBirth,  Adress,  wCity, hourlySalry, DirectorLvl, DirectorEx, EmployeesType.Actor) 
        {
            this.ActorLevel= level;
        }

        public Actor(System.Data.DataRow dataRow) 
            :base(dataRow)
        {
      
        }

        public override double CalcSalary(int workingHours, int WorkingDays)
        {
            if (this.employeeExperince >6 && WorkingDays > 8 && this.ActorLevel == DAL.ActorLevel.Lead)
            {
                return (1.3 * this.hourSalary) * workingHours;
            }
            else
            {
                return base.CalcSalary(workingHours, WorkingDays);
            }
        }
    }

    public enum ActorLevel
    {
        Lead , Secondary , starting , Extra
    }
}
