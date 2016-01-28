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
    /// <summary>
    /// Handle the Request RecognitionEngine.
    /// </summary>
    public class RequestEar : Ear
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="earManager">Ear manager</param>
        public RequestEar(EarManager earManager)
            : base(earManager)
        {
        }

        /// <summary>
        /// Called when a speech is in the process.
        /// </summary>
        public override void SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            PackageHost.WriteInfo("Jarvis vous écoute.");
        }

        /// <summary>
        /// Called when a speech is rejected.
        /// </summary>
        public override void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            this.EarManager.StopRequestEar();
            PackageHost.WriteInfo("Jarvis n'a pas reconnu votre demande.");
            Voice.Speak("Je n'ai pas compris.", false);
            this.EarManager.StartJarvisEar();
        }

        /// <summary>
        /// Load the grammar from the JarvisSettings.
        /// </summary>
        /// <returns>Grammar</returns>
        public override Grammar GetGrammar()
        {
            SrgsDocument xmlGrammar = new SrgsDocument(JarvisSettings.GrammarFile);
            return new Grammar(xmlGrammar);
        }

        /// <summary>
        /// Called when a speech is recognized.
        /// </summary>
        public override void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            this.EarManager.StopRequestEar();

            if (e.Result.Confidence >= 0.7)
            {
                PackageHost.CreateScope(MessageScope.ScopeType.Groups, "JarvisSpeech").Proxy.SpeechReceive(new
                {
                    Text = e.Result.Text,
                    Confidence = e.Result.Confidence,
                    SemanticValue = e.Result.Semantics.Count > 0 ? e.Result.Semantics.ToDictionary(sv => sv.Key, sv => sv.Value.Value) : new Dictionary<string, object>() { { "RootKey", (object)e.Result.Semantics.Value.ToString() } },
                    Words = e.Result.Words.Select(w => w.Text).ToList()
                });                
            }
            EarManager.StartJarvisEar();
        }
    }
}
