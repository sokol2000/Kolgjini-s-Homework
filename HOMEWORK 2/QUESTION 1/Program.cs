using System;
using System.Data;
using System.IO;

namespace Question_1_CSharp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Inserisci il percorso del file Excel (.xlsx):");
            string filePath = Console.ReadLine();

            if (File.Exists(filePath) && filePath.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                ProcessExcelFile(filePath);
            }
            else
            {
                Console.WriteLine("Il percorso specificato non è valido o il file non è un file Excel (.xlsx).");
            }
        }


        static void ProcessExcelFile(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();

                    if (result.Tables.Count > 0)
                    {
                        DataTable data = result.Tables[0];
                        DisplayDataInTable(data);
                        CalculateFrequency(data, "Age", "frequencyAge");
                        CalculateFrequency(data, "height", "frequencyheight");
                        CalculateFrequency(data, "Sports", "frequencySports");
                        CalculateJointDistribution(data, "Sports", "Age", "jointDistributionSportsAge");
                    }
                    else
                    {
                        Console.WriteLine("Il file Excel non contiene dati.");
                    }
                }
            }
        }


        static void DisplayDataInTable(DataTable data)
        {
            foreach (DataRow row in data.Rows)
            {
                foreach (DataColumn col in data.Columns)
                {
                    Console.Write(row[col] + "\t");
                }
                Console.WriteLine();
            }
        }


        static void CalculateFrequency(DataTable data, string variableName, string outputElementId)
        {
            var frequencies = new Dictionary<object, int>();
            var totalEntries = data.Rows.Count;

            foreach (DataRow row in data.Rows)
            {
                var value = row[variableName];

                if (frequencies.ContainsKey(value))
                {
                    frequencies[value]++;
                }
                else
                {
                    frequencies[value] = 1;
                }
            }

            Console.WriteLine("Variable: " + variableName);
            Console.WriteLine("Valore\tAbsolute frequency\tRelative frequency\tPercentage frequency");
            foreach (var kvp in frequencies)
            {
                var value = kvp.Key;
                var frequency = kvp.Value;
                var relativeFrequency = (double)frequency / totalEntries;
                var percentage = (relativeFrequency * 100).ToString("0.00");
                Console.WriteLine($"{value}\t{frequency}\t{relativeFrequency:F2}\t{percentage}%");
            }
        }


        static void CalculateJointDistribution(DataTable data, string variable1, string variable2, string outputElementId)
        {
            var jointDistribution = new Dictionary<string, int>();
            var totalEntries = data.Rows.Count;

            foreach (DataRow row in data.Rows)
            {
                var value1 = row[variable1];
                var value2 = row[variable2];
                var key = $"{value1} | {value2}";

                if (jointDistribution.ContainsKey(key))
                {
                    jointDistribution[key]++;
                }
                else
                {
                    jointDistribution[key] = 1;
                }
            }

            Console.WriteLine($"JOINT DISTRIBUTION: {variable1} and {variable2}");
            Console.WriteLine("Valore\tAbsolute frequency\tRelative frequency\tPercentage frequency");
            foreach (var kvp in jointDistribution)
            {
                var key = kvp.Key;
                var frequency = kvp.Value;
                var relativeFrequency = (double)frequency / totalEntries;
                var percentage = (relativeFrequency * 100).ToString("0.00");
                Console.WriteLine($"{key}\t{frequency}\t{relativeFrequency:F2}\t{percentage}%");
            }
        }




    }
}