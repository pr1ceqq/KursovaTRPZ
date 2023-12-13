using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KursovaTRPZ.Models;

namespace KursovaTRPZ.Windows.SensorWindows
{
    public partial class MotionSensorWindow : Window
    {
        private readonly int Id;

        public MotionSensorWindow(int id)
        {
            InitializeComponent();
            Id = id;
            DisplayMotionSensors();
        }

        private void DisplayMotionSensors()
        {
            try
            {
                using (var dbContext = new MyDbContext())
                {
                    var motionSensors = new ObservableCollection<MotionSensor>(dbContext.MotionSensors.Where(el => el.Engineer.UserId == Id).ToList());
                    MotionSensorDataGrid.ItemsSource = motionSensors;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching Motion Sensors: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateSensor_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetSensorValues(out bool motionValue, out string location))
            {
                try
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var authenticatedUser = dbContext.Users.FirstOrDefault(user => user.UserId == Id);
                        if (authenticatedUser is Engineer engineer)
                        {
                            var newSensor = new MotionSensor
                            {
                                Engineer = engineer,
                                MotionSensor_Value = motionValue,
                                Sensor_Location = location
                            };
                            dbContext.MotionSensors.Add(newSensor);
                            dbContext.SaveChanges();
                            DisplayMotionSensors(); 
                            InfoTextBlock.Text = $"New Sensor created: Motion Value = {motionValue}, Location = {location}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding Motion Sensor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool TryGetSensorValues(out bool motionValue, out string location)
        {
            if (bool.TryParse(MotionValueTextBox.Text, out motionValue))
            {
                location = LocationTextBox.Text;
                return true;
            }
            else
            {
                MessageBox.Show("Invalid Motion Value. Please enter a valid boolean value (true/false).", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                motionValue = false; 
                location = "";
                return false;
            }
        }
        
        private void DeleteMotionSensorByIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(DeleteMotionSensorIdTextBox.Text, out int sensorIdToDelete))
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var sensorToDelete = dbContext.MotionSensors.Find(sensorIdToDelete);

                        if (sensorToDelete != null)
                        {
                            dbContext.MotionSensors.Remove(sensorToDelete);
                            dbContext.SaveChanges();
                            MessageBox.Show("Motion Sensor deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            DisplayMotionSensors(); 
                        }
                        else
                        {
                            MessageBox.Show("Motion Sensor with specified ID not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Motion Sensor ID. Please enter a valid numeric ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Motion Sensor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
