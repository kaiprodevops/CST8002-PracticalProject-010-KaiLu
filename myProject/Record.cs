using System;

/// <summary>
/// Represents a single record (row) from the dataset.
/// </summary>
public class Record
{
    /// <summary>
    /// Gets or sets the unique site identification number.
    /// </summary>
    public int Site_identification { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the camera was set.
    /// </summary>
    public DateTime Camera_set_date_time { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the camera was checked.
    /// </summary>
    public DateTime Camera_check_date_time { get; set; }

    /// <summary>
    /// Gets or sets the type of lure used.
    /// </summary>
    public string Lure_type { get; set; }

    /// <summary>
    /// Gets or sets the common name of the species.
    /// </summary>
    public string Species_common_name { get; set; }

    /// <summary>
    /// Gets or sets the count of individuals observed.
    /// </summary>
    public int Count_of_individuals { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the observation.
    /// </summary>
    public DateTime Observation_date_time { get; set; }

    /// <summary>
    /// Record class constructor.
    /// </summary>
    /// <param name="siteID">The unique site identification.</param>
    /// <param name="cameraSetDT">The camera set date and time.</param>
    /// <param name="cameraCheckDT">The camera check date and time.</param>
    /// <param name="lureType">The lure type.</param>
    /// <param name="speciesName">The common name of the species.</param>
    /// <param name="individualCount">The count of individuals.</param>
    /// <param name="observationDT">The observation date and time.</param>
    public Record(int siteID, DateTime cameraSetDT, DateTime cameraCheckDT, string lureType, string speciesName, int individualCount, DateTime observationDT)
    {
        Site_identification = siteID;
        Camera_set_date_time = cameraSetDT;
        Camera_check_date_time = cameraCheckDT;
        Lure_type = lureType;
        Species_common_name = speciesName;
        Count_of_individuals = individualCount;
        Observation_date_time = observationDT;
    }
}
