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

        public List<Worker> GetAllWorkers()
        {
            try
            {
                this.GoToFirst();
                Worker Current = new Worker();
                List<Worker> list = new List<Worker>();

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
                return new List<Worker>();
            }
        }
        
    }
}
