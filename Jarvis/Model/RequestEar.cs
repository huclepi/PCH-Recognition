using Constellation;
using Constellation.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Recognition.SrgsGrammar;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Model
{
    public class RequestEar : Ear
    {

        public RequestEar(EarManager earManager)
            : base(earManager)
        {
        }

        /// <summary>
        /// Méthode utilisée lorsque la reconnaissance vocale  est en cours
        /// </summary>
        public override void SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            PackageHost.WriteInfo("Jarvis vous écoute.");
        }

        /// <summary>
        /// Méthode utilisée lorsque la reconnaissance vocale a échoué
        /// </summary>
        public override void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            this.EarManager.StopRequestEar();
            PackageHost.WriteInfo("Jarvis n'a pas reconnu votre demande.");
            Voice.Speak("Je n'ai pas compris.", false);
            this.EarManager.StartJarvisEar();
        }

        public override Grammar GetGrammar()
        {
            //Création d'un document de la norme SRGS à partir du fichier grxml
            SrgsDocument xmlGrammar = new SrgsDocument(JarvisSettings.GrammarFile);
            return new Grammar(xmlGrammar);
        }

        /// <summary>
        /// Méthode utilisée lorsque la reconnaissance vocale est réussi
        /// </summary>
        public override void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            this.EarManager.StopRequestEar();
            PackageHost.CreateScope(MessageScope.ScopeType.Groups, "JarvisSpeech").Proxy.SpeechReceive(new
            {
                Text = e.Result.Text,
                Confidence = e.Result.Confidence,
                SemanticValue = e.Result.Semantics.Count > 0 ? e.Result.Semantics.ToDictionary(sv => sv.Key, sv => sv.Value.Value) : new Dictionary<string, object>() { { "RootKey", (object)e.Result.Semantics.Value.ToString() } },
                Words = e.Result.Words.Select(w => w.Text).ToList()
            });

            EarManager.StartJarvisEar();
        }
    }
}
