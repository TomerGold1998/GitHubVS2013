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
        public DateTime FromDateAndHour { get; set; }
        public DateTime ToDateAndHour { get; set; }
        
         public Show(DataRow dataRow)
         {
             this.ID = dataRow["ID"].ToString();
             this.Play = new Play(dataRow.GetParentRow(PlayRelation));
             this.PlayPlace = new Auditorium(dataRow.GetParentRow(AuditoriumRelation));
             this.FromDateAndHour = DateTime.Parse(dataRow["FromDateAndHour"].ToString());
             this.ToDateAndHour = DateTime.Parse(dataRow["ToDateAndHour"].ToString());
         }
        
      
        public void populate(System.Data.DataRow dataRow)
        {
            dataRow["ID"] = this.ID;
            dataRow["Play"] = this.Play.ID;
            dataRow["PlayPlace"] = this.PlayPlace.ID;
            dataRow["FromDateAndHour"] = this.FromDateAndHour.ToString("MM/dd/yy HH:mm:ss tt");
            dataRow["ToDateAndHour"] = this.ToDateAndHour.ToString("MM/dd/yy HH:mm:ss tt");
        }
    }
}
