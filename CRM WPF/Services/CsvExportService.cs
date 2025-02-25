using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

public class CsvExportService
{
    public void ExportToCsv<T>(List<T> data)
    {
        if (data == null || !data.Any())
            throw new ArgumentException("Brak danych do eksportu.");

        var saveFileDialog = new SaveFileDialog
        {
            Filter = "CSV Files (*.csv)|*.csv",
            FileName = "export.csv" 
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            var filePath = saveFileDialog.FileName;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var sb = new StringBuilder();

            // Nagłówki kolumn (nazwy właściwości klasy)
            sb.AppendLine(string.Join(",", properties.Select(p => p.Name)));

            foreach (var item in data)
            {
                var values = properties.Select(p => p.GetValue(item)?.ToString() ?? string.Empty);
                sb.AppendLine(string.Join(",", values));
            }

            File.WriteAllText(filePath, sb.ToString());

            MessageBox.Show($"Dane zostały wyeksportowane do {filePath}");
        }
    }
}
