namespace Commander.Utilities
{
    public class DateConverter
    {
        public static System.DateTime ConvertFromUnixTimestamp(double Timestamp) => new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime().AddMilliseconds(Timestamp);
        public static double ConvertToUnixTimestamp(System.DateTime Date)
        {
            System.DateTime localTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            return (Date - localTime).TotalMilliseconds;
        }
    }
}
