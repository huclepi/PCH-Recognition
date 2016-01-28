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
    /// <summary>
    /// Class which represents a SpeechRecognitionEngine.
    /// </summary>
    public abstract class Ear
    {
        /// <summary>
        /// Say if the RecognitionEngine is working or not.
        /// </summary>
        private bool isRunning = false;

        public bool IsRunning
        {
            get { return isRunning; }
        }

        /// <summary>
        /// RecognitionEngine
        /// </summary>
        private SpeechRecognitionEngine RecognitionEngine { get; set; }

        /// <summary>
        /// EarManager
        /// </summary>
        public EarManager EarManager { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="earManager">Ear manager</param>
        public Ear(EarManager earManager)
        {
            this.EarManager = earManager;
            Init();
        }

        /// <summary>
        /// Return the grammar.
        /// </summary>
        /// <returns>Grammar</returns>
        public abstract Grammar GetGrammar();

        /// <summary>
        /// Initialize the RecognitionEngine.
        /// </summary>
        public void Init()
        {
            //Grammar creation
            Grammar grammar = GetGrammar();

            //RecognitionEngine constructor
            RecognitionEngine = new SpeechRecognitionEngine();

            //Get the microphone.
            RecognitionEngine.SetInputToDefaultAudioDevice();
            //Load  the grammar file.
            RecognitionEngine.LoadGrammar(grammar);
            // Event handler
            RecognitionEngine.SpeechRecognized += SpeechRecognized;
            RecognitionEngine.SpeechRecognitionRejected += SpeechRejected;
            RecognitionEngine.SpeechHypothesized += SpeechHypothesized;
            //Set the max options
            RecognitionEngine.MaxAlternates = 4;
        }

        /// <summary>
        /// Stop the RecognitionEngine.
        /// </summary>
        public void Stop()
        {
            if (isRunning)
            {
                RecognitionEngine.RecognizeAsyncStop();
                isRunning = false;
            }
        }

        /// <summary>
        /// Start the RecognitionEngine.
        /// </summary>
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

        /// <summary>
        /// Restart the RecognitionEngine.
        /// </summary>
        public void Restart()
        {
            Stop();
            Start();
        }

        /// <summary>
        /// Called when a speech is recognized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void SpeechRecognized(object sender, SpeechRecognizedEventArgs e);

        /// <summary>
        /// Called when a speech is rejected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e);

        /// <summary>
        /// Called when a speech is in the process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e);
    }
}
