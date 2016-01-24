using Constellation.Host;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Model
{
    public abstract class Ear
    {
        private bool isRunning = false;

        public bool IsRunning
        {
            get { return isRunning; }
        }

        private SpeechRecognitionEngine RecognitionEngine { get; set; }
        public EarManager EarManager { get; set; }

        public Ear(EarManager earManager)
        {
            this.EarManager = earManager;
            Init();
        }

        public abstract Grammar GetGrammar();

        public void Init()
        {
            //Création d'une grammaire depuis le fichier de grammaire
            Grammar grammar = GetGrammar();

            //Création de l'objet traitant la reconnaissance vocale
            RecognitionEngine = new SpeechRecognitionEngine();

            //Récupération du son du microphone
            RecognitionEngine.SetInputToDefaultAudioDevice();
            //Chargement de la grammaire
            RecognitionEngine.LoadGrammar(grammar);
            // Event handler
            RecognitionEngine.SpeechRecognized += SpeechRecognized;
            RecognitionEngine.SpeechRecognitionRejected += SpeechRejected;
            RecognitionEngine.SpeechHypothesized += SpeechHypothesized;
            //Spécification du nombre maximum d'alternatives
            RecognitionEngine.MaxAlternates = 4;
        }

        public void Stop()
        {
            if (isRunning)
            {
                RecognitionEngine.RecognizeAsyncStop();
                isRunning = false;
            }
        }

        public void Start()
        {
            if (!isRunning)
            {
                try
                {
                    RecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
                    isRunning = true;
                }
                catch
                {
                    isRunning = false;
                }
            }

        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public abstract void SpeechRecognized(object sender, SpeechRecognizedEventArgs e);
        public abstract void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e);
        public abstract void SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e);
    }
}
