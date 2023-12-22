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
using KursovaTRPZ.Windows;

namespace KursovaTRPZ
{
    public partial class MainWindow : Window
    {
        private void GoToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
        private void EnterAsGuest_Click(object sender, RoutedEventArgs e)
        {
            GuestWindow guestWindow = new GuestWindow();
            guestWindow.Show();
            this.Close(); 
        }
    }
}
