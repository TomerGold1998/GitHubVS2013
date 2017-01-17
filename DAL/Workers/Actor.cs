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

        public Actor(string ID, string FirstName, string LastName, DateTime DateOfBirth, string Adress, string wCity 
            , double hourlySalry, int DirectorLvl, int DirectorEx, ActorLevel level) 
            :base(ID,FirstName, LastName,  DateOfBirth,  Adress,  wCity, hourlySalry, DirectorLvl, DirectorEx, EmployeesType.Actor) 
        {
            this.ActorLevel= level;
        }

        public Actor(System.Data.DataRow dataRow) 
            :base(dataRow)
        {
            this.ActorLevel = dataRow["ActorLevel"].ToString() == ActorLevel.Lead.ToString() ? ActorLevel.Lead : dataRow["ActorLevel"].ToString() == ActorLevel.Secondary.ToString() ? ActorLevel.Secondary : dataRow["ActorLevel"].ToString() == ActorLevel.starting.ToString() ? ActorLevel.starting : DAL.ActorLevel.Extra; 
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

        public override void populate(System.Data.DataRow dr)
        {
            dr["ActorLevel"] = this.ActorLevel.ToString();
            base.populate(dr);
        }
    }

    public enum ActorLevel
    {
        Lead , Secondary , starting , Extra
    }
}
