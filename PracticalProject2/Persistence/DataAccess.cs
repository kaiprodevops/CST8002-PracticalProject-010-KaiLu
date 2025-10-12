using PracticalProject2.Model;
using System.Globalization;

namespace PracticalProject2.Persistence
{
    /// <summary>
    /// Handles data access operations, including reading from and writing to files.
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// Loads mammal observation records from the specified CSV file.
        /// Reads the first 100 records, skipping the header lines.
        /// </summary>
        /// <param name="filePath">The path to the dataset file.</param>
        /// <returns>A list of ForestMammalObservation objects.</returns>
        public List<ForestMammalObservation> LoadObservations(string filePath)
        {
            var observations = new List<ForestMammalObservation>();
            // Using exception handling for file operations
            try
            {
                var lines = File.ReadAllLines(filePath);

                // Skip the first two header lines and take up to 100 records
                foreach (var line in lines.Skip(2).Take(100))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    var columns = line.Split(','); // The dataset is comma-separated
                    if (columns.Length < 7) continue;

                    try
                    {
                        var observation = new ForestMammalObservation
                        {
                            SiteId = int.Parse(columns[0]),
                            CameraSetDateTime = DateTime.ParseExact(columns[1], "d/M/yyyy H:mm", CultureInfo.InvariantCulture),
                            CameraCheckDateTime = DateTime.ParseExact(columns[2], "d/M/yyyy H:mm", CultureInfo.InvariantCulture),
                            LureType = columns[3],
                            SpeciesCommonName = columns[4],
                            IndividualCount = int.Parse(columns[5]),
                            ObservationDateTime = DateTime.ParseExact(columns[6], "d/M/yyyy H:mm", CultureInfo.InvariantCulture)
                        };
                        observations.Add(observation);
                    }
                    catch (FormatException ex)
                    {
                        // Log or handle records that cannot be parsed
                        Console.WriteLine($"Skipping malformed record: {line}. Error: {ex.Message}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Error: The file was not found at {filePath}.");
                throw; // Re-throw to be handled by the caller
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred while reading the file: {ex.Message}");
                throw;
            }

            return observations;
        }

        /// <summary>
        /// Persist the data from memory to the disk as a comma-separated file, writing to a new file.
        /// The filename is a globally unique identifier (GUID).
        /// </summary>
        /// <param name="observations">The list of observations to save.</param>
        /// <returns>The generated file path of the saved file.</returns>
        public string SaveObservations(List<ForestMammalObservation> observations)
        {
            // Generate a unique filename using a GUID
            string fileName = $"{Guid.NewGuid()}.csv";
            string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

            try
            {
                using (var writer = new StreamWriter(filePath))
                {
                    // Write header
                    writer.WriteLine("SiteId,CameraSetDateTime,CameraCheckDateTime,LureType,SpeciesCommonName,IndividualCount,ObservationDateTime");

                    // Write records
                    foreach (var obs in observations)
                    {
                        writer.WriteLine($"{obs.SiteId}," +
                                         $"{obs.CameraSetDateTime:o}," + // ISO 8601 format for consistency
                                         $"{obs.CameraCheckDateTime:o}," +
                                         $"\"{obs.LureType}\"," +
                                         $"\"{obs.SpeciesCommonName}\"," +
                                         $"{obs.IndividualCount}," +
                                         $"{obs.ObservationDateTime:o}");
                    }
                }
                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
                return string.Empty;
            }
        }
    }
}