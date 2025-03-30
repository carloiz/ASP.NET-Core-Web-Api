namespace ASP.NetCoreWebApi
{
    public class SystemDate
    {
        public DateTime Date()
        {
            var TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Manila");

            // Convert current UTC time to Philippine Time
            var Time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZone);

            return Time;
        }
    }
}