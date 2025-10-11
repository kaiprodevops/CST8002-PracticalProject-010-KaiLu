/// <summary>
/// Course number: CST8002
/// Course name: Programming Language Research Project
/// Professor's name: Stanley Pieda
/// Due date: 2025-09-21
/// Author name: Kai Lu
/// </summary>

using PracticalProject2.Business;
using PracticalProject2.Presentation;

namespace PracticalProject2
{
    class Program
    {
        // Set the path to the data file.
        private const string DataFilePath = @"Data/data.csv";

        static void Main(string[] args)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFilePath);

            ObservationManager manager = new ObservationManager(fullPath);
            manager.LoadInitialData(); // Load data on startup [cite: 11]

            ConsoleUI ui = new ConsoleUI(manager);
            ui.Run();
        }
    }
}