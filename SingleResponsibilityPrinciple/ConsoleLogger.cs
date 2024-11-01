using System;

using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    public class ConsoleLogger : ILogger
    {
        public void LogMessage(string type, string message, params object[] args)
        {
            // Output to console
            Console.WriteLine(type + ": " + message, args);

            // Append log to XML file
            using (StreamWriter logfile = File.AppendText("log.xml"))
            {
                logfile.WriteLine("<log><type>{0}</type><message>{1}</message></log>", type, string.Format(message, args));
            }
        }

        public void LogInfo(string message, params object[] args)
        {
            LogMessage("INFO", message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            LogMessage("WARN", message, args);
        }

        public void LogError(string message, params object[] args)
        {
            LogMessage("ERROR", message, args);
        }
    }
}