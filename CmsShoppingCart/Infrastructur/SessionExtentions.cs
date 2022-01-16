using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CmsShoppingCart.Infrastructur
{
    public static class SessionExtentions
    {
        public static void SetJson(this ISession session,string key,object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T Getjson<T>(this ISession session,string key)
        {
            var getsession = session.GetString(key);
            return getsession == null ? default(T) : JsonConvert.DeserializeObject<T>(getsession);
        }
    }
}
