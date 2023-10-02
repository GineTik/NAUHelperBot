using System.Reflection;

namespace NauHelper.Startup
{
    public static class UserInterfacesAssemblies
    {
        public static IEnumerable<Assembly> Assemblies => new[]
        {
            // need add link on the project
            Assembly.Load("UserInterfaces.CommonUser"),
            Assembly.Load("UserInterfaces.Owner")
        };
    }
}
