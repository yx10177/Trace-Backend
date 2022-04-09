using Jose;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trace.Model;
using Utils;

namespace Trace.Api.Filter
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string secretKey = ConfigurationHelper.GetSection("TokenSecret").Value;
            string authorization = context.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer"))
            {
                try
                {
                    string jwtToken = authorization.Substring("Bearer ".Length).Trim();
                    var jwtObject = JWT.Decode<JWTPayload>(jwtToken, Encoding.UTF8.GetBytes(secretKey), JwsAlgorithm.HS256);
                    //TODO 驗證是否存在
                    
                    bool isLive = true;
                    if (isLive)
                    {
                        context.HttpContext.Items.Add("jwtPayload", jwtObject);
                    }
                    else //過期
                    {
                        context.Result = new UnauthorizedResult();
                    }

                }
                catch (Exception)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else 
            {
                context.Result = new UnauthorizedResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
