using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using System.Data;

namespace DAL.Subscriptions
{
 public   class TicketSubscription : IEntity
    {
        public static string SubscriptionRelation = "TicketSubscriptionToSubscriptionRelation";
        public static string TicketRelation = "TicketSubscriptionToTicketRelation";


        public string ID { get; set;}
        public Subscription Subscription { get; set; }
        public Ticket ticket { get; set; }


        public TicketSubscription(DataRow dataRow)
        {
            this.ID = dataRow["ID"].ToString();
            this.Subscription = new Subscription(dataRow.GetParentRow(SubscriptionRelation));
            this.ticket = new Ticket(dataRow.GetParentRow(TicketRelation));
        }

        public TicketSubscription()
        {
            // TODO: Complete member initialization
        }

        public void populate(System.Data.DataRow dataRow)
        {
            dataRow["ID"] = this.ID;
            dataRow["Subscription"] = this.Subscription.ID;
            dataRow["ticket"] = this.ticket.TicketID;
        }
    }
}
