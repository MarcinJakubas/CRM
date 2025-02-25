using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

public class ExcelExportService
{
    public void ExportToExcel<T>(List<T> data)
    {
        if (data == null || !data.Any())
            throw new ArgumentException("Brak danych do eksportu.");

        var saveFileDialog = new SaveFileDialog
        {
            Filter = "Excel Files (*.xlsx)|*.xlsx",
            FileName = "export.xlsx" 
        };
        if (saveFileDialog.ShowDialog() == true)
        {
            var filePath = saveFileDialog.FileName;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  // lub LicenseContext.Commercial, w zależności od licencji

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Export");

                // Nagłówki kolumn (nazwy właściwości klasy)
                for (int col = 1; col <= properties.Length; col++)
                {
                    worksheet.Cells[1, col].Value = properties[col - 1].Name;
                }

                for (int row = 2; row <= data.Count + 1; row++)
                {
                    var item = data[row - 2];
                    for (int col = 1; col <= properties.Length; col++)
                    {
                        worksheet.Cells[row, col].Value = properties[col - 1].GetValue(item);
                    }
                }
                FileInfo file = new FileInfo(filePath);
                package.SaveAs(file);

                MessageBox.Show($"Dane zostały wyeksportowane do {filePath}");
            }
        }
    }
}
