using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Project.Global_Classes
{
    public static class clsLogger
    {
        // Specify the source name for the event log
        private static string sourceName = "DVLD";

        public static void ExceptionLogger(Exception ex, EventLogEntryType type)
        {
            // Create the event source if it does not exist
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }

            // Log an information event
            EventLog.WriteEntry(sourceName, _FormatErrorMessage(ex), type);
        }

        private static string _FormatErrorMessage(Exception ex)
        {
            string message =
                 $"--- Exception Log ---\n" +
                 $"Timestamp: {DateTime.Now}\n" +
                 $"Message: {ex.Message}\n" +
                 $"Inner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "N/A")}\n" +
                 $"Stack Trace: {ex.StackTrace}\n" +
                 $"Source: {ex.Source}\n" +
                 $"-----------------------";

            return message;
        }
    }
}
