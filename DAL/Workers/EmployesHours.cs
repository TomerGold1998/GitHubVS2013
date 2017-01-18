using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;

namespace DAL.Workers
{
  public class EmployesHours :  IEntity
    {

      public static string relationWorker = "EmployesHoursWorkerRelation";

        public string ID;
        public Worker worker;
        public DateTime DateOfWork;
        public DateTime FromHour;
        public DateTime ToHour;

        public EmployesHours(Worker w, DateTime dateOfWork, DateTime fromHour, DateTime toHour)
        {
            this.ID = Guid.NewGuid().ToString().Substring(0,7);
            this.worker = w;
            this.DateOfWork = dateOfWork;
            this.FromHour = fromHour;
            this.ToHour= toHour;
        }
     


        public EmployesHours(System.Data.DataRow dataRow)
        {
            this.ID = dataRow["ID"].ToString();
            this.worker = new Worker(dataRow.GetParentRow(relationWorker));
            this.DateOfWork = DateTime.Parse(dataRow["DateOfWork"].ToString());
            this.FromHour = DateTime.Parse(dataRow["FromHour"].ToString());
            this.ToHour = DateTime.Parse(dataRow["ToHour"].ToString());
        }

        public EmployesHours()
        {
            // TODO: Complete member initialization
        }
        public void populate(System.Data.DataRow dataRow)
        {
           

            dataRow["ID"] = this.ID;
            dataRow["worker"] = this.worker.ID;
            dataRow["DateOfWork"] = this.DateOfWork.ToShortDateString();
            dataRow["FromHour"] = this.FromHour.ToShortTimeString();
            dataRow["ToHour"] = this.ToHour.ToShortTimeString();
        }
        public int CaucalateHours()
        {
            return ToHour.Hour - FromHour.Hour;
        }
      
    
  }
}
