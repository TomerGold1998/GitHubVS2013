using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Workers;
using DAL.Subscriptions;
using DAL.Shows;
using DAL.Customers;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");


             DateTime d = new DateTime(1998,7,25);
              Worker w = new Worker("206949158", "Tomer", "Goldberg", d, "Mivza Nacshon", "Modiin", 35, 2, 2, EmployeesType.Salesman);
              WorkerDB Wdb = new WorkerDB();
              Wdb.AddWorker(w);

              Console.WriteLine("Done!");
              Console.ReadKey();  //*** Works for worker!
           
              Customer c = new Customer() { ID = "206349158", c_Name = "Yoe Master", Type = CustomerType.Private };
              CustomerDB Cdb = new CustomerDB();
              Cdb.AddCustomer(c);
              Console.WriteLine("Done!");
              Console.ReadKey();   // Works for Customer!
           


        }
    }
}
