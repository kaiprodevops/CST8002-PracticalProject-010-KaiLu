/// <summary>
/// Course number: CST8002
/// Course name: Programming Language Research Project
/// Professor's name: Stanley Pieda
/// Due date: 2025-10-12
/// Author name: Kai Lu
/// </summary>
/// 

using PracticalProject2.Persistence;

namespace PracticalProject2.NUnitTests
{
    /// <summary>
    /// Contains NUnit unit tests for the DataAccess class.
    /// This test fixture focuses on verifying the data loading and parsing functionality.
    /// </summary>
    [TestFixture]
    public class DataAccessNUnitTests
    {
        private readonly string _testFileName = "test-data.csv";

        /// <summary>
        /// Verifies that the LoadObservations method correctly loads 100 records from a full data file
        /// and that all fields of the first record are parsed into the object's properties with the correct values.
        /// This test dynamically resolves the path to 'test-data.csv' to ensure it is portable and can run on any machine.
        /// </summary>
        [Test]
        public void LoadObservations_WithFullDataFile_ParsesFirstRecordCorrectly()
        {
            // Arrange
            var dataAccess = new DataAccess();
            string testDirectory = TestContext.CurrentContext.TestDirectory;
            string fullPathToTestData = Path.Combine(testDirectory, _testFileName);

            // Pre-check to ensure the test file exists at the expected path before running the main logic.
            Assert.That(File.Exists(fullPathToTestData), Is.True,
                $"FATAL: Test file could not be found. Expected at: {fullPathToTestData}");

            // Act
            var observations = dataAccess.LoadObservations(fullPathToTestData);

            // Assert
            Assert.That(observations, Is.Not.Null);
            Assert.That(observations.Count, Is.EqualTo(100));

            var firstRecord = observations.First();

            Assert.Multiple(() =>
            {
                Assert.That(firstRecord.SiteId, Is.EqualTo(1));
                Assert.That(firstRecord.CameraSetDateTime, Is.EqualTo(new DateTime(2014, 5, 9, 8, 35, 0)));
                Assert.That(firstRecord.CameraCheckDateTime, Is.EqualTo(new DateTime(2014, 5, 23, 16, 35, 0)));
                Assert.That(firstRecord.LureType, Is.EqualTo("Anis Oil"));
                Assert.That(firstRecord.SpeciesCommonName, Is.EqualTo("Black-tailed Deer"));
                Assert.That(firstRecord.IndividualCount, Is.EqualTo(1));
                Assert.That(firstRecord.ObservationDateTime, Is.EqualTo(new DateTime(2014, 5, 23, 7, 43, 0)));
            });
        }
    }
}