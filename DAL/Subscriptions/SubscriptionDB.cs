using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using System.Data;

namespace DAL.Subscriptions
{
   public class SubscriptionDB : GeneralTable
    {
        public SubscriptionDB()
            : base("Subscription", "ID")
        {
            Customers.CustomerDB Cdb = new Customers.CustomerDB();
            DataColumn d1 = Cdb.GetPrimaryKeyColumn();
            DataColumn d2 = this.GetColumn("Customer");
            DBConnector.GetInstance().AddRelation(Subscription.CustomerRelation, d1, d2);                  
        }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new Subscription GetCurrentRowData()
        {
            return new Subscription(base.GetCurrentRowData());
        }

        public void UpdateSubscription(Subscription s)
        {
            base.UpdateRow(s);
        }

        public void AddSubscription(Subscription s)
        {
            base.AddRow(s);
        }
    }
}
