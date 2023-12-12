using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KursovaTRPZ.Models;
using System.Windows;
using KursovaTRPZ.Models;

namespace KursovaTRPZ
{
    public partial class MainWindow : Window
    {
        private void GoToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            // Check if the user is authenticated and proceed accordingly
            if (loginWindow.IsAuthenticated)
            {
                var dbContext = new MyDbContext();
                var authenticatedUser = dbContext.Users.FirstOrDefault(user => user.UserId == loginWindow.Admin_Id);

                if (authenticatedUser != null)
                {
                    if (authenticatedUser is Administrator admin)
                    {
                        // It's an Administrator
                        var userId = admin.UserId;
                        var userFirstName = admin.FirstName;
                        var userLastName = admin.LastName;
                        // Open the InfoWindow and pass the User's information
                        var infoWindow = new InfoWindow(userId, userFirstName, userLastName);
                        infoWindow.Show();
                    }
                    else if (authenticatedUser is Engineer engineer)
                    {
                        // It's an Engineer
                        var userId = engineer.UserId;
                        var userFirstName = engineer.FirstName;
                        var userLastName = engineer.LastName;
                        var EngieWindow = new EngineerMenuWindow(userId, userFirstName, userLastName);
                        EngieWindow.Show();
                    }


                    // Close the login window
                    loginWindow.Close();
                }
                else
                {
                    // Handle authentication failure if needed
                }
            }
            else
            {
                // Handle authentication failure if needed
            }
        }

    }
}
