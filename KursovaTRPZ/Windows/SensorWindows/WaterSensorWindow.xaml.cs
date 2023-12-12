using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using KursovaTRPZ.Models;

namespace KursovaTRPZ.Windows.SensorWindows
{
    public partial class WaterSensorWindow : Window
    {
        private readonly int Id;

        public WaterSensorWindow(int id)
        {
            InitializeComponent();
            Id = id;
            DisplayWaterSensors();
        }

        private void DisplayWaterSensors()
        {
            try
            {
                using (var dbContext = new MyDbContext())
                {
                    var waterSensors = new ObservableCollection<WaterSensor>(dbContext.WaterSensors.Where(el => el.Engineer.UserId == Id).ToList());
                    WaterSensorDataGrid.ItemsSource = waterSensors;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching Water Sensors: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateWaterSensor_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetSensorValues(out float waterPhValue, out string location))
            {
                try
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var authenticatedUser = dbContext.Users.FirstOrDefault(user => user.UserId == Id);
                        if (authenticatedUser is Engineer engineer)
                        {
                            var newSensor = new WaterSensor
                            {
                                Engineer = engineer,
                                Ph_Value = waterPhValue,
                                Sensor_Location = location
                            };
                            dbContext.WaterSensors.Add(newSensor);
                            dbContext.SaveChanges();
                            DisplayWaterSensors(); // Refresh the displayed Water Sensors
                            InfoTextBlock.Text = $"New Sensor created: pH Value = {waterPhValue}, Location = {location}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding Water Sensor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool TryGetSensorValues(out float waterPhValue, out string location)
        {
            if (float.TryParse(WaterPhValueTextBox.Text, out waterPhValue))
            {
                location = WaterLocationTextBox.Text;
                return true;
            }
            else
            {
                // Handle the case where parsing fails, show an error message, etc.
                MessageBox.Show("Invalid pH Value. Please enter a valid numeric value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                waterPhValue = 0; // Set a default value or handle it according to your logic
                location = "";
                return false;
            }
        }

        private void DeleteWaterSensorByIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(DeleteWaterSensorIdTextBox.Text, out int sensorIdToDelete))
                {
                    using (var dbContext = new MyDbContext())
                    {
                        // Find the WaterSensor with the specified ID
                        var sensorToDelete = dbContext.WaterSensors.Find(sensorIdToDelete);

                        if (sensorToDelete != null)
                        {
                            // Remove the WaterSensor from the database
                            dbContext.WaterSensors.Remove(sensorToDelete);
                            dbContext.SaveChanges();
                            MessageBox.Show("Water Sensor deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            DisplayWaterSensors(); // Refresh the displayed Water Sensors
                        }
                        else
                        {
                            MessageBox.Show("Water Sensor with specified ID not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Water Sensor ID. Please enter a valid numeric ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Water Sensor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
