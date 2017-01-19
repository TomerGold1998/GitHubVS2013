using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using System.Data;

namespace DAL.Shows
{
   public class PlayDB : GeneralTable
    {
       public PlayDB()
           : base("Play", "ID")
        {
            WorkerDB Wdb = new WorkerDB();
            DataColumn d1 = Wdb.GetPrimaryKeyColumn();
            DataColumn d2 = this.GetColumn("Director");
            DBConnector.GetInstance().AddRelation(Play.DirectorRelation, d1, d2);                  
        }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new Play GetCurrentRowData()
        {
            return new Play(base.GetCurrentRowData());
        }

        public void UpdatePlay(Play p)
        {
            base.UpdateRow(p);
        }

        public void AddPlay(Play p)
        {
            base.AddRow(p);
        }

        public List<Play> GetAllPlays()
        {
            try
            {
                this.GoToFirst();
                Play Current = new Play();
                List<Play> list = new List<Play>();

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
                return new List<Play>();
            }
        }
    }
}
