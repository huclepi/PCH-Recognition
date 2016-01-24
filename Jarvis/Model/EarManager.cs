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
    }
}
