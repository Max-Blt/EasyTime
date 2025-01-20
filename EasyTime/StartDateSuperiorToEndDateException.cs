namespace EasyTime
{
    public class StartDateSuperiorToEndDateException : Exception
    {
        public StartDateSuperiorToEndDateException(Date a, Date b) : base($"Not a correct period, startDate {a.ToString()} is superior to endDate {b.ToString()}")
        {
        }
    }
}
