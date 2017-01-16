using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBAcsses;

namespace DAL
{
   public class WorkerDB :  GeneralTable
    {
        public WorkerDB() : base("Worker", "ID") { }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new Worker GetCurrentRowData()
        {
            return new Worker(base.GetCurrentRowData());
        }

        public void UpdateWorker(Worker w)
        {
            base.UpdateRow(w);
        }

        public void AddWorker(Worker w)
        {
            base.AddRow(w);
        }
    }
}
