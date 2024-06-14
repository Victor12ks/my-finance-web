﻿using Newtonsoft.Json;

namespace MyFinanceWeb.Domain.Utils
{
    public static class ExtensionMethods
    {
        public static T Clone<T>(this object source) where T : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Objeto de clone não pode ser nulo.");
            }

            var jsonString = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        public static bool IsEqualTo(this string source, string target)
        {
            return source.Equals(target, StringComparison.OrdinalIgnoreCase);
        }
    }
}
