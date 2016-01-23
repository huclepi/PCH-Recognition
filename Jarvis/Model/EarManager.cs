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
        public QuestionEar Question { get; set; }

        public EarManager()
        {
            Jarvis = new JarvisEar(this);
            Question = new QuestionEar(this);
            StartJarvisEar();
        }

        public void StartJarvisEar()
        {
            StopQuestionEar();
            Jarvis.Start();
        }

        public void RestartJarvisEar()
        {
            StopQuestionEar();
            Jarvis.Restart();
        }

        public void StartQuestionEar()
        {
            StopJarvisEar();
            Question.Start();
        }

        private void StopJarvisEar()
        {
            Jarvis.Stop();
        }

        private void StopQuestionEar()
        {
            Question.Stop();
        }
    }
}
