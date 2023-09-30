using System.Reflection;

namespace NauHelper.Startup
{
    public static class UserInterfacesAssemblies
    {
        public static IEnumerable<Assembly> Assemblies => new[]
        {
            Assembly.Load("UserInterfaces.CommonUser")
        };
    }
}
