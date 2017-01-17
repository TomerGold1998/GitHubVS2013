using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Customers;
using DBAcsses;
using System.Data;

namespace DAL.Subscriptions 
{
  public  class Subscription : IEntity
    {
        public static string CustomerRelation = "SubscriptionToCustomerRelation";
        public string ID { get; set; }
        public Customer Customer { get; set; }

        public Subscription(DataRow dr)
        {
            this.ID = dr["ID"].ToString();
            this.Customer = new Customer(dr.GetParentRow(CustomerRelation));
        }

        public void populate(DataRow dataRow)
        {
            dataRow["ID"] = this.ID;
            dataRow["Customer"] = this.Customer.ID;
        }
    }
}
