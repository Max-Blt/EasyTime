using System.Text.RegularExpressions;

namespace EasyTime
{
    public class Date
    {
        public int Day { get; }
        public int Month { get; }
        public int Year { get; }

        public Date(int day, int month, int year)
        {
            if (day <= 0 || day > 31)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (month <= 0 || month > 12)
            {
                throw new ArgumentOutOfRangeException();
            }
            Day = day;
            Month = month;
            Year = year;
        }
        public Date(string date)
        {
            string pattern = @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$";
            if (!Regex.IsMatch(date, pattern))
            {
                throw new ArgumentException("Invalid date format. Use dd/mm/yyyy.");
            }

            var day = Int32.Parse(date.Split('/')[0]);
            var month = Int32.Parse(date.Split('/')[1]);
            var year = Int32.Parse(date.Split('/')[2]);
            if (day <= 0 || day > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 31");
            }
            if (month <= 0 || month > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(month), "Month must be between 1 and 12");
            }
            Day = day;
            Month = month;
            Year = year;
        }

        public Date(int year) : this(1, 1, year) { }

        public static Date FromYear(int year) => new Date(1, 1, year);

        public static Date FromEndYear(int year) => new Date(31, 12, year);

        public static Date FromJanuary(int day, int year) => new Date(day, 1, year);
        public static Date FromFebruary(int day, int year) => new Date(day, 2, year);

        public int GetJulianDayNumber()
        {
            int newYear = Year;
            int newMonth = Month;

            if (Month <= 2)
            {
                newYear = Year - 1;
                newMonth = Month + 12;
            }
            int A = newYear / 100;
            int B = 2 - A + (A / 4);
            return (int)(365.25 * (newYear + 4716)) + (int)(30.6001 * (newMonth + 1)) + Day + B - 1524;
        }

        public static bool operator ==(Date a, Date b) => a.Year == b.Year && a.Month == b.Month && a.Day == b.Day;

        public static bool operator !=(Date a, Date b) => !(a == b);

        public static bool operator >=(Date a, Date b)
    => a.Year > b.Year || (a.Year == b.Year && a.Month > b.Month) || (a.Year == b.Year && a.Month == b.Month && a.Day >= b.Day);
        public static bool operator <=(Date a, Date b)
    => !(a >= b) || (a == b);
        public static bool operator >(Date a, Date b)
    => (a >= b) && (a != b);
        public static bool operator <(Date a, Date b)
    => (a <= b) && (a != b);
        public string ToString() => $"{Day}/{Month}/{Year}";
    }
}
