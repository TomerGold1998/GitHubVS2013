using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using System.Data;

namespace DAL.Shows
{
  public  class Show : IEntity
    {
      public static string PlayRelation = "ShowToPlayRelation";
      public static string AuditoriumRelation = "ShowToAuditoriumRelation"; 

        public string ID { get; set; }
        public Play Play { get; set; }
        public Auditorium PlayPlace { get; set; }
        public DateTime AtDate { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }  

         public Show(DataRow dataRow)
         {
             this.ID = dataRow["ID"].ToString();
             this.Play = new Play(dataRow.GetParentRow(PlayRelation));
             this.PlayPlace = new Auditorium(dataRow.GetParentRow(AuditoriumRelation));
             this.AtDate = DateTime.Parse(dataRow["AtDate"].ToString());
             this.FromTime = DateTime.Parse(dataRow["FromTime"].ToString());
             this.ToTime = DateTime.Parse(dataRow["ToTime"].ToString());
         }

         public Show()
         {
             // TODO: Complete member initialization
         }
        
      
        public void populate(System.Data.DataRow dataRow)
        {
            dataRow["ID"] = this.ID;
            dataRow["Play"] = this.Play.ID;
            dataRow["PlayPlace"] = this.PlayPlace.ID;
            dataRow["AtDate"] = this.AtDate.ToShortDateString();
            dataRow["FromTime"] = this.FromTime.ToShortTimeString();
            dataRow["ToTime"] = this.ToTime.ToShortTimeString();
        }


        public override string ToString()
        {
            return String.Format("ID: {0}, Name of play {1} at {2} from {3} to {4}, in auditruim {5}", this.ID, this.Play.p_Name, this.AtDate.ToShortDateString(), this.FromTime.ToShortTimeString(), this.ToTime.ToShortTimeString(), this.PlayPlace.a_Name);
        }
    }
}
