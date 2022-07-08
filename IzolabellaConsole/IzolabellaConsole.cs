using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Util.IzolabellaConsole
{
    public class IzolabellaConsole
    {
        public static bool CheckY(string Context, string MessageBefore)
        {
            System.Console.WriteLine($"[{Context}]: {MessageBefore} - Press Y to accept, anything else to decline.");
            return System.Console.ReadKey(true).Key == ConsoleKey.Y;
        }

        public static bool GetNext(string Context, string MessageBefore, out string? Result)
        {
            Write(Context, MessageBefore);
            try
            {
                Result = System.Console.ReadLine();
                return true;
            }
            catch (IOException E)
            {
                Result = E.Message;
                return false;
            }
        }

        public static bool GetProtectedNext(string Context, string MessageBefore, out string Result)
        {
            System.Console.WriteLine($"[{Context}]: {MessageBefore}");
            bool R = true;
            Result = string.Empty;
            while (R)
            {
                ConsoleKeyInfo Key = System.Console.ReadKey(true);
                if (Key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    Result += Key.KeyChar;
                }
            }
            return true;
        }

        public static void Write(string Context, string Message)
        {
            System.Console.WriteLine($"[{Context}]: {Message.ToLower()}");
        }
    }
}
