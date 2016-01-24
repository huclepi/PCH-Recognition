using Constellation.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis
{
    class Voice
    {
        private static string voiceName = "Microsoft Hortense Desktop";

        private static SpeechSynthesizer synth = initVoice();

        public static SpeechSynthesizer initVoice()
        {
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoice(voiceName);
            return synth;
        }

        public static void Speak(string text, bool writeOnConsole = true, bool notify = false)
        {
            if (writeOnConsole)
            {
                PackageHost.WriteInfo(Format(text));
            }
            synth.Speak(text);
            if (notify)
            {
                Notify(text);
            }
        }

        public static string Format(string text)
        {
            return String.Format("[Jarvis] {0}", text);
        }

        public static void Notify(string text, string title = "Jarvis")
        {
            PackageHost.CreateScope("PushBullet").Proxy.SendPush(new { Title = title, Message = text });
        }
    }
}
