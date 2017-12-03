using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notes
{
    class FileLogger : ILogger
    {

        private string logger_dir;

        public FileLogger()
        {
            
            logger_dir = Path.Combine(StaticRes.AppData, "Notes_logs");
            if (!Directory.Exists(logger_dir))
            {
                Directory.CreateDirectory(logger_dir);

            }

        }

        private void LogExists()
        {
            var log_file = Path.Combine(logger_dir, DateTime.Now.ToString("yyyy.MM.dd") + ".txt");

            if (!File.Exists(log_file))
            {
                File.Create(log_file).Close();
            }

        }

        public void Print(string s)
        {
            LogExists();
            String fileName = Path.Combine(logger_dir, DateTime.Now.ToString("yyyy.MM.dd") + ".txt");

            String mutexName = fileName.Replace(Path.DirectorySeparatorChar, '_');  // Fixed DirectoryNotFoundException (throw from Mutex)

            Mutex mutexObj = new Mutex(true, mutexName);


            mutexObj.WaitOne();

            try
            {
                    using (StreamWriter writer = new System.IO.StreamWriter(fileName, true))
                    {
                        writer.WriteLine(DateTime.Now.ToString("hh:mm:ss.ms") + "---------------" + s);
                    }   
            }
            catch
            {
            }
            finally
            {
                fileName = null;
            }
            mutexObj.ReleaseMutex();
            mutexObj = null;
        }

        public void PrintError(String message,Exception ex)
        {

            Print(message);
            var realException = ex;
            while (realException != null)
            {
                Print(realException.Message);
                Print(realException.StackTrace);
                realException = realException.InnerException;
            }
            
        }
    }
}
