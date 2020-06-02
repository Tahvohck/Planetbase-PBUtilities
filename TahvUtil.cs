using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tahvohck_Mods
{
    /***********************
    * !! File currently not set to be compiled
    * !! Change Build Action back to compile in order to have this get built back into 
    ***********************/

    public class TahvUtil
    {
        /// <summary>
        /// Logfile gathered from Planetbase's utils since it's there.
        /// </summary>
        public readonly static string Logfile = Util.getFilesFolder() + "/Modhooker.log";

        /// <summary>
        /// Log to both the unity debug log and modhooker.log.
        /// </summary>
        /// <param name="message">Object to log to file. Will be converted to a string via ToString</param>
        /// <param name="context">Unity Context. Largely unneeded, but might be useful if you're using
        /// a debugger.</param>
        public static void Log(object message, UnityEngine.Object context = null)
        {
            string logMessage;
            MethodBase method = new StackFrame(1).GetMethod();
            logMessage = $"[{method.DeclaringType.FullName}] {message}";

            // Log to Logfile, if errors are encountered then modify the logMessage.
            try {
                File.AppendAllText(Logfile, logMessage + Environment.NewLine, Encoding.ASCII);
            } catch (Exception ex) {
                logMessage = $"{logMessage}" +
                    $"\n  Error logging this message to the modhooker log: {Logfile}" +
                    $"\n  {ex.Message}";
            }
            UnityEngine.Debug.Log(logMessage, context);
        }

        /// <summary>
        /// Only meant to be used internally to clear the log before use.
        /// </summary>
        internal static void ClearLog()
        {
            File.WriteAllText(Logfile, string.Empty);
        }
    }
}
