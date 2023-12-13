using System.Windows;
using KursovaTRPZ.Models;
using KursovaTRPZ.Windows.SensorWindows;

namespace KursovaTRPZ
{
    public partial class EngineerMenuWindow : Window
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EngineerMenuWindow(int id, string firstName, string lastName)
        {
            InitializeComponent();
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            WelcomeMessageTextBlock.Text = $"Welcome, {FirstName} {LastName}!";
        }

        private void ShowRadiationSensorWindow(object sender, RoutedEventArgs e)
        {
            RadiationSensorWindow radiationSensorWindow = new RadiationSensorWindow(Id);
            radiationSensorWindow.Show();
        }

        private void ShowSoilSensorWindow(object sender, RoutedEventArgs e)
        {
            SoilSensorWindow soilSensorWindow = new SoilSensorWindow(Id);
            soilSensorWindow.Show();
        }
        
        private void ShowWaterSensorWindow(object sender, RoutedEventArgs e)
        {
            WaterSensorWindow waterSensorWindow = new WaterSensorWindow(Id);
            waterSensorWindow.Show();
        }
        
        private void ShowMotionSensorWindow(object sender, RoutedEventArgs e)
        {
            MotionSensorWindow motionSensorWindow = new MotionSensorWindow(Id);
            motionSensorWindow.Show();
        }
    }
}