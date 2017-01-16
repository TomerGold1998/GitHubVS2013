using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using System.Data;

namespace DAL.Shows
{
    class ActorsInShowsDB : GeneralTable
    {
        public ActorsInShowsDB()
            : base("ActorsInShow", "ID")
        {
            WorkerDB Wdb = new WorkerDB();
            DataColumn d1 = Wdb.GetPrimaryKeyColumn();
            DataColumn d2 = this.GetColumn("Actor");
            DBConnector.GetInstance().AddRelation(ActorsInShow.relationActor, d1, d2);

            ShowDB Sdb = new ShowDB();
            DataColumn d3 = Sdb.GetPrimaryKeyColumn();
            DataColumn d4 = this.GetColumn("Show");
            DBConnector.GetInstance().AddRelation(ActorsInShow.relatioShow, d3, d4); 
        }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new ActorsInShow GetCurrentRowData()
        {
            return new ActorsInShow(base.GetCurrentRowData());
        }

        public void UpdateActorsInShow(ActorsInShow ain)
        {
            base.UpdateRow(ain);
        }

        public void AddActorsInShow(ActorsInShow ain)
        {
            base.AddRow(ain);
        }
    }
}
