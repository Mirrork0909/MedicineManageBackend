using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineManageProject.Utils
{
    public class SessionUtil
    {
        public static bool checkSessionExisted(HttpContext httpContext)
        {
            string browsertoken = httpContext.Request.Cookies["token"];
            //不存在，则判断未登录
            if (string.IsNullOrEmpty(browsertoken) || string.IsNullOrEmpty(httpContext.Session.GetString(browsertoken)))
            {
                return false;
            }
            else
            {
                string url = httpContext.Session.GetString(browsertoken);
                //将请求的url注册
                httpContext.Session.SetString(browsertoken, url);
                return true;
            }
        }

        public static void addTokenToSession(HttpContext httpContext,string user_id)
        {
            //保存用户信息
            //httpContext.Session.SetString("uid", "1234");
            //生成token
            string token = Guid.NewGuid().ToString();
            //写入浏览器token
            httpContext.Response.Cookies.Append("token", token);
            //将请求的url注册
            httpContext.Session.SetString(token, user_id);
        }
    }
}
