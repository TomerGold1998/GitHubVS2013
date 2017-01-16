using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DBAcsses;
namespace DAL.Customers
{
  public  class Customer :IEntity
    {      
        public string ID { get; set; }
        public string c_Name { get; set; }
        public CustomerType Type{ get;set;}

        public Customer(DataRow dataRow)
        {
            this.ID = dataRow["ID"].ToString();
            this.c_Name = dataRow["c_Name"].ToString();
            this.Type = dataRow["Type"].ToString() == "Institutional" ? CustomerType.Institutional : dataRow["Type"].ToString() == "VIP" ? CustomerType.VIP : CustomerType.Private;
        }

        public Customer()
        {
            // TODO: Complete member initialization
        }
        public void populate(DataRow dataRow)
        {
            dataRow["ID"] = this.ID;
            dataRow["c_Name"] = this.c_Name;
            dataRow["Type"] = this.Type.ToString();
        }
    }

  public enum CustomerType {
      Private,VIP,Institutional
  }
}
