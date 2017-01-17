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
    public class App
    {
        private WorkerDB workerDB;
        private EmployesHoursDB employyesHoursDB;
        private CustomerDB customerDB;

        private PlayDB playDB;
        private ShowDB showDB;
        private AuditoriumDB auditoriumDB;
        private ActorsInShowsDB actorInShowDB;

        private SubscriptionDB subscriptionDB;
        private TicketDB ticketDB;
        private TicketSubscriptionDB ticketSubscrioptionDB;

        public App()
        {
            Console.WriteLine("Loading App");
            workerDB = new WorkerDB();
            employyesHoursDB = new EmployesHoursDB();
            customerDB = new CustomerDB();
            playDB = new PlayDB();
            showDB = new ShowDB();
            auditoriumDB = new AuditoriumDB();
            actorInShowDB = new ActorsInShowsDB();
            subscriptionDB = new SubscriptionDB();
            ticketDB = new TicketDB();
            ticketSubscrioptionDB = new TicketSubscriptionDB();

            Console.WriteLine("Done");

            Console.WriteLine();
            Console.WriteLine();

            InitApp();
        }


        private void InitApp()
        {
            int DepartmentChoice = this.GetDepartment();
            if (DepartmentChoice == -1)
            {
                return;
            }
            else
            {
                switch (DepartmentChoice)
                {
                    case 1: OpenWorkersMannagmentView();
                        break;
                    case 2: OpenShowsAndPlaysMannagment();
                        break;
                    case 3: OpenCustomerAndSubscriptionMannagment();
                        break;
                    default: return;
                }
            }
        }

        private void OpenCustomerAndSubscriptionMannagment()
        {

        }

        private void OpenShowsAndPlaysMannagment()
        {

        }

        private void OpenWorkersMannagmentView()
        {

        }




        private int GetDepartment()
        {
            Console.WriteLine("For workers mannagemnt, Press 1. For Shows and Plays mannagment, Press 2. For Customers and Subscription Mannagment, Press 3. (q to exit)");
            string choice = Console.ReadLine().ToLower();
            switch (choice)
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
