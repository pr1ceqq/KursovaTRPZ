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
                }

                // If authentication succeeds, close the login window
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
                // Query the Auth table to find a match for the provided username and password
                var user = dbContext.Auth.FirstOrDefault(u => u.Login == username && u.Password == password);

                // Set the authentication status
                IsAuthenticated = user != null;

                return IsAuthenticated;
            }
        }
    }
}
