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

        /// <summary>
        /// Initiate the SpeechSynthesizer.
        /// </summary>
        /// <returns></returns>
        public static SpeechSynthesizer initVoice()
        {
            synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SelectVoice(voiceName);
            return synth;
        }

        /// <summary>
        /// Speak function.
        /// </summary>
        /// <param name="text">The text which will be use by the SpeechSynthesizer.</param>
        /// <param name="writeOnConsole">Say if you write the result on the console.</param>
        /// <param name="notify">Say if you send a notification to the user (Pushbullet).</param>
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

        /// <summary>
        /// Text format with Jarvis on the beginning. 
        /// </summary>
        /// <param name="text">Text to be format</param>
        /// <returns></returns>
        public static string Format(string text)
        {
            return String.Format("[Jarvis] {0}", text);
        }

        /// <summary>
        /// Pushbullet handler
        /// </summary>
        /// <param name="text">Text of the notification</param>
        /// <param name="title">Title of the notification</param>
        public static void Notify(string text, string title = "Jarvis")
        {
            PackageHost.CreateScope("PushBullet").Proxy.SendPush(new { Title = title, Message = text });
        }
    }
}
