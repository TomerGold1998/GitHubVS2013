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
    }
}
