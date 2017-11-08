using System;

namespace LaCorridor.Logging.AzureEventHubs
{
    internal static class Arguments
    {
        public static T IsNotNull<T>(T obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
            return obj;
        }

        public static string IsNotNullOrEmpty(string obj, string name)
        {
            if (string.IsNullOrEmpty(obj))
            {
                throw new ArgumentNullException(name);
            }
            return obj;
        }
    }
}
