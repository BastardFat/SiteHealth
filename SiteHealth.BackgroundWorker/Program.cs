using Hangfire;
using Hangfire.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteHealth.BackgroundWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press ENTER key to exit...");
                Console.ReadLine();
            }
        }
    }
}
