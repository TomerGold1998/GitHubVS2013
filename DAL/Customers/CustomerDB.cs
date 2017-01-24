using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAcsses;
namespace DAL.Customers
{
  public  class CustomerDB : GeneralTable
    {
        public CustomerDB() : base("Customer", "ID") { }

        public string GetKey()
        {
            return base.PrimaryKey;
        }

        public new Customer GetCurrentRowData()
        {
            return new Customer(base.GetCurrentRowData());
        }

        public void UpdateCustomer(Customer c)
        {
            base.UpdateRow(c);
        }

        public void AddCustomer(Customer c)
        {
            base.AddRow(c);
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                this.GoToFirst();
                Customer Current = new Customer();
                List<Customer> list = new List<Customer>();

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
                return new List<Customer>();
            }
        }
    }
}
