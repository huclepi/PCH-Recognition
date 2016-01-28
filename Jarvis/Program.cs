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

        [MessageCallback]
        public void Speak(string text)
        {
            manager.Pause();
            Voice.Speak(text, true, false);
            manager.Resume();
        }

        [MessageCallback]
        public void EnableRecognition(bool enable)
        {
            manager.EnableRecognition(enable);
        }

        /*[MessageCallback]
        public void Speak(string text, bool writeOnConsole = true, bool notify = false)
        {
            Voice.Speak(text, writeOnConsole, notify);
        }*/

    }
}
