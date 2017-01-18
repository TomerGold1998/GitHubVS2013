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
            this.workerDB = new WorkerDB();
            this.employyesHoursDB = new EmployesHoursDB();
            this.customerDB = new CustomerDB();
            this.playDB = new PlayDB();
            this.showDB = new ShowDB();
            this.auditoriumDB = new AuditoriumDB();
            this.actorInShowDB = new ActorsInShowsDB();
            this.subscriptionDB = new SubscriptionDB();
            this.ticketDB = new TicketDB();
            this.ticketSubscrioptionDB = new TicketSubscriptionDB();

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



        #region Employees Mangagment 

        private void OpenWorkersMannagmentView()
        {

            Console.WriteLine("");
            Console.WriteLine("Welcome to employees managment, please choose an action from the following list(q to quit, b to go back to the main screen)");
            Console.WriteLine("To add a new employee, Press 1.");
            Console.WriteLine("To see the full list of all the employees, Press 2.");
            Console.WriteLine("To get search employee data by ID, Press 3.");
            Console.WriteLine("To add employee hours of work, Press 4");
            Console.WriteLine("To cauclate Employee salary, Press 5");
            string Choice = Console.ReadLine().ToLower();

            switch (Choice)
            {
                case "q": return;

                case "b": InitApp();
                    break;
                case "1": AddNewEmployee();
                    break;
                case "2": ListOfAllEmployees();
                    break;
                case "3": EmployeeDataByID();
                    break;
                case "4": AddNewEmployeeHours();
                    break;
                case "5": CaucalteEmployeeSalary();
                    break;
                default: Console.WriteLine("Invalid choice"); OpenWorkersMannagmentView();
                    break;
            }



        } //Done

        private void CaucalteEmployeeSalary()
       {
            Console.WriteLine("Enter Employee ID, b to back");
            string EmployeeID = Console.ReadLine().ToLower();
            if (EmployeeID == "b")
            {
                OpenWorkersMannagmentView();
            }
            else
            {
                if (!workerDB.Find(EmployeeID))
                {
                    Console.WriteLine("Could not find the Employee by that ID");
                    CaucalteEmployeeSalary();

                }
                else
                {
                    Worker w = new Worker(workerDB.FindRow(EmployeeID));

                    DateTime FromCaucaltion;
                    do
                    {
                        Console.WriteLine("Enter the date you want to caculate from the employee salaray");
                    } while (!DateTime.TryParse(Console.ReadLine(),out FromCaucaltion));

                    Console.WriteLine("The employee salary is: "+employyesHoursDB.CaculateEmployeeSalary(FromCaucaltion, w)+"NIS");

                }
            }
        } //Done

        private void AddNewEmployeeHours()
        {
            Console.WriteLine("Enter Employee ID, b to back");
            string EmployeeID = Console.ReadLine().ToLower();
            if (EmployeeID == "b")
            {
                OpenWorkersMannagmentView();
            }
            else
            {
                if (!workerDB.Find(EmployeeID))
                {
                    Console.WriteLine("Could not find the Employee by that ID");
                    AddNewEmployeeHours();

                }
                else
                {
                 Worker w = new Worker(workerDB.FindRow(EmployeeID));

                 int Date = 0;
                 do
                 {
                     Console.WriteLine("Enter Date in this month(1-31)");
                 } while (!int.TryParse(Console.ReadLine(), out Date) && Date >=1 & Date <= 31);

                 int StartHour = 0;
                 do
                 {
                     Console.WriteLine("Enter Start Hour(0-23)");
                 } while (!int.TryParse(Console.ReadLine(), out StartHour) && StartHour >= 0 & StartHour < 24);

                 int EndHour = 0;
                 do
                 {
                     Console.WriteLine("Enter end Hour(0-24)");
                 } while (!int.TryParse(Console.ReadLine(), out EndHour) && EndHour >= 0 & EndHour < 24 && EndHour >= StartHour);

                 DateTime DateOfWork = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Date);
                 DateTime from = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Date, StartHour, 0, 0);
                 DateTime to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Date, EndHour, 0, 0);

                 EmployesHours eh = new EmployesHours(w,DateOfWork,from,to);
                 this.employyesHoursDB.AddEmployesHours(eh);

                 Console.WriteLine("Added employee hours");
                 OpenWorkersMannagmentView();
                 
                }
            }
        } //Done

        private void EmployeeDataByID()
        {
            Console.WriteLine("Enter Employee ID, b to back");
            string EmployeeID = Console.ReadLine().ToLower();
            if (EmployeeID == "b")
            {
                OpenWorkersMannagmentView();
            }
            else
            {
                if (!workerDB.Find(EmployeeID))
                {
                    Console.WriteLine("Could not find the Employee by that ID");
                    EmployeeDataByID();

                }
                else
                {
                    Console.WriteLine("Worker : {0}",new Worker(workerDB.FindRow(EmployeeID)).ToString());
                }
            }
            Console.WriteLine();
            OpenWorkersMannagmentView();

        } //Done

        private void ListOfAllEmployees()
        {
            Console.WriteLine();
            Console.WriteLine("All workers:");
            foreach (Worker w in workerDB.GetAllWorkers())
            {
                Console.WriteLine(w.ToString());
            }
            Console.WriteLine();
            OpenWorkersMannagmentView();
        } //Done

        private void AddNewEmployee()
        {
            Console.WriteLine("You Chose to add a new employee you can go back to the Employee managment by writing b on the employee ID column");
            Console.WriteLine("Enter new Employee ID: ");
            string EmployeeID = Console.ReadLine().ToLower();
            if (EmployeeID == "b")
            {
                OpenWorkersMannagmentView();
            }
            else
            {
                if (EmployeeID.Length == 9 && !workerDB.Find(EmployeeID))
                {
                    Console.WriteLine("Enter employee first name");
                    string EmployeeFirstName = Console.ReadLine();
                    Console.WriteLine("Enter employee last name");
                    string EmployeeLastName = Console.ReadLine();

                    DateTime EmployeeDateOfBirth;
                    do{
                    Console.WriteLine("Enter employee date of birth");                     
                    }while(!DateTime.TryParse(Console.ReadLine(),out EmployeeDateOfBirth));
                 
                    
                    Console.WriteLine("Enter employee address");
                    string EmployeeAdress = Console.ReadLine();
                    Console.WriteLine("Enter employee City");
                    string EmployeeCity = Console.ReadLine();

                    double EmployeeHourSalary;
                    do
                    {
                        Console.WriteLine("Enter employee hour salary");
                    }
                    while (!double.TryParse(Console.ReadLine(), out EmployeeHourSalary));

                    int EmployeeLevel;
                    do
                    {
                        Console.WriteLine("Enter employee level");
                    }
                    while (!int.TryParse(Console.ReadLine(), out EmployeeLevel));

                    int EmployeeExspreince;
                    do
                    {
                        Console.WriteLine("Enter employee exprince(in years)");
                    }
                    while (!int.TryParse(Console.ReadLine(), out EmployeeExspreince));

                    string EmployeeType = "";
                    
                    do
                    {  
                        Console.WriteLine("Enter employee type: (Salesman, Adminstration , Actor , Director)");
                        EmployeeType = Console.ReadLine();
                    } while (EmployeeType != "Salesman" && EmployeeType != "Adminstration" && EmployeeType != "Actor" && EmployeeType != "Director");

                    EmployeesType RealType;
                    switch (EmployeeType)
                    {
                        case "Salesman": RealType = EmployeesType.Salesman;
                            break;
                        case "Adminstration": RealType = EmployeesType.Adminstration;
                            break;
                        case "Actor": RealType = EmployeesType.Actor;
                            break;
                        case "Director": RealType = EmployeesType.Director;
                            break;
                        default: RealType = EmployeesType.Salesman;
                            break;
                    }

                    if (RealType == EmployeesType.Actor)
                    {
                        string actorLeve = "";
                        do
                        {
                            Console.WriteLine("Enter Actor level: ( Lead , Secondary , starting , Extra)");
                            actorLeve = Console.ReadLine();
                        } while (actorLeve != "Lead" && actorLeve != "Secondary" && actorLeve != "starting" && actorLeve != "Extra");

                        ActorLevel RealActorLevel;
                        switch (actorLeve)
                        {
                            case "Lead": RealActorLevel = ActorLevel.Lead;
                                break;
                            case "Secondary": RealActorLevel = ActorLevel.Secondary;
                                break;
                            case "starting": RealActorLevel = ActorLevel.starting;
                                break;
                            case "Extra": RealActorLevel = ActorLevel.Extra;
                                break;
                            default: RealActorLevel = ActorLevel.Extra;
                                break;
                        }

                        Worker w = new Actor(EmployeeID, EmployeeFirstName, EmployeeLastName, EmployeeDateOfBirth, EmployeeAdress, EmployeeCity, EmployeeHourSalary, EmployeeLevel, EmployeeExspreince, RealActorLevel);
                        workerDB.AddWorker(w);
                        Console.WriteLine("Added new actor : " + w.ToString());

                    }
                    else
                    {
                        if (RealType == EmployeesType.Director)
                        {
                            Worker w = new Director(EmployeeID, EmployeeFirstName, EmployeeLastName, EmployeeDateOfBirth, EmployeeAdress, EmployeeCity, EmployeeHourSalary, EmployeeLevel, EmployeeExspreince);
                            workerDB.AddWorker(w);
                            Console.WriteLine("Added new director : " + w.ToString());
                        }
                        else
                        {
                            if (RealType == EmployeesType.Adminstration)
                            {
                                Worker w = new Adminstration(EmployeeID, EmployeeFirstName, EmployeeLastName, EmployeeDateOfBirth, EmployeeAdress, EmployeeCity, EmployeeHourSalary, EmployeeLevel, EmployeeExspreince);
                                Console.WriteLine("Added new Admin : " + w.ToString());
                            }
                            else
                            {
                                Worker w = new Worker(EmployeeID, EmployeeFirstName, EmployeeLastName, EmployeeDateOfBirth, EmployeeAdress, EmployeeCity, EmployeeHourSalary, EmployeeLevel, EmployeeExspreince,RealType);
                                Console.WriteLine("Added new worker : " + w.ToString());
                            }

 
                        }
                    }


                    OpenWorkersMannagmentView();

                }
                else
                {
                    Console.WriteLine("Invalid ID.. ");
                    AddNewEmployee();
                }
            }
        } //Done
        #endregion /

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
