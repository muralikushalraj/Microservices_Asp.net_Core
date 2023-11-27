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
        public static string AuthApiUrl { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
    }
}
