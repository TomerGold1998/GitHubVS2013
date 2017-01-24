using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DBAcsses;
using DAL.Shows;
using DAL.Customers;

namespace DAL.Subscriptions
{
    public class TicketSubscriptionDB : GeneralTable
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

        public bool TestIfSitIsOpen(Show s, int ReqestedRow, int ReqestetLocationInRow)
        {
            return GetAllTicketSubscriptions().Where(t => t.ticket.Show.ID == s.ID && t.ticket.RowNumber == ReqestedRow && t.ticket.LocationInRow == ReqestetLocationInRow).ToList().Count == 0;
        }
        public List<TicketSubscription> GetAllTicketSubscriptions()
        {
            try
            {
                this.GoToFirst();
                TicketSubscription Current = new TicketSubscription();
                List<TicketSubscription> list = new List<TicketSubscription>();

                for (int i = 0; i < this.LengthOfTable; i++)
                {
                    Current = this.GetCurrentRowData();
                    if (Current != null)
                    {
                        list.Add(Current);
                        this.MoveNext();
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                return new List<TicketSubscription>();
            }
        }

        public double CalcPaymentForCustomer(Customer c, int NumberOfTicket)
        {
            int DiscountPer = 0;
            switch (c.Type)
            {
                case CustomerType.Private:
                    if (GetAllTicketSubscriptions().Where(ts => ts.Subscription.Customer.ID == c.ID).ToList().Count >= 10)
                    {
                        DiscountPer = 10;
                    }
                    break;

                case CustomerType.VIP:
                    if (GetAllTicketSubscriptions().Where(ts => ts.Subscription.Customer.ID == c.ID).ToList().Count >= 5)
                    {
                        DiscountPer = 20;
                    }
                    break;
                case CustomerType.Institutional:
                    if (GetAllTicketSubscriptions().Where(ts => ts.Subscription.Customer.ID == c.ID).ToList().Count >= 100)
                    {
                        DiscountPer = 2;
                    }
                    break;

            }
            if (DiscountPer == 0)
            {
                return (NumberOfTicket * 50);
            }else
            {
                return (NumberOfTicket * 50) * ((100 - DiscountPer) / 100);
            }
        }


    }
}
