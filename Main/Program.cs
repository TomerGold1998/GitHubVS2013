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
            try
            {
                Console.WriteLine("System loading..");
                WorkerDB WorkerDB = new WorkerDB();
                EmployesHoursDB EmployyesHoursDB = new EmployesHoursDB();
                CustomerDB CustomerDB = new CustomerDB();

                PlayDB PlayDB = new PlayDB();
                ShowDB ShowDB = new ShowDB();
                ActorsInShowsDB ActorInShowDB = new ActorsInShowsDB();

                SubscriptionDB SubscriptionDB = new SubscriptionDB();
                TicketDB TicketDB = new TicketDB();
                TicketSubscriptionDB TicketSubscrioptionDB = new TicketSubscriptionDB();
                Console.WriteLine("Done!");

                Console.WriteLine();
                Console.WriteLine();
                int DepartmentChoice = GetDepartment();
                if (DepartmentChoice == -1)
                {
                    return;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error had occuerd, Error information : {0}", ex.Message);
                Console.ReadLine();
            }

            /*
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
           */


        }

        private static int GetDepartment()
        {
            Console.WriteLine("For workers mannagemnt, Press 1. For Shows and Plays mannagment, Press 2. For Customers and Subscription Mannagment, Press 3. (q to exit)");
            string choice = Console.ReadLine().ToLower();
            switch(choice)
            {
                case "q": return -1;
                case "1": return 1;
                case "2": return 2;
                case "3": return 3;
                default: Console.WriteLine("Invalid choice"); return GetDepartment();
            }
        }
    }
}
