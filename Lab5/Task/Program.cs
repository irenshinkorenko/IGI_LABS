using System;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Application.Models;
using Lab5.Data;

namespace Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (HospitalContext db = new HospitalContext())
            {
                DbInitializer.Initialize(db);
            }
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel(options => options.Listen(IPAddress.Loopback, 50023))
                .Build();
    }
}
