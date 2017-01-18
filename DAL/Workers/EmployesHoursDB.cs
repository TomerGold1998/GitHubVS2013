using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using DAL.Workers;
using System.Data;
using DBAcsses;

namespace DAL.Workers
{
   public class EmployesHoursDB : GeneralTable
    {
        public EmployesHoursDB()
           : base("EmployesHours", "ID")
        {
            WorkerDB Wdb = new WorkerDB();
            DataColumn d1 = Wdb.GetPrimaryKeyColumn();
            DataColumn d2 = this.GetColumn("worker");
            DBConnector.GetInstance().AddRelation(EmployesHours.relationWorker, d1, d2);                  
        }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new EmployesHours GetCurrentRowData()
        {
            return new EmployesHours(base.GetCurrentRowData());
        }

        public void UpdateEmployesHours(EmployesHours EH)
        {
            base.UpdateRow(EH);
        }

        public void AddEmployesHours(EmployesHours EH)
        {
            base.AddRow(EH);
        }

        public double CaculateEmployeeSalary(DateTime from, Worker w)
        {
            List<EmployesHours> TheWorkerHours = new List<EmployesHours>();
            foreach(var hours in GetEmployeesHours())
            {
                if(hours.DateOfWork >= from && hours.worker.ID == w.ID)
                {
                    TheWorkerHours.Add(hours);
                }
            }
            int WorkingHours = 0;
            int WorkingDays = 0;

            foreach(var eHour in TheWorkerHours)
            {
                WorkingHours += eHour.CaucalateHours();
                WorkingDays += 1;
            }

            WorkerDB db = new WorkerDB();
            DataRow dr = db.FindRow(w);
            switch(w.type)
            {
                case EmployeesType.Actor :
                    return new Actor(dr).CalcSalary(WorkingHours, WorkingDays);
                case EmployeesType.Director:
                    return new Director(dr).CalcSalary(WorkingHours, WorkingDays);
                case EmployeesType.Adminstration:
                    return new Adminstration(dr).CalcSalary(WorkingHours, WorkingDays);
                case EmployeesType.Salesman:
                    return w.CalcSalary(WorkingHours, WorkingDays);
                default: return 0;
            }       
            
        }

        private List<EmployesHours> GetEmployeesHours()
        {
            try
            {
                this.GoToFirst();
                EmployesHours Current = new EmployesHours();
                List<EmployesHours> list = new List<EmployesHours>();

                for (int i = 0; i < this.LengthOfTable; i++)
                {
                    Current = this.GetCurrentRowData();
                    if (Current != null)
                    {
                        list.Add(Current);
                        this.MoveNext();
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return new List<EmployesHours>();
            }
        }
    }
}
