using Constellation.Host;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Model
{
    public class JarvisEar : Ear
    {
        public JarvisEar(EarManager earManager)
            : base(earManager)
        {

        }

        /// <summary>
        /// Méthode utilisée lorsque la reconnaissance vocale pour Jarvis est en cours
        /// </summary>
        public override void SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            PackageHost.WriteInfo("Appel de Jarvis.");
        }

        /// <summary>
        /// Méthode utilisée lorsque la reconnaissance vocale pour Jarvis a échoué
        /// </summary>
        public override void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            PackageHost.WriteInfo("Relancement de Jarvis.");
            //EarManager.RestartJarvisEar();
        }

        public override Grammar GetGrammar()
        {
            using (MemoryStream memStream = new MemoryStream(Jarvis.Properties.Resources.jarvis))
            {
                return new Grammar(memStream);
            }
        }

        /// <summary>
        /// Méthode utilisée lorsque la reconnaissance vocale pour Jarvis est réussi
        /// </summary>
        public override void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            this.EarManager.StopJarvisEar();
            if ((e.Result.Semantics["data_type"].Value.ToString().Equals("Jarvis") || e.Result.Semantics["data_type"].Value.ToString().Equals("a dit")) && e.Result.Confidence >= 0.7)
            {
                Voice.Speak(String.Format("Oui {0} ?", Model.JarvisSettings.User));
                EarManager.StartRequestEar();
            }
            else
            {
                PackageHost.WriteInfo("[Jarvis] Bonjour qui ?");
                EarManager.StartJarvisEar();
            }
        }

    }
}
