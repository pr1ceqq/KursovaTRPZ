using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using KursovaTRPZ.Models;

namespace KursovaTRPZ.Windows.SensorWindows
{
    public partial class RadiationSensorWindow : Window
    {
        private readonly int Id;

        public RadiationSensorWindow(int id)
        {
            InitializeComponent();
            Id = id;
            DisplayRadiationSensors();
        }

        private void DisplayRadiationSensors()
        {
            try
            {
                using (var dbContext = new MyDbContext())
                {
                    var RadiationSensors = new ObservableCollection<RadiationSensor>(dbContext.RadiationSensors.Where(el => el.Engineer.UserId == Id).ToList());
                    RadiationSensorDataGrid.ItemsSource = RadiationSensors;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching Radiation Sensors: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateSensor_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetSensorValues(out float RadiationValue, out string location))
            {
                try
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var authenticatedUser = dbContext.Users.FirstOrDefault(user => user.UserId == Id);
                        if (authenticatedUser is Engineer engineer)
                        {
                            var newSensor = new RadiationSensor
                            {
                                Engineer = engineer,
                                Radiation_Value = RadiationValue,
                                Sensor_Location = location
                            };
                            dbContext.RadiationSensors.Add(newSensor);
                            dbContext.SaveChanges();
                            DisplayRadiationSensors(); 
                            InfoTextBlock.Text = $"New Sensor created: Radiation Value = {RadiationValue}, Location = {location}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding Radiation Sensor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool TryGetSensorValues(out float RadiationValue, out string location)
        {
            if (float.TryParse(RadiationValueTextBox.Text, out RadiationValue))
            {
                location = LocationTextBox.Text;
                return true;
            }
            else
            {
                MessageBox.Show("Invalid Radiation Value. Please enter a valid numeric value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                RadiationValue = 0; 
                location = "";
                return false;
            }
        }
        private void DeleteRadiationSensorByIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(DeleteRadiationSensorIdTextBox.Text, out int sensorIdToDelete))
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var sensorToDelete = dbContext.RadiationSensors.Find(sensorIdToDelete);

                        if (sensorToDelete != null)
                        {
                            dbContext.RadiationSensors.Remove(sensorToDelete);
                            dbContext.SaveChanges();
                            MessageBox.Show("Radiation Sensor deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            DisplayRadiationSensors(); 
                        }
                        else
                        {
                            MessageBox.Show("Radiation Sensor with specified ID not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Radiation Sensor ID. Please enter a valid numeric ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Radiation Sensor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
