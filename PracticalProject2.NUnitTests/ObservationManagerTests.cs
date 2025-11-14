/// <summary>
/// Course number: CST8002
/// Course name: Programming Language Research Project
/// Professor's name: Stanley Pieda
/// Due date: 2025-11-16
/// Author name: Kai Lu
/// </summary>

using NUnit.Framework; 
using PracticalProject2.Business;
using PracticalProject2.Model;
using System.Collections.Generic;

namespace PracticalProject2.Tests
{
    /// <summary>
    /// Unit tests for the ObservationManager class using NUnit.
    /// </summary>
    [TestFixture] 
    public class ObservationManagerTests
    {
        /// <summary>
        /// Tests the SortBySpecies method to ensure it correctly sorts records alphabetically.
        /// Project 3 Requirement: Unit-Test validating the sorting algorithm.
        /// </summary>
        [Test]
        public void SortBySpecies_ShouldSortRecordsAlphabetically()
        {
            // Arrange: Create manager and add out-of-order observations
            // Pass a dummy path to test the in-memory list sorting
            var manager = new ObservationManager("dummy_path.csv");

            var obs1 = new ForestMammalObservation { SpeciesCommonName = "Zebra" };
            var obs2 = new ForestMammalObservation { SpeciesCommonName = "Ant" };
            var obs3 = new ForestMammalObservation { SpeciesCommonName = "Bear" };

            manager.AddObservation(obs1);
            manager.AddObservation(obs2);
            manager.AddObservation(obs3);

            // Act: Perform the sort
            manager.SortBySpecies();

            // Assert: Verify the order is now Ant, Bear, Zebra
            List<ForestMammalObservation> sortedList = manager.GetAllObservations();

            Assert.That(sortedList[0].SpeciesCommonName, Is.EqualTo("Ant"), "First item should be Ant");
            Assert.That(sortedList[1].SpeciesCommonName, Is.EqualTo("Bear"), "Second item should be Bear");
            Assert.That(sortedList[2].SpeciesCommonName, Is.EqualTo("Zebra"), "Third item should be Zebra");
        }
    }
}