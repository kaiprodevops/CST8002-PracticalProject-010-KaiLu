/// <summary>
/// Course number: CST8002
/// Course name: Programming Language Research Project
/// Professor's name: Stanley Pieda
/// Due date: 2025-10-12
/// Author name: Kai Lu
/// </summary>

using PracticalProject2.Business;
using PracticalProject2.Presentation;

namespace PracticalProject2
{
    /// <summary>
    /// The main entry point class for the console application.
    /// Responsible for initializing and wiring up the application layers.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the relative path to the dataset file.
        /// The path is relative to the application's execution directory 
        /// </summary>
        private const string DataFilePath = @"PracticalProject2/data/data.csv";

        /// <summary>
        /// The main entry point for the application.
        /// This method initializes the business and presentation layers, triggers the initial data load,
        /// and starts the main user interface loop.
        /// </summary>
        /// <param name="args">Command-line arguments passed to the application (not used).</param>
        static void Main(string[] args)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataFilePath);

            ObservationManager manager = new ObservationManager(fullPath);
            manager.LoadInitialData(); // Load data on startup [cite: 11]
            Console.WriteLine("\n--- Data loading complete. Press any key to see the menu... ---"); // 添加一句提示
            Console.ReadKey(); 
            ConsoleUI ui = new ConsoleUI(manager);
            ui.Run();
        }
    }
}