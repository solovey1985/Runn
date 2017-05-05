using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner.Services.Framework
{
    internal static class FileSystemHelper
    {
        internal static bool ExistOrCreateDirectory(string directoryName) {
            if (Directory.Exists(directoryName))
            {
                return true;
            }
            else
            {
                Directory.CreateDirectory(directoryName);
                return false;
            }
        }
    }
}
