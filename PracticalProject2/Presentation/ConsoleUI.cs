/// <summary>
/// Course number: CST8002
/// Course name: Programming Language Research Project
/// Professor's name: Stanley Pieda
/// Due date: 2025-11-30
/// Author name: Kai Lu
/// </summary>
/// 

using PracticalProject2.Business;
using PracticalProject2.Model;
using Spectre.Console;

namespace PracticalProject2.Presentation
{
    /// <summary>
    /// Handles the console-based user interface and user interactions.
    /// </summary>
    public class ConsoleUI
    {
        private readonly ObservationManager _manager;

        /// <summary>
        /// Initializes a new instance of the ConsoleUI class.
        /// </summary>
        /// <param name="manager">The business logic manager.</param>
        public ConsoleUI(ObservationManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Runs the main application loop, displaying the menu and handling user input.
        /// </summary>
        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                Console.Write("Enter your choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllRecords();
                        break;
                    case "2":
                        DisplaySingleRecord();
                        break;
                    case "3":
                        CreateNewRecord();
                        break;
                    case "4":
                        EditRecord();
                        break;
                    case "5":
                        DeleteRecord();
                        break;
                    case "6":
                        _manager.ReloadData();
                        break;
                    // Persist the data from memory to the disk as a comma-separated file, writing to a new file.check ObservationManager.PersistData()
                    case "7":
                        _manager.PersistData();
                        break;
                    
                    case "8": // Project 3 Requirement: Option to use functionality
                        SortRecords();
                        break;
                    case "9": // Project 4 Requirement: New Feature
                        VisualizeData();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Displays the main menu and programmer's name.
        /// </summary>
        private void DisplayMenu()
        {
            Console.Clear();
            // Using Spectre.Console for a nicer header
            AnsiConsole.Write(
                new Rule("[yellow]Forest Mammal Observation Program[/]")
                .RuleStyle("grey"));
            Console.WriteLine("=================================================");
            Console.WriteLine(" Forest Mammal Observation Program by Kai Lu ");
            Console.WriteLine("=================================================");
            Console.WriteLine("1. Display All Records");
            Console.WriteLine("2. Display a Single Record");
            Console.WriteLine("3. Create a New Record");
            Console.WriteLine("4. Edit a Record");
            Console.WriteLine("5. Delete a Record");
            Console.WriteLine("6. Reload All Data from File");
            Console.WriteLine("7. Save All Data to New File");
            Console.WriteLine("8. Sort Records by Species (Project 3)");
            AnsiConsole.MarkupLine("[bold cyan]9. Visualize Data (Project 4)[/]"); // Highlight new feature
            Console.WriteLine("0. Exit");
            Console.WriteLine("-------------------------------------------------");
        }

        /// <summary>
        /// Project 4 Feature: Interactive Data Visualization.
        /// Allows the user to filter data at runtime and views it as a Horizontal Bar Chart.
        /// </summary>
        private void VisualizeData()
        {
            Console.Clear();
            AnsiConsole.Write(new Rule("[cyan]Visualize Species Data[/]"));

            // 1. User Interaction (Run-time customization)
            // Use simple Console.ReadLine here to keep interaction consistent
            Console.WriteLine("You can filter the chart by year.");
            Console.Write("Enter a year (e.g. 2014) or press Enter to visualize ALL years: ");
            string? input = Console.ReadLine();

            int? selectedYear = null;
            if (int.TryParse(input, out int parsedYear))
            {
                selectedYear = parsedYear;
            }

            // 2. Data Retrieval (Business Layer)
            var data = _manager.GetSpeciesObservationCounts(selectedYear);

            if (data.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No observations found for the selected criteria.[/]");
                return;
            }

            // 3. Visualization (Presentation Layer - Spectre.Console)
            // Modern Best Practice: Use a specific rendering object for clean code
            var chart = new BarChart()
                .Width(60)
                .Label($"[green bold underline]Species Observations ({(selectedYear.HasValue ? selectedYear.ToString() : "All Time")})[/]")
                .CenterLabel();

            // Add data to chart
            foreach (var item in data)
            {
                // We can dynamically assign colors or use a static one
                chart.AddItem(item.Key, item.Value, Color.Yellow);
            }

            // Render
            AnsiConsole.WriteLine();
            AnsiConsole.Write(chart);
            AnsiConsole.WriteLine();
        }


        /// <summary>
        /// Handles the logic to display all records, with pagination
        /// </summary>
        private void DisplayAllRecords()
        {
            var records = _manager.GetAllObservations();
            if (records.Count == 0)
            {
                Console.WriteLine("No records to display.");
                return;
            }

            for (int i = 0; i < records.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {records[i]}");
                // Display programmer's name every 10 records as per example 
                if ((i + 1) % 10 == 0)
                {
                    Console.WriteLine("--- Page Break --- Program by Kai Lu ---");
                }
            }
        }

        /// <summary>
        /// Project 3: Handles sorting functionality.
        /// </summary>
        private void SortRecords()
        {
            // Here I default to Species per requirements for "single column".
            Console.WriteLine("Sorting records by Species Common Name...");
            _manager.SortBySpecies();
        }

        /// <summary>
        /// Handles displaying a single record selected by the user
        /// use the index to access the list
        /// </summary>
        private void DisplaySingleRecord()
        {
            Console.Write("Enter the record number(using list index) to display: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _manager.GetAllObservations().Count)
            {
                Console.WriteLine(_manager.GetAllObservations()[index - 1]);
            }
            else
            {
                Console.WriteLine("Invalid record number.");
            }
        }

        /// <summary>
        /// Guides the user through creating a new observation record.
        /// check ObservationManager.AddObservation()
        /// </summary>
        private void CreateNewRecord()
        {
            try
            {
                Console.WriteLine("--- Create New Record ---");
                var newObs = new ForestMammalObservation();

                Console.Write("Enter Site ID: ");
                newObs.SiteId = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter Species Common Name: ");
                newObs.SpeciesCommonName = Console.ReadLine();

                Console.Write("Enter Count of Individuals: ");
                newObs.IndividualCount = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter Lure Type: ");
                newObs.LureType = Console.ReadLine();

                // For simplicity, using current time for date fields
                newObs.ObservationDateTime = DateTime.Now;
                newObs.CameraSetDateTime = DateTime.Now;
                newObs.CameraCheckDateTime = DateTime.Now;

                _manager.AddObservation(newObs);
                Console.WriteLine("Record created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating record: {ex.Message}");
            }
        }

        /// <summary>
        /// Guides the user through editing an existing record.
        /// relate to ObservationManager.UpdateObservation()
        /// </summary>
        private void EditRecord()
        {
            Console.Write("Enter the record number to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index <= 0 || index > _manager.GetAllObservations().Count)
            {
                Console.WriteLine("Invalid record number.");
                return;
            }
            int recordIndex = index - 1;

            var existingObs = _manager.GetAllObservations()[recordIndex];
            Console.WriteLine($"Editing Record: {existingObs}");

            try
            {
                Console.Write($"Enter new Species Common Name ({existingObs.SpeciesCommonName}): ");
                string? species = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(species)) existingObs.SpeciesCommonName = species;

                Console.Write($"Enter new Count of Individuals ({existingObs.IndividualCount}): ");
                string? countStr = Console.ReadLine();
                if (int.TryParse(countStr, out int count)) existingObs.IndividualCount = count;

                if (_manager.UpdateObservation(recordIndex, existingObs))
                {
                    Console.WriteLine("Record updated successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to update record.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating record: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a record selected by the user.
        /// relate to ObservationManager.DeleteObservation()
        /// </summary>
        private void DeleteRecord()
        {
            Console.Write("Enter the record number to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _manager.GetAllObservations().Count)
            {
                if (_manager.DeleteObservation(index - 1))
                {
                    Console.WriteLine("Record deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to delete record.");
                }
            }
            else
            {
                Console.WriteLine("Invalid record number.");
            }
        }
    }
}