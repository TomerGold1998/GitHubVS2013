using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;

namespace DAL
{
    public class Worker : IEntity
   {
       #region Attr
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Adress { get; set; }
        public string WorkerCity { get; set; }


        protected double hourSalary;
        protected int employeeLevel;
        protected int employeeExperince;
        protected EmployeesType type;

       #endregion
       public Worker(string id, string firstName , string lastName , DateTime dateOfBirth, string Adress , string city , double hourlySalray, int empoyeeLvl , int employeeExp, EmployeesType Type)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Adress = Adress;
            this.WorkerCity = city;
            this.hourSalary = hourlySalray;
            this.employeeLevel = empoyeeLvl;
            this.employeeExperince = employeeExp;
            this.type = Type;
        }

      

       public virtual double CalcSalary(int workingHours, int WorkingDays)
        {
            return workingHours * this.hourSalary;
        }
       public override string ToString()
       {
           return String.Format("{0} - Name {1} {2}  ,Type:{3}", this.ID, this.FirstName, this.LastName, this.type.ToString());
       }
    

       public Worker(System.Data.DataRow dr)
       {
           this.ID = dr["ID"].ToString();
           this.FirstName = dr["FirstName"].ToString();
           this.LastName = dr["LastName"].ToString();
           this.DateOfBirth = DateTime.Parse(dr["DateOfBirth"].ToString());
           this.Adress = dr["Adress"].ToString();
           this.WorkerCity = dr["WorkerCity"].ToString();
           this.hourSalary = double.Parse(dr["hourSalary"].ToString());
           this.employeeLevel = int.Parse(dr["employeeLevel"].ToString());
           this.employeeExperince = int.Parse(dr["employeeExperince"].ToString());
           this.type = dr["type"].ToString() == EmployeesType.Actor.ToString() ? EmployeesType.Actor : dr["type"].ToString() == EmployeesType.Adminstration.ToString() ? EmployeesType.Adminstration : dr["type"].ToString() == EmployeesType.Director.ToString() ? EmployeesType.Director : EmployeesType.Salesman;

       }
       public void populate(System.Data.DataRow dr)
       {
           dr["ID"] = this.ID;
           dr["FirstName"] = this.FirstName;
           dr["LastName"] = this.LastName;
           dr["DateOfBirth"] = this.DateOfBirth.ToShortDateString();
           dr["Adress"] = this.Adress;
           dr["WorkerCity"] = this.WorkerCity;
           dr["hourSalary"] = this.hourSalary.ToString();
           dr["employeeLevel"] = this.employeeLevel.ToString();
           dr["employeeExperince"] = this.employeeExperince.ToString();
           dr["type"] = this.type.ToString();
       }
   }
    public enum EmployeesType
    {
       Salesman, Adminstration , Actor , Director 
    }
}
