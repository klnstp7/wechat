using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Peacock.PEP.WeChat.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //要使用route特性必须运行此方法
            config.MapHttpAttributeRoutes();

            //config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    Converters = new JsonConverter[]
                    {
                        new StringEnumConverter()
                    }
                }
            });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
             );
        }
    }
}
