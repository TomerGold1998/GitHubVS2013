using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Shows;
using DBAcsses;
using System.Data;

namespace DAL.Subscriptions
{
  public class Ticket : IEntity
    {
        public static string ShowRelation = "TicketToShowRelation";

        public string TicketID { get; set; }
        public int RowNumber { get; set; }
        public int LocationInRow { get; set; }
        public Show Show { get; set; }
        
        public Ticket(DataRow dataRow)
        {
            this.TicketID = dataRow["TicketID"].ToString();
            this.RowNumber = int.Parse(dataRow["RowNumber"].ToString());
            this.LocationInRow = int.Parse(dataRow["LocationInRow"].ToString());
            this.Show = new Show(dataRow.GetParentRow(ShowRelation));
        }

        public void populate(DataRow dataRow)
        {
            dataRow["TicketID"] = this.TicketID.ToString();
            dataRow["RowNumber"] = this.RowNumber.ToString();
            dataRow["LocationInRow"] = this.LocationInRow.ToString();
            dataRow["Show"] = this.Show.ID;
        }
    }
}
