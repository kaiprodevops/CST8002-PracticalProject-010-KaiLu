/// <summary>
/// Course number: CST8002
/// Course name: Programming Language Research Project
/// Professor's name: Stanley Pieda
/// Due date: 2025-09-21
/// Author name: Kai Lu
/// </summary>

using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;

/// <summary>
/// The main program class that handles file I/O, data parsing, and console output.
/// </summary>
public class Program
{
    /// <summary>
    /// The name of the dataset file to be read.
    /// </summary>
    private const string DATASET_FILE_NAME = "dataset.csv";

    /// <summary>
    /// The maximum number of records to load from the dataset.
    /// </summary>
    private const int MAX_RECORDS_TO_LOAD = 3;

    /// <summary>
    /// The main entry point for the application.
    /// This method performs File-IO, handles exceptions, parses data,
    /// and displays the processed records to the console.
    /// </summary>
    public static void Main()
    {
        List<Record> records = new List<Record>();
        string[] dateFormat = { "dd/MM/yyyy HH:mm", "dd/MM/yyyy H:mm" };

        try
        {
            // Use a StreamReader to open and read the dataset file.
            using (StreamReader sr = new StreamReader(DATASET_FILE_NAME))
            {
                // Skip the first two header rows (English and French column names).
                sr.ReadLine();
                sr.ReadLine();

                string line;
                int count = 0;

                while ((line = sr.ReadLine()) != null && count < MAX_RECORDS_TO_LOAD)
                {
                    string[] parts = line.Split(',');

                    // Ensure the line has the correct number of fields.
                    if (parts.Length == 7)
                    {
                        // Create a new Record object with data parsed from the CSV.
                        records.Add(new Record(
                            int.Parse(parts[0]),
                            DateTime.ParseExact(parts[1], dateFormat, CultureInfo.InvariantCulture),
                            DateTime.ParseExact(parts[2], dateFormat, CultureInfo.InvariantCulture),
                            parts[3],
                            parts[4],
                            int.Parse(parts[5]),
                            DateTime.ParseExact(parts[6], dateFormat, CultureInfo.InvariantCulture)
                        ));
                        count++;
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            // Gracefully exit the program if the file is not found.
            Console.WriteLine("Error: The dataset file was not found.");
            Console.WriteLine("Program by: Kai Lu");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            return;
        }
        catch (Exception ex)
        {
            // Catch any other unexpected errors during file reading or parsing.
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            Console.WriteLine("Program by: Kai Lu");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            return;
        }

        // Loop over the data structure and display the record data.
        foreach (var record in records)
        {
            Console.WriteLine($"Site ID: {record.Site_identification}");
            Console.WriteLine($"  Camera Set: {record.Camera_set_date_time}");
            Console.WriteLine($"  Camera Check: {record.Camera_check_date_time}");
            Console.WriteLine($"  Lure Type: {record.Lure_type}");
            Console.WriteLine($"  Species: {record.Species_common_name}");
            Console.WriteLine($"  Count: {record.Count_of_individuals}");
            Console.WriteLine($"  Observation: {record.Observation_date_time}\n");
        }

        // Display my full name on screen.
        Console.WriteLine("------------------------------------------");
        Console.WriteLine("Program by: Kai Lu");
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();

    }
}