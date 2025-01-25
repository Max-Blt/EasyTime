using EasyTime;
using NFluent;

namespace UnitTests
{
    public class PeriodTests
    {
        [Theory]
        [InlineData("01/01/2000", "02/11/2000", false)]
        [InlineData("01/01/2001", "02/11/2000", true)]
        [InlineData("01/01/1999", "02/11/2000", false)]
        [InlineData("02/11/2000", "01/01/2000", true)]
        [InlineData("02/11/2000", "01/01/2001", false)]
        [InlineData("02/11/2000", "01/01/1999", true)]
        [InlineData("01/01/2000", "01/01/2000", true)]
        public void TestPeriodClass(string startDate, string endDate, bool assert)
        {
            // Arrange

            var startDateTest = new Date(startDate);
            var endDateTest = new Date(endDate);

            // Act
            var result = startDateTest >= endDateTest;


            // Assert
            Check.That(result).Is(assert);

        }

        [Theory]
        [InlineData("01/01/2001", "02/11/2000")]
        [InlineData("02/11/2000", "01/01/1999")]
        public void Test_StartDateSuperiorToEndDateException_Constructor(string startDate, string endDate)
        {
            // Arrange
            var startDateTest = new Date(startDate);
            var endDateTest = new Date(endDate);

            // Act & Assert
            Check.ThatCode(() => new Period(startDateTest, endDateTest))
            .Throws<StartDateSuperiorToEndDateException>().WithMessage($"Not a correct period, startDate {startDateTest.ToString()} is superior to endDate {endDateTest.ToString()}");

        }

        [Theory]
        [InlineData("01/01/2000", "02/11/2000")]
        [InlineData("01/01/1999", "02/11/2000")]
        [InlineData("01/01/2000", "01/01/2000")]
        public void PeriodConstructor_CreatesPeriod_WhenStartDateIsLessThanOrEqualToEndDate(string startDate, string endDate)
        {
            // Arrange
            var startDateTest = new Date(startDate);
            var endDateTest = new Date(endDate);
            // Act
            var period = new Period(startDateTest, endDateTest);
            // Assert
            Check.That(period.StartDate).IsEqualTo(startDateTest);
            Check.That(period.EndDate).IsEqualTo(endDateTest);
        }

        [Fact]
        public void PeriodConstructorFromYear_CreatesPeriodOfOneYear()
        {
            //Arrange
            var testYear = 2023;

            // Act
            var result = new Period(testYear);

            // Assert
            Check.That(result.StartDate.Year).IsEqualTo(testYear);
            Check.That(result.StartDate.Month).IsEqualTo(01);
            Check.That(result.StartDate.Day).IsEqualTo(01);
            Check.That(result.EndDate.Year).IsEqualTo(testYear);
            Check.That(result.EndDate.Month).IsEqualTo(12);
            Check.That(result.EndDate.Day).IsEqualTo(31);
        }

        [Theory]
        [InlineData("01/01/2020", "31/12/2020", 366)] // une année bisextile
        [InlineData("01/01/1999", "31/12/1999", 365)] // une année non bisextile
        [InlineData("01/01/2020", "12/12/2020", 347)] // dans une année bisextile
        [InlineData("01/01/1999", "12/12/1999", 346)] // dans une année non bisextile
        [InlineData("01/01/2010", "31/12/2030", 7670)] // deux années différentes
        [InlineData("01/08/2020", "31/08/2020", 31)] // un mois d'août
        [InlineData("01/09/2020", "30/09/2020", 30)] // un mois de septembre
        [InlineData("01/09/2020", "01/10/2020", 31)] // septembre à octobre
        [InlineData("01/01/2020", "02/01/2020", 2)] // un jour
        [InlineData("01/01/2020", "01/01/2020", 1)] // 0 jour
        public void LengthInDays_ShouldReturnCorrectNumberOfDays(string startDate, string endDate, int expectedValue)
        {
            //Arrange
            var period = new Period(new Date(startDate), new Date(endDate));

            //Act
            var result = period.LengthInDays();

            //Assert
            Check.That(result).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("01/01/2020", "12/12/2020", "02/12/2020", true)]
        [InlineData("02/01/2020", "31/12/2020", "01/01/2020", false)]
        [InlineData("01/01/2020", "31/12/2020", "01/01/2020", true)]
        public void IsDateInPeriod_ShouldReturnTrueIfDateIsInPeriod(string startDate, string endDate, string date, bool expectedValue)
        {
            //Arrange
            var testDate = new Date(date);
            var period = new Period(new Date(startDate), new Date(endDate));

            //Act
            var result = period.Contains(testDate);

            //Assert
            Check.That(result).IsEqualTo(expectedValue);
        }
    }
}
