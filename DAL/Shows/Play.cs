using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using System.Data;

namespace DAL.Shows
{
   public class Play : IEntity
    {
       public static string DirectorRelation = "PlayToDirectorRelation";

        public string ID{get;set;}
        public string p_Name { get; set; }
        public Director Director { get; set; }
        public string CreatorName { get; set; }
        public DateTime StartDate { get; set; }
     
        public Play(DataRow dataRow)
        {
            this.ID = dataRow["ID"].ToString();
            this.p_Name = dataRow["p_Name"].ToString();
            this.Director = new Director(dataRow.GetParentRow(DirectorRelation));
            this.CreatorName = dataRow["CreatorName"].ToString();
            this.StartDate = DateTime.Parse(dataRow["StartDate"].ToString());
        }

        public Play()
        {
            
        }

        public void populate(System.Data.DataRow dataRow)
        {
            dataRow["ID"] = this.ID;
            dataRow["p_Name"] = this.p_Name;
            dataRow["Director"] = this.Director.ID;
            dataRow["CreatorName"] = this.CreatorName;
            dataRow["StartDate"] = this.StartDate.ToShortDateString();
        }

        public override string ToString()
        {
            return string.Format("ID: {0}, Name: {1}, directe by {2} {3} {4}, Creator :{5} , StartDate {6}", this.ID, this.p_Name, this.Director.ID, this.Director.FirstName, this.Director.LastName, this.CreatorName, this.StartDate.ToShortDateString());
        }
    }
}
