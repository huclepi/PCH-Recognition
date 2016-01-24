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

        bool jarvisRunning = false;
        bool requestRunning = false;

        public EarManager()
        {
            Jarvis = new JarvisEar(this);
            Request = new RequestEar(this);
            StartJarvisEar();
        }

        public void StartJarvisEar()
        {
            Request.Stop();
            Jarvis.Start();
        }

        public void RestartJarvisEar()
        {
            Request.Stop();
            Jarvis.Restart();
        }

        public void StartRequestEar()
        {
            Jarvis.Stop();
            Request.Start();
        }

        public void StopJarvisEar()
        {
            Jarvis.Stop();
        }

        public void StopRequestEar()
        {
            Request.Stop();
        }

        internal void Pause()
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

        internal void Resume()
        {
            if (jarvisRunning)
            {
                Jarvis.Start();
            }
            else if (requestRunning)
            {
                Request.Start();
            }
        }
    }
}
