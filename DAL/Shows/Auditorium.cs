using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using System.Data;

namespace DAL.Shows
{
   public class Auditorium : IEntity
    {
        public string ID { get; set; }
        public string a_Name{get;set;}
        public int NumberOfRows { get; set; }
        public int NumberOfSitsInRow {get;set;}
        public AuditoriumStyle a_Style { get; set; }
        public AuditoriumType a_Type { get; set; }

        public Auditorium(DataRow dr)
        {
            this.ID = dr["ID"].ToString();
            this.a_Name = dr["a_Name"].ToString();
            this.NumberOfRows = int.Parse(dr["NumberOfRows"].ToString());
            this.NumberOfSitsInRow = int.Parse(dr["NumberOfSitsInRow"].ToString());
            this.a_Style = dr["a_Style"].ToString() == "Regular" ? AuditoriumStyle.Regular : AuditoriumStyle.Cirular;
            this.a_Type = dr["a_Type"].ToString() == "Regular" ? AuditoriumType.Regular : dr["a_Type"].ToString() == "Public" ? AuditoriumType.Public : AuditoriumType.Siesta;
        }
        public void populate(System.Data.DataRow dr)
        {
            dr["ID"] = this.ID;
            dr["a_Name"] = this.a_Name;
            dr["NumberOfRows"] = this.NumberOfRows.ToString();
            dr["NumberOfSitsInRow"] = this.NumberOfSitsInRow.ToString();
            dr["a_Style"] = this.a_Style.ToString();
            dr["a_Type"] = this.a_Type.ToString();
        }
    }
    public enum AuditoriumStyle
    { 
        Regular , Cirular
    }
    public enum AuditoriumType
   {
       Regular,Public,Siesta
   }
}
