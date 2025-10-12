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
    class Program
    {
        // Set the path to the data file.
        private const string DataFilePath = @"PracticalProject2/data/data.csv";

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