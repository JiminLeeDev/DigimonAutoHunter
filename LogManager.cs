using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace DigimonAutoHunter
{
    static class LogManager
    {
        public static string SavePath = "./log.txt";

        public static void AppendErrorLog(Exception e, [CallerLineNumber] int lineNubmer = 0, [CallerMemberName] string caller = null)
        {
            File.AppendAllText(SavePath, e.Message + " at line " + lineNubmer.ToString() + " by " + caller+"\n", Encoding.UTF8);
        }

        public static void ApeendLog(string msg, [CallerLineNumber] int lineNubmer = 0, [CallerMemberName] string caller = null)
        {
            File.AppendAllText(SavePath, msg + " at line " + lineNubmer.ToString() + " by " + caller + "\n", Encoding.UTF8);
        }
    }
}
