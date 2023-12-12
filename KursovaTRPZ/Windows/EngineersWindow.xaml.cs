using System.Collections.ObjectModel;
using System.Windows;
using KursovaTRPZ.Models;
using Microsoft.EntityFrameworkCore;

namespace KursovaTRPZ
{
    public partial class EngineersWindow : Window
    {
        // Assuming you have the Engineers collection available
        public ObservableCollection<Engineer> Engineers { get; set; }

        public EngineersWindow()
        {
            InitializeComponent();
            using (var dbContext = new MyDbContext())
            {
                // Query the EventLogs associated with the specified adminId
                Engineers = new ObservableCollection<Engineer>(dbContext.Engineers.Include(e => e.Auth).ToList());
                EngineersDataGrid.ItemsSource = Engineers;
            }
        }

        private void AddEngineerButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) || string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                MessageBox.Show("Fill all pls!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Add a new user to the collection
                var newEngie = new Engineer
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text
                };
                // Save changes to the database
                using (var dbContext = new MyDbContext())
                {
                    dbContext.Users.Add(newEngie); // Assuming Users includes all user types
                    dbContext.SaveChanges();
                }

                // Add a new auth to the collection
                var newAuth = new Auth
                {
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Password, // Use Password property for security
                    UserId = newEngie.UserId
                };
                using (var dbContext = new MyDbContext())
                {
                    dbContext.Auth.Add(newAuth); // Assuming Users includes all user types
                    dbContext.SaveChanges();
                }

                // Clear input fields
                FirstNameTextBox.Clear();
                LastNameTextBox.Clear();
                LoginTextBox.Clear();
                PasswordTextBox.Clear();
                using (var dbContext = new MyDbContext())
                {
                    // Clear the existing collection and add the updated engineers
                    Engineers.Clear();
                    dbContext.Engineers.Include(e => e.Auth).ToList().ForEach(Engineers.Add);
                }
            }
        }
        private void DeleteEngineerButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(EngineerIdToDeleteTextBox.Text, out int engineerIdToDelete))
            {
                using (var dbContext = new MyDbContext())
                {
                    var engineerToDelete = dbContext.Engineers.Find(engineerIdToDelete);

                    if (engineerToDelete != null)
                    {
                        dbContext.Engineers.Remove(engineerToDelete);
                        dbContext.SaveChanges();

                        // Clear the existing collection and add the updated engineers
                        Engineers.Clear();
                        dbContext.Engineers.ToList().ForEach(Engineers.Add);

                        MessageBox.Show($"Engineer with ID {engineerIdToDelete} deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Engineer with ID {engineerIdToDelete} not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Invalid Engineer ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}