using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Linq;
using KursovaTRPZ.Models;
using System.Windows;
using System.Linq;
using KursovaTRPZ.Models;

namespace KursovaTRPZ
{
    public partial class LoginWindow : Window
    {
        // Add a property to indicate authentication status
        public bool IsAuthenticated { get; private set; }
        public int Admin_Id { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve username and password
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Validate credentials
            if (AuthenticateUser(username, password))
            {
                // Set the authentication status to true
                IsAuthenticated = true;

                // Set the Admin_Id based on the authenticated user
                using (var dbContext = new MyDbContext())
                {
                    var authenticatedUser = dbContext.Auth.FirstOrDefault(u => u.Login == username && u.Password == password);
                    if (authenticatedUser != null)
                    {
                        Admin_Id = authenticatedUser.UserId;
                    }
                    var User = dbContext.Users.FirstOrDefault(user => user.UserId == Admin_Id);
                    if (User is Administrator admin)
                    {
                        var userId = admin.UserId;
                        var userFirstName = admin.FirstName;
                        var userLastName = admin.LastName;
                        var infoWindow = new AdminWindow(userId, userFirstName, userLastName);
                        infoWindow.Show();
                        this.Close(); 
                    }
                    else if (User is Engineer engineer)
                    {
                        var userId = engineer.UserId;
                        var userFirstName = engineer.FirstName;
                        var userLastName = engineer.LastName;
                        var EngieWindow = new EngineerMenuWindow(userId, userFirstName, userLastName);
                        EngieWindow.Show();
                        this.Close(); 
                    }
                }
                Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials. Please try again.");
            }
        }
        
        private bool AuthenticateUser(string username, string password)
        {
            using (var dbContext = new MyDbContext())
            {
                var user = dbContext.Auth.FirstOrDefault(u => u.Login == username && u.Password == password);
                
                IsAuthenticated = user != null;

                return IsAuthenticated;
            }
        }
    }
}
