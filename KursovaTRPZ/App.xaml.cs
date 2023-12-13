using System;
using System.Configuration;
using System.Data;
using System.Windows;
using KursovaTRPZ.Models;

namespace KursovaTRPZ
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Create an instance of your database context
            using (var dbContext = new MyDbContext())
            {
                // Seed data
                var dataSeeder = new DataSeeder(dbContext);
                dataSeeder.SeedData();
            }
        }
    }
}