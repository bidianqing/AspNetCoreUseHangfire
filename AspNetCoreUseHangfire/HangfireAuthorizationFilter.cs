using Hangfire.Dashboard;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace AspNetCoreUseHangfire
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            HttpContext httpContext = context.GetHttpContext();

            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                httpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"BasicAuthentication\", charset=\"UTF-8\"";
                httpContext.Response.StatusCode = 401;
                return false;
            }

            try
            {
                if (!AuthenticationHeaderValue.TryParse(httpContext.Request.Headers["Authorization"], out var authHeader))
                {
                    httpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"BasicAuthentication\", charset=\"UTF-8\"";
                    httpContext.Response.StatusCode = 401;
                    return false;
                }

                var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
                if (credentials.Length != 2)
                {
                    httpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"BasicAuthentication\", charset=\"UTF-8\"";
                    httpContext.Response.StatusCode = 401;
                    return false;
                }
                var username = credentials[0];
                var password = credentials[1];
                if(username != "admin" || password != "123456")
                {
                    httpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"BasicAuthentication\", charset=\"UTF-8\"";
                    httpContext.Response.StatusCode = 401;
                    return false;
                }
            }
            catch (FormatException)
            {
                httpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"BasicAuthentication\", charset=\"UTF-8\"";
                httpContext.Response.StatusCode = 401;
                return false;
            }
            catch (Exception)
            {
                httpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"BasicAuthentication\", charset=\"UTF-8\"";
                httpContext.Response.StatusCode = 401;
                return false;
            }

            // 验证通过后
            return true;
        }
    }
}
