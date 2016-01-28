using Constellation.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jarvis.Model
{
    public class EarManager
    {
        public JarvisEar Jarvis { get; set; }
        public RequestEar Request { get; set; }

        private bool jarvisRunning = false;
        private bool requestRunning = false;

        private bool isEnable = true;

        public EarManager()
        {
            Jarvis = new JarvisEar(this);
            Request = new RequestEar(this);
            StartJarvisEar();
        }

        public void StartJarvisEar()
        {
            if (isEnable)
            {
                Request.Stop();
                Jarvis.Start();
            }
        }

        public void RestartJarvisEar()
        {
            if (isEnable)
            {
                Request.Stop();
                Jarvis.Restart();
            }
        }

        public void StartRequestEar()
        {
            if (isEnable)
            {
                Jarvis.Stop();
                Request.Start();
            }
        }

        public void StopJarvisEar()
        {
            Jarvis.Stop();
        }

        public void StopRequestEar()
        {
            Request.Stop();
        }

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

        private void StopAll()
        {
            StopJarvisEar();
            StopRequestEar();
        }

        private void RestartAll()
        {
            StartJarvisEar();
        }

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
