using CRM_WPF.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CRM_WPF.Views
{
    public partial class CalendarView : UserControl
    {
        private List<CalendarEvent> events;

        public CalendarView()
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents()
        {
            // Pobieranie przykładowych wydarzeń
            events = CalendarEvent.GetSampleEvents();

            foreach (var calendarEvent in events)
            {
                AddEventToCalendar(calendarEvent);
            }
        }

        private void AddEventToCalendar(CalendarEvent calendarEvent)
        {
            Border eventBorder = new Border
            {
                Background = calendarEvent.EventColor,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Padding = new Thickness(5),
                CornerRadius = new CornerRadius(3),
                Margin = new Thickness(2)
            };

            TextBlock eventText = new TextBlock
            {
                Text = $"{calendarEvent.Title}\n{calendarEvent.StartTime:HH:mm} - {calendarEvent.EndTime:HH:mm}",
                TextWrapping = TextWrapping.Wrap,
                Foreground = Brushes.Black,
                FontSize = 12
            };

            eventBorder.Child = eventText;

            // Wyliczenie wiersza dla odpowiedniej godziny (np. 6:00 = 1, 7:00 = 2)
            int row = calendarEvent.StartTime.Hour - 6;
            if (row < 1 || row > 13) return; // Nie dodajemy eventów poza zakresem 6:00 - 18:00

            Grid.SetColumn(eventBorder, calendarEvent.DayOfWeek + 1);
            Grid.SetRow(eventBorder, row);
            MainGrid.Children.Add(eventBorder);
        }

        private void PrevWeek_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Przełączono na poprzedni tydzień.", "Nawigacja");
        }

        private void NextWeek_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Przełączono na następny tydzień.", "Nawigacja");
        }
    }
}