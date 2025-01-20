
namespace EasyTime
{
    public class Period
    {
        public Date StartDate { get; }
        public Date EndDate { get; }

        public Period(Date startDate, Date endDate)
        {

            if (startDate > endDate)
            {
                throw new StartDateSuperiorToEndDateException(startDate, endDate);
            }
            StartDate = startDate;
            EndDate = endDate;
        }
        public Period(int year) : this(Date.FromYear(year), Date.FromEndYear(year))
        {

        }

        public int LengthInDays() => EndDate.GetJulianDayNumber() - StartDate.GetJulianDayNumber() + 1;



        public bool Contains(Date date) => date >= StartDate && date <= EndDate;
    }
}