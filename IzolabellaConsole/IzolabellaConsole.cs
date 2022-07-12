using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Console = Colorful.Console;

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
            Write(Context, $"{MessageBefore} - Press Y to accept, anything else to decline.");
            return Console.ReadKey(true).Key == ConsoleKey.Y;
        }

        /// <summary>
        /// Awaits for the end user to input a message.
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="MessageBefore"></param>
        /// <param name="Result"></param>
        /// <returns></returns>
        public static bool GetNext(string? Context, string? MessageBefore, out string? Result)
        {
            if(Context != null && MessageBefore != null)
            {
                Write(Context, MessageBefore);
            }
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

        private static List<Color> AllowedConsoleColors { get; } = new()
        {
            Color.FromArgb(213, 126, 126),
            Color.FromArgb(172, 125, 136),
            Color.FromArgb(133, 88, 111),
            Color.FromArgb(137, 138, 166),
        };

        private static Dictionary<string, Color> ContextColors { get; } = new();

        /// <summary>
        /// Writes a message to the console in the format `[Context]: Message`.
        /// </summary>
        /// <param name="Context">[Context]: Message</param>
        /// <param name="Message">[Context]: Message</param>
        public static void Write(string Context, string Message)
        {
            KeyValuePair<string, Color>? ColorKV = ContextColors.FirstOrDefault(C => C.Key.ToLower() == Context.ToLower());
            Color Color = AllowedConsoleColors.ElementAtOrDefault(new Random().Next(0, AllowedConsoleColors.Count));
            if (ColorKV.HasValue && ColorKV.Value.Key != null)
            {
                Color = ColorKV.Value.Value;
            }
            else
            {
                ContextColors.TryAdd(Context, Color);
            }
            Console.WriteLine($"[{Context}]: {Message.ToLower()}", Color);
        }
    }
}
