using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.Util.IzolabellaConsole
{
    public class IzolabellaConsole
    {
        /// <summary>
        /// Awaits for the user to input the letter Y, and returns false for anything else.
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="MessageBefore"></param>
        /// <returns></returns>
        public static bool CheckY(string Context, string MessageBefore)
        {
            Console.WriteLine($"[{Context}]: {MessageBefore} - Press Y to accept, anything else to decline.");
            return Console.ReadKey(true).Key == ConsoleKey.Y;
        }

        /// <summary>
        /// Awaits for the end user to input a message.
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="MessageBefore"></param>
        /// <param name="Result"></param>
        /// <returns></returns>
        public static bool GetNext(string Context, string MessageBefore, out string? Result)
        {
            Write(Context, MessageBefore);
            try
            {
                Result = Console.ReadLine();
                return true;
            }
            catch (IOException E)
            {
                Result = E.Message;
                return false;
            }
        }

        /// <summary>
        /// Awaits for the end user to input a message, but never actually displays that message to the console.
        /// Useful for passwords, tokens, etc.
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="MessageBefore"></param>
        /// <param name="Result"></param>
        /// <returns></returns>
        public static bool GetProtectedNext(string Context, string MessageBefore, out string Result)
        {
            Write(Context, MessageBefore);
            bool R = true;
            Result = string.Empty;
            while (R)
            {
                ConsoleKeyInfo Key = Console.ReadKey(true);
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

        /// <summary>
        /// Writes a message to the console in the format `[Context]: Message`.
        /// </summary>
        /// <param name="Context">[Context]: Message</param>
        /// <param name="Message">[Context]: Message</param>
        public static void Write(string Context, string Message)
        {
            Console.WriteLine($"[{Context}]: {Message.ToLower()}");
        }
    }
}
