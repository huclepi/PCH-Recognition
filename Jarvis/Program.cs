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

        /// <summary>
        /// Start function.
        /// </summary>
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

        /// <summary>
        /// Allow to use the SpeechSynthesizer of the Jarvis package.
        /// </summary>
        /// <param name="text">The text which will be use by the SpeechSynthesizer.</param>
        [MessageCallback]
        public void Speak(string text)
        {
            manager.Pause();
            Voice.Speak(text, true, false);
            manager.Resume();
        }

        /// <summary>
        /// Enable or disable the recognition engine.
        /// </summary>
        /// <param name="enable">Status of the recognition.</param>
        [MessageCallback]
        public void EnableRecognition(bool enable)
        {
            manager.EnableRecognition(enable);
        }
    }
}
