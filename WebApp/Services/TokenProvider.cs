using Newtonsoft.Json.Linq;
using WebApp.Services.IServices;
using WebApp.Utility;

namespace WebApp.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        public void ClearToken()
        {
            _contextAccessor.HttpContext.Response.Cookies.Delete(StaticData.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? isValue = _contextAccessor.HttpContext.Request.Cookies.TryGetValue(StaticData.TokenCookie, out token);
            return isValue is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext.Response.Cookies.Append(StaticData.TokenCookie, token);
        }
    }
}
