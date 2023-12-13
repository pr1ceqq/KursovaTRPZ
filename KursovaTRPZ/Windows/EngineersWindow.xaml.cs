using System.Collections.ObjectModel;
using System.Windows;
using KursovaTRPZ.Models;
using Microsoft.EntityFrameworkCore;

namespace KursovaTRPZ
{
    public partial class EngineersWindow : Window
    {
        public ObservableCollection<Engineer> Engineers { get; set; }

        public EngineersWindow()
        {
            InitializeComponent();
            using (var dbContext = new MyDbContext())
            {
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
                var newEngie = new Engineer
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text
                };
                using (var dbContext = new MyDbContext())
                {
                    dbContext.Users.Add(newEngie); 
                    dbContext.SaveChanges();
                }
                
                var newAuth = new Auth
                {
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Password, 
                    UserId = newEngie.UserId
                };
                using (var dbContext = new MyDbContext())
                {
                    dbContext.Auth.Add(newAuth); 
                    dbContext.SaveChanges();
                }
                
                FirstNameTextBox.Clear();
                LastNameTextBox.Clear();
                LoginTextBox.Clear();
                PasswordTextBox.Clear();
                using (var dbContext = new MyDbContext())
                {
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