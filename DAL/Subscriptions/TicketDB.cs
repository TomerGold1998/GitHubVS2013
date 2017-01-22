﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
using System.Data;

namespace DAL.Subscriptions
{
  public  class TicketDB : GeneralTable
    {
        public TicketDB()
            : base("Ticket", "TicketID")
        {
            Shows.ShowDB Sdb = new Shows.ShowDB();
            DataColumn d1 = Sdb.GetPrimaryKeyColumn();
            DataColumn d2 = this.GetColumn("Show");
            DBConnector.GetInstance().AddRelation(Ticket.ShowRelation, d1, d2);                  
        }

        public string GetKey()
        {
            return base.PrimaryKey;
        }    

        public new Ticket GetCurrentRowData()
        {
            return new Ticket(base.GetCurrentRowData());
        }

        public void UpdateTicket(Ticket t)
        {
            base.UpdateRow(t);
        }

        public void AddTicket(Ticket t)
        {
            base.AddRow(t);
        }

        public List<Ticket> GetAllTickets()
        {
            try
            {
                this.GoToFirst();
                Ticket Current = new Ticket();
                List<Ticket> list = new List<Ticket>();

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
                return new List<Ticket>();
            }
        }
    
    }
}
