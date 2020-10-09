using System.Linq;
using System.Reflection;
using Xunit;

namespace TestProjectSelenium.Code.Extensions
{
    public static class ExtensionMethods
    {
        public static void CopyPropertiesTo<T>(this T source, T dest)
        {
            var plist = from prop in typeof(T).GetProperties() where prop.CanRead && prop.CanWrite select prop;

            foreach (PropertyInfo prop in plist)
            {
                prop.SetValue(dest, prop.GetValue(source, null), null);
            }
        }

        public static void IsEqualTo(this object a, object b)
        {
            Assert.True(a.Equals(b), $"'{a}' ≠ '{b}'");
        }

        public static void IsEqualTo(this object a, object b, string message)
        {
            Assert.True(a.Equals(b), $"'{a}' ≠ '{b}'. {message}");
        }
    }
}
