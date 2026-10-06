namespace BancoAPI.Applications.Conversions
{
    public static class DateTimeToOnly
    {
        public static DateOnly ToOnly(this DateTime dt) => DateOnly.FromDateTime(dt); 
    }
}