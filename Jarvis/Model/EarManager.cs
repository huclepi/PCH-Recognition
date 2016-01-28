using Constellation.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Model
{
    /// <summary>
    /// Handler of Jarvis.
    /// </summary>
    public class EarManager
    {
        /// <summary>
        /// Jarvis RecognitionEngine
        /// </summary>
        public JarvisEar Jarvis { get; set; }

        /// <summary>
        /// Request RecognitionEngine : Handle the grammar file.
        /// </summary>
        public RequestEar Request { get; set; }

        /// <summary>
        /// Status of Jarvis RecognitionEngine.
        /// </summary>
        private bool jarvisRunning = false;

        /// <summary>
        /// Status of Request RecognitionEngine.
        /// </summary>
        private bool requestRunning = false;

        /// <summary>
        /// Status of Jarvis.
        /// </summary>
        private bool isEnable = true;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EarManager()
        {
            Jarvis = new JarvisEar(this);
            Request = new RequestEar(this);
            StartJarvisEar();
        }

        /// <summary>
        /// Start Jarvis RecognitionEngine.
        /// </summary>
        public void StartJarvisEar()
        {
            if (isEnable)
            {
                Request.Stop();
                Jarvis.Start();
            }
        }

        /// <summary>
        /// Restart Jarvis RecognitionEngine.
        /// </summary>
        public void RestartJarvisEar()
        {
            if (isEnable)
            {
                Request.Stop();
                Jarvis.Restart();
            }
        }

        /// <summary>
        /// Start Request RecognitionEngine.
        /// </summary>
        public void StartRequestEar()
        {
            if (isEnable)
            {
                Jarvis.Stop();
                Request.Start();
            }
        }

        /// <summary>
        /// Stop Jarvis RecognitionEngine.
        /// </summary>
        public void StopJarvisEar()
        {
            Jarvis.Stop();
        }

        /// <summary>
        /// Stop Request RecognitionEngine.
        /// </summary>
        public void StopRequestEar()
        {
            Request.Stop();
        }

        /// <summary>
        /// Pause both RecognitionEngines.
        /// </summary>
        public void Pause()
        {
            jarvisRunning = Jarvis.IsRunning;
            requestRunning = Request.IsRunning;
            if (jarvisRunning)
            {
                Jarvis.Stop();
            }
            if (requestRunning)
            {
                Request.Stop();
            }
            
        }

        /// <summary>
        /// Resume
        /// </summary>
        public void Resume()
        {
            if (isEnable && jarvisRunning)
            {
                Jarvis.Start();
            }
            else if (isEnable && requestRunning)
            {
                Request.Start();
            }
        }

        /// <summary>
        /// Stop both RecognitionEngines.
        /// </summary>
        private void StopAll()
        {
            StopJarvisEar();
            StopRequestEar();
        }

        /// <summary>
        /// Restart both RecognitionEngines.
        /// </summary>
        private void RestartAll()
        {
            StartJarvisEar();
        }

        /// <summary>
        /// Enable or disable the RecognitionEngine.
        /// </summary>
        /// <param name="enable">Future status of the RecognitionEngine</param>
        public void EnableRecognition(bool enable)
        {
            isEnable = enable;
            if (enable)
            {
                PackageHost.WriteInfo("Restarting Jarvis...");
                RestartAll();
            }
            else
            {
                PackageHost.WriteInfo("Stopping Jarvis...");
                StopAll();
            }
            
        }
    }
}
