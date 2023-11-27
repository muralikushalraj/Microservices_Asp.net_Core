namespace WebApp.Utility
{
    public class StaticData
    {
        public enum ApiType
        {
            GET,
            POST, 
            PUT, 
            DELETE
        }
        public static string CouponApiUrl { get; set; }
    }
}
