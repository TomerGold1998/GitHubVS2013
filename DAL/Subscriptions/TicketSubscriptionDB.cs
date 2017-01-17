using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DBAcsses;

namespace DAL.Subscriptions
{
  public  class TicketSubscriptionDB : GeneralTable
    {
        public TicketSubscriptionDB()
            : base("TicketSubscription", "ID")
        {
            SubscriptionDB Sdb = new SubscriptionDB();
            DataColumn d1 = Sdb.GetPrimaryKeyColumn();
            DataColumn d2 = this.GetColumn("Subscription");
            DBConnector.GetInstance().AddRelation(TicketSubscription.SubscriptionRelation, d1, d2);

            TicketDB Tdb = new TicketDB();
            DataColumn d3 = Tdb.GetPrimaryKeyColumn();
            DataColumn d4 = this.GetColumn("ticket");
            DBConnector.GetInstance().AddRelation(TicketSubscription.TicketRelation, d3, d4);            
        }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new TicketSubscription GetCurrentRowData()
        {
            return new TicketSubscription(base.GetCurrentRowData());
        }

        public void UpdateTicketSubscription(TicketSubscription ts)
        {
            base.UpdateRow(ts);
        }

        public void AddTicketSubscription(TicketSubscription ts)
        {
            base.AddRow(ts);
        }
    }
}
