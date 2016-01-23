using Constellation.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Model
{
    class JarvisSettings
    {
        public static string User
        {
            get { return PackageHost.GetSettingValue<string>("User"); }
        }

        public static string GrammarFile
        {
            get { return PackageHost.GetSettingValue<string>("Grammar"); }
        }

    }
}
