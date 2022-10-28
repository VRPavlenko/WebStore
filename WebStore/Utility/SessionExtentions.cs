using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace WebStore.Utility
{
    //
    public static class SessionExtentions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var val = session.GetString(key);
            return val == null ? default : JsonSerializer.Deserialize<T>(val);
        }
    }
}