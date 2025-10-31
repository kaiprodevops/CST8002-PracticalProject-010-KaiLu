/// <summary>
/// Course number: CST8002
/// Course name: Programming Language Research Project
/// Professor's name: Stanley Pieda
/// Due date: 2025-10-12
/// Author name: Kai Lu
/// </summary>
using PracticalProject2.Model;
using PracticalProject2.Persistence;

namespace PracticalProject2.Business
{
    /// <summary>
    /// Manages the business logic and data for mammal observations.
    /// Acts as an intermediary between the Presentation and Persistence layers.
    /// </summary>
    public class ObservationManager
    {
        private List<ForestMammalObservation> _observations;
        private readonly DataAccess _dataAccess;
        private readonly string _filePath;

        /// <summary>
        /// Initializes a new instance of the ObservationManager class.
        /// </summary>
        /// <param name="filePath">The path to the dataset file.</param>
        public ObservationManager(string filePath)
        {
            _dataAccess = new DataAccess();
            _observations = new List<ForestMammalObservation>();
            _filePath = filePath;
        }

        /// <summary>
        /// Loads the initial set of data from the source file.
        /// </summary>
        public void LoadInitialData()
        {
            try
            {
                _observations = _dataAccess.LoadObservations(_filePath);
                Console.WriteLine($"{_observations.Count} records loaded successfully.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load initial data. The application may not function correctly.");
            }
        }

        /// <summary>
        /// Reloads data from the source file, replacing the current in-memory data
        /// To give the user a fresh restart without restarting the application
        /// </summary>
        public void ReloadData()
        {
            Console.WriteLine("Reloading data...");
            LoadInitialData();
        }

        /// <summary>
        /// Retrieves all current observations.
        /// </summary>
        /// <returns>A list of all observations.</returns>
        public List<ForestMammalObservation> GetAllObservations()
        {
            return _observations;
        }

        /// <summary>
        /// Adds a new observation to the ***in-memory*** list. 
        /// For create a new record functionality
        /// </summary>
        /// <param name="observation">The new observation to add.</param>
        public void AddObservation(ForestMammalObservation observation)
        {
            _observations.Add(observation);
        }

        /// <summary>
        /// Updates an existing observation at a specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the observation to update.</param>
        /// <param name="updatedObservation">The observation with updated information.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        public bool UpdateObservation(int index, ForestMammalObservation updatedObservation)
        {
            if (index >= 0 && index < _observations.Count)
            {
                _observations[index] = updatedObservation;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes an observation from the in-memory list at a specified index.
        /// relate to Presentation.ConsoleUI.DeleteRecord()
        /// </summary>
        /// <param name="index">The zero-based index of the observation to delete.</param>
        /// <returns>True if deletion was successful, otherwise false.</returns>
        public bool DeleteObservation(int index)
        {
            if (index >= 0 && index < _observations.Count)
            {
                _observations.RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Persists the current in-memory data to a new file. check DataAccess.SaveObservations for details.
        /// </summary>
        public void PersistData()
        {
            string savedFilePath = _dataAccess.SaveObservations(_observations);
            if (!string.IsNullOrEmpty(savedFilePath))
            {
                Console.WriteLine($"Data successfully persisted to: {savedFilePath}");
            }
            else
            {
                Console.WriteLine("Failed to persist data.");
            }
        }
    }
}