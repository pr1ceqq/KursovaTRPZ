using System.Windows;

namespace KursovaTRPZ
{
    public partial class AdminWindow : Window
    {
        // Add properties to store administrator information
        public int AdminId { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }

        // Constructor that takes parameters
        public AdminWindow(int adminId, string adminFirstName, string adminLastName)
        {
            InitializeComponent();

            AdminId = adminId;
            AdminFirstName = adminFirstName;
            AdminLastName = adminLastName;

            WelcomeMessageTextBlock.Text = $"Welcome, {AdminFirstName} {AdminLastName}!";
        }
        
        private void ViewEventLogsButton_Click(object sender, RoutedEventArgs e)
        {
            var eventLogsWindow = new EventLogsWindow(AdminId);
            eventLogsWindow.Show();
        }

        private void ShowEngineersButton_Click(object sender, RoutedEventArgs e)
        {
            var engineersWindow = new EngineersWindow(AdminId);
            engineersWindow.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}