using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace CRM_WPF.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; } // Unikalny identyfikator wydarzenia
        public int CustomerId { get; set; } // ID Klienta
        public Customer Customer { get; set; } // Relacja do Klienta
        public string Title { get; set; } // Krótki tytuł wydarzenia
        public string Description { get; set; } // Dodatkowy opis wydarzenia
        public DateTime StartTime { get; set; } // Data i godzina rozpoczęcia
        public DateTime EndTime { get; set; } // Data i godzina zakończenia
        public int DayOfWeek { get; set; } // Numer dnia tygodnia (0 = poniedziałek, 6 = niedziela)
        public SolidColorBrush EventColor { get; set; } // Kolor wydarzenia (np. kolor w kalendarzu)

        public CalendarEvent(int id, string title, string description, DateTime start, DateTime end, int dayOfWeek, SolidColorBrush color)
        {
            Id = id;
            Title = title;
            Description = description;
            StartTime = start;
            EndTime = end;
            DayOfWeek = dayOfWeek;
            EventColor = color;
        }

        public override string ToString()
        {
            return $"{Title} ({StartTime:HH:mm} - {EndTime:HH:mm})";
        }

        public static List<CalendarEvent> GetSampleEvents()
        {
            return new List<CalendarEvent>
            {
                new CalendarEvent(1, "Spotkanie z klientem A", "Omówienie szczegółów współpracy",
                    new DateTime(2024, 3, 4, 10, 0, 0), new DateTime(2024, 3, 4, 11, 0, 0), 1, Brushes.LightBlue),

                new CalendarEvent(2, "Prezentacja produktu", "Nowy produkt dla inwestorów",
                    new DateTime(2024, 3, 5, 9, 0, 0), new DateTime(2024, 3, 5, 10, 30, 0), 2, Brushes.LightGreen),

                new CalendarEvent(3, "Telefon do dostawcy", "Uzgodnienie terminów dostaw",
                    new DateTime(2024, 3, 6, 14, 30, 0), new DateTime(2024, 3, 6, 15, 0, 0), 3, Brushes.LightCoral),

                new CalendarEvent(4, "Raport miesięczny", "Podsumowanie wyników sprzedaży",
                    new DateTime(2024, 3, 7, 16, 0, 0), new DateTime(2024, 3, 7, 17, 30, 0), 4, Brushes.LightGoldenrodYellow),

                new CalendarEvent(5, "Szkolenie zespołu", "Nowe narzędzia sprzedażowe",
                    new DateTime(2024, 3, 8, 13, 0, 0), new DateTime(2024, 3, 8, 15, 0, 0), 5, Brushes.LightSalmon)
            };
        }
    }
}
