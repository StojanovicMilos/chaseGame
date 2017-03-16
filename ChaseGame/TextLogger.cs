using System;
using System.IO;

namespace ChaseGameNamespace
{
    public class TextLogger : ILogger
    {
        private StreamWriter streamWriter = new StreamWriter("Log" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + ".txt");
        public void Log(string message)
        {
            streamWriter.WriteLine(message);
            streamWriter.WriteLine();
        }

        public void Dispose()
        {
            streamWriter.Close();
            streamWriter.Dispose();
        }
    }
}