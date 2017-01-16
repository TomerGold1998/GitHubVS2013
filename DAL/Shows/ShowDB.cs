using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBAcsses;
using System.Data;

namespace DAL.Shows
{
   public class ShowDB : GeneralTable
    {
       public ShowDB()
           : base("Show", "ID")
        {
            PlayDB Pdb = new PlayDB();
            DataColumn d1 = Pdb.GetPrimaryKeyColumn();
            DataColumn d2 = this.GetColumn("Play");
            DBConnector.GetInstance().AddRelation(Show.PlayRelation, d1, d2);

            AuditoriumDB Adb = new AuditoriumDB();
            DataColumn d3 = Adb.GetPrimaryKeyColumn();
            DataColumn d4 = this.GetColumn("PlayPlace");
            DBConnector.GetInstance().AddRelation(Show.AuditoriumRelation, d3, d4);       
            
        }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new Show GetCurrentRowData()
        {
            return new Show(base.GetCurrentRowData());
        }

        public void UpdateShow(Show s)
        {
            base.UpdateRow(s);
        }

        public void AddShow(Show s)
        {
            base.AddRow(s);
        }
    }
}
