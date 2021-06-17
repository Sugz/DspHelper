using System.IO;
using System.Reflection;

namespace DspHelper.Helpers
{
    internal class Constants
    {
        internal static string ExecutableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        internal static string DataFile = $"{ExecutableDirectory}/data.xml";
    }
}
