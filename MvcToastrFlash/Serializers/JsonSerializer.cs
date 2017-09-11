using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RedWillow.MvcToastrFlash.Serializers
{
    /// <summary>
    /// <para>
    ///     Simple JSON serializer for handling the options object
    ///     for modifying the default Toastr settings.
    /// </para>
    /// <para>
    ///     Didn't go the route of JSON.NET because: first, our needs
    ///     here are pretty simple; and second, this avoids needing to
    ///     manually setup assembly binding redirects for JSON.NET when
    ///     projects adding this package already have a different version
    ///     of JSON.NET.
    /// </para>
    /// </summary>
    internal static class JsonSerializer
    {
        /// <summary>
        /// Serializes the specified flat dynamic object.
        /// </summary>
        /// <param name="value">The object to be serialized.</param>
        /// <returns>JSON string representing the object.</returns>
        public static string Serialize(object value)
        {
            if (value == null)
                return "null";

            var properties = new List<string>();
            

            foreach(var propertyInfo in value.GetType().GetTypeInfo().GetProperties())
            {
                var name = propertyInfo.Name.Replace("\"", "\\\"");
                var data = propertyInfo.GetValue(value, null);

                var jsonData = string.Empty;
                if (data == null)
                {
                    jsonData = "null";
                }
                else
                {
                    switch (propertyInfo.PropertyType.Name)
                    {
                        case "Boolean":
                            jsonData = ((bool)data) ? "true" : "false";
                            break;
                        default:
                            // Toastr is happy to have everything that's not null or boolean
                            // as a string.
                            jsonData = string.Format(@"""{0}""",
                                data.ToString().Replace(@"""", @"\"""));
                            break;
                    }
                }
                properties.Add(string.Format(@"    ""{0}"": {1}",
                    name, jsonData));
            }

            var jsonText = new StringBuilder();
            jsonText.AppendLine("{");
            jsonText.AppendLine(string.Join($",{Environment.NewLine}", properties));
            jsonText.Append("}");

            return jsonText.ToString();
        }
    }
}
