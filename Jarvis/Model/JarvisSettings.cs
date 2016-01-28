using Constellation.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Model
{
    /// <summary>
    /// Static class which give us the settings which have been set during the package installation.
    /// </summary>
    class JarvisSettings
    {
        /// <summary>
        /// Return the name of the User.
        /// </summary>
        public static string User
        {
            get { return PackageHost.GetSettingValue<string>("User"); }
        }

        /// <summary>
        /// Return the location of the XML grammar file.
        /// </summary>
        public static string GrammarFile
        {
            get { return PackageHost.GetSettingValue<string>("Grammar"); }
        }

    }
}
