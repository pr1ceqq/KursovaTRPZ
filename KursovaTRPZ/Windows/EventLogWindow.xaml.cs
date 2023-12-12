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

        public EventLogsWindow(int adminId)
        {
            InitializeComponent();
            this.adminId = adminId;

            // Fetch and display EventLogs associated with the adminId
            DisplayEventLogs();
        }

        private void DisplayEventLogs()
        {
            try
            {
                using (var dbContext = new MyDbContext())
                {
                    // Query the EventLogs associated with the specified adminId
                    var eventLogs = new ObservableCollection<EventLog>(dbContext.EventLogs.Where(el => el.AdminNavigation.UserId == adminId).ToList());
                    // Set the DataGrid's ItemsSource to the list of EventLogs
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
                // Get values from the input fields
                if (TryGetEventLogValues(out string eventName, out DateTime eventTime, out int? SensorId))
                {
                    using (var dbContext = new MyDbContext())
                    {
                        // Fetch the authenticated user
                        var authenticatedUser = dbContext.Users.FirstOrDefault(user => user.UserId == adminId);
        
                        if (IsAuthorized(authenticatedUser))
                        {
                            // It's an Administrator, allow adding EventLog
                            var newEventLog = new EventLog
                            {
                                EventName = eventName,
                                EventTime = eventTime,
                                AdminNavigation = (Administrator)authenticatedUser,
                                Sensor_ID = SensorId
                            };
        
                            // Add the new EventLog to the database
                            dbContext.EventLogs.Add(newEventLog);
                            dbContext.SaveChanges();
                            MessageBox.Show("Event Log added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            // Handle unauthorized access
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
            // Initialize output variables
            eventName = EventNameTextBox.Text;
            eventTime = EventTimeDatePicker.SelectedDate ?? DateTime.Now; // Default to current date if not selected
            SensorId = TryParseSensorId(WeatherSensorIdTextBox.Text);
        
            // Add validation logic if needed
        
            return true; // Assuming the values are always valid for now
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

    // Handle invalid input
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
                        // Find the EventLog with the specified ID
                        var eventLogToDelete = dbContext.EventLogs.Find(eventIdToDelete);

                        if (eventLogToDelete != null)
                        {
                            // Remove the EventLog from the database
                            dbContext.EventLogs.Remove(eventLogToDelete);
                            dbContext.SaveChanges();
                            MessageBox.Show("Event Log deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            DisplayEventLogs(); // Refresh the displayed EventLogs
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