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
        public DateTime From;
        public DateTime To;


        public EmployesHours(Worker w, DateTime  from, DateTime to)
        {
            this.ID = new Guid().ToString();
            this.worker = w;
            this.From = from;
            this.To = to;
        }
        public EmployesHours(Worker w, int Year , int Month , int Day, int FromHour ,int ToHour)
        {
            this.ID = new Guid().ToString();
            this.worker = w;
            this.From = new DateTime(Year, Month, Day, FromHour,0,0);
            this.To = new DateTime(Year, Month, Day, ToHour, 0, 0);

        }
        public EmployesHours(System.Data.DataRow dataRow)
        {
            this.ID = dataRow["ID"].ToString();
            this.worker = new Worker(dataRow.GetParentRow(relationWorker));
            this.From = DateTime.Parse(dataRow["From"].ToString());
            this.To = DateTime.Parse(dataRow["To"].ToString());
        }
        public void populate(System.Data.DataRow dataRow)
        {
            dataRow["ID"] = this.ID;
            dataRow["worker"] = this.worker.ID;
            dataRow["From"] = this.From.ToString("MM/dd/yy HH:mm:ss tt");
            dataRow["To"] = this.To.ToString("MM/dd/yy HH:mm:ss tt");
        }
        public int CaucalateHours()
        {
            return To.Hour - From.Hour;
        }
      
    
  }
}
