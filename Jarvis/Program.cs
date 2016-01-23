using Constellation;
using Constellation.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jarvis.Model;
using System.Speech.Synthesis;

namespace Jarvis
{
    public class Program : PackageBase
    {
        EarManager manager;

        public SpeechSynthesizer synth;


        static void Main(string[] args)
        {
            PackageHost.Start<Program>(args);
        }

        public override void OnStart()
        {
            if (!PackageHost.HasControlManager)
            {
                PackageHost.WriteError("Unable to connect !");
                return;
            }

            PackageHost.ControlManager.RegisterStateObjectLinks(this);
            manager = new EarManager();
        }


    }
}
