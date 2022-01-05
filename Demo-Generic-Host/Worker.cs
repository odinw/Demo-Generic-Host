using Demo_Generic_Host.Helpers;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Demo_Generic_Host
{
    public class Worker : BackgroundService
    {
        readonly IDbHelper _dbHelper;

        public Worker(IDbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Factory.StartNew(async () =>
            {
                Console.WriteLine("Execute Background Service");

                var dbInfo = _dbHelper.GetDbInfo();
                Console.WriteLine(dbInfo);

                await Task.Delay(1000);
                Environment.Exit(0);
            });
        }
    }
}
