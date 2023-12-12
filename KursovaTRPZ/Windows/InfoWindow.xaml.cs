using System.Windows;

namespace KursovaTRPZ
{
    public partial class InfoWindow : Window
    {
        // Add properties to store administrator information
        public int AdminId { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }

        // Constructor that takes parameters
        public InfoWindow(int adminId, string adminFirstName, string adminLastName)
        {
            InitializeComponent();

            // Assign values to properties
            AdminId = adminId;
            AdminFirstName = adminFirstName;
            AdminLastName = adminLastName;

            // Set the welcome message dynamically
            WelcomeMessageTextBlock.Text = $"Welcome, {AdminFirstName} {AdminLastName}!";
        }
        
        private void ViewEventLogsButton_Click(object sender, RoutedEventArgs e)
        {
            // You can open a new window to display EventLogs or handle it as needed
            var eventLogsWindow = new EventLogsWindow(AdminId);
            eventLogsWindow.Show();
        }

        private void ShowEngineersButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the EngineersWindow
            var engineersWindow = new EngineersWindow();
            engineersWindow.Show();
        }
    }
}