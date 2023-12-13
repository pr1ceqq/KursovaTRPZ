using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using KursovaTRPZ.Models;

namespace KursovaTRPZ
{
    public partial class EventLogsWindow : Window
    {
        private readonly int adminId;
        private ObservableCollection<EventLog> eventLogs;
        public EventLogsWindow(int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;

            DisplayEventLogs();
        }

        private void DisplayEventLogs()
        {
            try
            {
                using (var dbContext = new MyDbContext())
                {
                    var eventLogs = new ObservableCollection<EventLog>(dbContext.EventLogs.Where(el => el.AdminNavigation.UserId == adminId).ToList());
                    EventLogsDataGrid.ItemsSource = eventLogs;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching EventLogs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddEventLogButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TryGetEventLogValues(out string eventName, out DateTime eventTime, out int? SensorId))
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var authenticatedUser = dbContext.Users.FirstOrDefault(user => user.UserId == adminId);
                        var sensor = dbContext.Sensors.Find(SensorId);
                        var EventContext = "";
                        if (sensor != null)
                        {
                            if (sensor is SoilSensor soilSensor)
                            {
                                EventContext = $"Soil Sensor - pH Value: {soilSensor.Ph_Value}, Humidity Value: {soilSensor.Humidity_Value}, Location: {soilSensor.Sensor_Location}";
                            }
                            else if (sensor is WaterSensor waterSensor)
                            {
                                EventContext = $"Water Sensor - pH Value: {waterSensor.Ph_Value}, Location: {waterSensor.Sensor_Location}";
                            }
                            else if (sensor is RadiationSensor radiationSensor)
                            {
                                EventContext = $"Radiation Sensor - Radiation Value: {radiationSensor.Radiation_Value}, Location: {radiationSensor.Sensor_Location}";
                            }
                            else if (sensor is MotionSensor motionSensor)
                            {
                                EventContext = $"Motion Sensor - Motion Value: {motionSensor.MotionSensor_Value}, Location: {motionSensor.Sensor_Location}";
                            }
                        }
                        if (IsAuthorized(authenticatedUser))
                        {
                            var newEventLog = new EventLog
                            {
                                EventName = eventName,
                                EventTime = eventTime,
                                AdminNavigation = (Administrator)authenticatedUser,
                                Sensor_ID = SensorId,
                                Event_Context = EventContext
                            };
        
                            dbContext.EventLogs.Add(newEventLog);
                            dbContext.SaveChanges();
                            MessageBox.Show("Event Log added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Authentication failed or unauthorized access", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding Event Log: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private bool TryGetEventLogValues(out string eventName, out DateTime eventTime, out int? SensorId)
        {
            eventName = EventNameTextBox.Text;
            eventTime = EventTimeDatePicker.SelectedDate ?? DateTime.Now; 
            SensorId = TryParseSensorId(WeatherSensorIdTextBox.Text);
            
        
            return true; 
        }

private int? TryParseSensorId(string input)
{
    if (string.IsNullOrWhiteSpace(input))
    {
        return null;
    }

    if (int.TryParse(input, out int result))
    {
        return result;
    }

    MessageBox.Show($"Invalid sensor ID: {input}. Please enter a valid numeric ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    return null;
}

private bool IsAuthorized(User authenticatedUser)
{
    return authenticatedUser != null && authenticatedUser is Administrator;
}

        private void DeleteEventLogByIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(DeleteEventLogIdTextBox.Text, out int eventIdToDelete))
                {
                    using (var dbContext = new MyDbContext())
                    {
                        var eventLogToDelete = dbContext.EventLogs.Find(eventIdToDelete);

                        if (eventLogToDelete != null)
                        {
                            dbContext.EventLogs.Remove(eventLogToDelete);
                            dbContext.SaveChanges();
                            MessageBox.Show("Event Log deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            DisplayEventLogs(); 
                        }
                        else
                        {
                            MessageBox.Show("Event Log with specified ID not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Event Log ID. Please enter a valid numeric ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Event Log: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}