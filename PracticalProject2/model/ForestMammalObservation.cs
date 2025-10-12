namespace PracticalProject2.Model
{
    /// <summary>
    /// Represents a single observation record of a forest mammal.
    /// This class is a data-transfer object used across layers.
    /// </summary>
    public class ForestMammalObservation
    {
        public int SiteId { get; set; }
        public DateTime CameraSetDateTime { get; set; }
        public DateTime CameraCheckDateTime { get; set; }
        public string? LureType { get; set; }
        public string? SpeciesCommonName { get; set; }
        public int IndividualCount { get; set; }
        public DateTime ObservationDateTime { get; set; }

        /// <summary>
        /// Provides a string representation of the observation, formatted for display.
        /// </summary>
        /// <returns>A formatted string with observation details.</returns>
        public override string ToString()
        {
            return $"Site ID: {SiteId}, " +
                   $"Species: {SpeciesCommonName}, " +
                   $"Count: {IndividualCount}, " +
                   $"Observation Time: {ObservationDateTime:yyyy-MM-dd HH:mm}, " +
                   $"Lure type: {LureType}, " +
                   $"Camera Set: { CameraSetDateTime: yyyy - MM - dd HH: mm}, " +
                   $"Camera Check: { CameraCheckDateTime: yyyy - MM - dd HH: mm}\n";
        }
    }
}