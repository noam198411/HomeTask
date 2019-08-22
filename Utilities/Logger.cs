using System;
using System.Globalization;
using System.IO;


namespace Utilities
{
   public static class Logger
    {

        public static void Log(string message, string testName)
        {

            string LOGS_FOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Logs");

            try
            {
                string formattedMessage = String.Format("[{0}] {1}", DateTime.Now.ToShortTimeString(), message);

                if (!Directory.Exists(LOGS_FOLDER))
                {
                    Console.WriteLine("Creating logs folder: " + LOGS_FOLDER);
                    Directory.CreateDirectory(LOGS_FOLDER);
                }
                string date = DateTime.Today.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
                string logFilename =$"{testName}.log";
                string logFullPath = Path.Combine(LOGS_FOLDER, logFilename);

                formattedMessage += Environment.NewLine;
                if (!File.Exists(logFullPath))
                {
                    File.WriteAllText(logFullPath, formattedMessage);
                }
                else
                {
                    File.AppendAllText(logFullPath, formattedMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
