using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using KursovaTRPZ.Models;

namespace KursovaTRPZ.Windows.SensorWindows
{
    public partial class SoilSensorWindow : Window
    {
        private readonly int Id;

        public SoilSensorWindow(int id)
        {
            InitializeComponent();
            Id = id;
            DisplaySoilSensors();
        }

        private void DisplaySoilSensors()
        {
            try
            {
                using (var dbContext = new MyDbContext())
                {
                    var soilSensors = new ObservableCollection<SoilSensor>(dbContext.SoilSensors.Where(el => el.Engineer.UserId == Id).ToList());
                    SoilSensorDataGrid.ItemsSource = soilSensors;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching Soil Sensors: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateSoilSensor_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetSensorValues(out float pHValue, out float humidityValue, out string location))
            {
                try
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var authenticatedUser = dbContext.Users.FirstOrDefault(user => user.UserId == Id);
                        if (authenticatedUser is Engineer engineer)
                        {
                            var newSensor = new SoilSensor
                            {
                                Engineer = engineer,
                                Ph_Value = pHValue,
                                Humidity_Value = humidityValue,
                                Sensor_Location = location
                            };
                            dbContext.SoilSensors.Add(newSensor);
                            dbContext.SaveChanges();
                            DisplaySoilSensors(); 
                            InfoTextBlock.Text = $"New Sensor created: pH Value = {pHValue}, Humidity Value = {humidityValue}, Location = {location}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding Soil Sensor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool TryGetSensorValues(out float pHValue, out float humidityValue, out string location)
        {
            if (float.TryParse(PhValueTextBox.Text, out pHValue) && float.TryParse(HumidityValueTextBox.Text, out humidityValue))
            {
                location = SoilLocationTextBox.Text;
                return true;
            }
            else
            {
                MessageBox.Show("Invalid pH or Humidity Value. Please enter valid numeric values.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                pHValue = 0; 
                humidityValue = 0; 
                location = "";
                return false;
            }
        }

        private void DeleteSoilSensorByIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(DeleteSoilSensorIdTextBox.Text, out int sensorIdToDelete))
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var sensorToDelete = dbContext.SoilSensors.Find(sensorIdToDelete);

                        if (sensorToDelete != null)
                        {
                            dbContext.SoilSensors.Remove(sensorToDelete);
                            dbContext.SaveChanges();
                            MessageBox.Show("Soil Sensor deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            DisplaySoilSensors(); 
                        }
                        else
                        {
                            MessageBox.Show("Soil Sensor with specified ID not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Soil Sensor ID. Please enter a valid numeric ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Soil Sensor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
