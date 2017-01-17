using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                App app = new App();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error had occuerd, Error information : {0}", ex.Message);
                Console.ReadLine();
            }


        }
    }
}
