using System.ComponentModel.DataAnnotations;

namespace GameOfLife;

public static class Utilities
{
    /*
     <summary>
     UTypes Methods
        ConvertObject
        IsNumeric
     <summary>
    */
    public static class UTypes
    {
        public static T CastObject<T>(object input)
        {
            return (T)input;
        }

        public static T ConvertObject<T>(object input)
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }

        private static readonly HashSet<Type?> NumericTypes = new()
        {
            typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
            typeof(int), typeof(uint), typeof(long), typeof(ulong),
            typeof(nint), typeof(nuint), typeof(UInt128),
            typeof(double), typeof(float), typeof(decimal)
        };

        public static bool IsNumeric(Type myType)
        {
            return NumericTypes.Contains(Nullable.GetUnderlyingType(myType) ?? myType);
        }
    }

    /*
     <summary>
     UConsole Methods
           GetUserInput
           GetUserInputAndConvert
           GetUserInputAsNumericType
           Clear
           GetUserOption
           GetEnterConfirmation
           MoveCursorUp
           MoveCursorDown
           MoveCursorLeft
           MoveCursorRight
     <summary>
     */
    public static class UConsole
    {
        private const string DefaultPromptMessage = "Input ";

        public static string GetUserInput(string? prompt = DefaultPromptMessage)
        {
            string? input;

            do
            {
                if (!string.IsNullOrEmpty(prompt)) Console.Write(prompt + " ");
                Console.Write("\U000027A4");
            } while (string.IsNullOrEmpty(input = Console.ReadLine()));

            input = input.Normalize().ToLower().Trim();

            return input;
        }

        public static T GetUserInputAndConvert<T>(Func<string, T> converter, string? prompt = DefaultPromptMessage)
        {
            var response = GetUserInput(prompt);
            return converter(response);
        }

        public static T GetUserInputAsNumericType<T>(string? prompt = DefaultPromptMessage)
        {
            if (!UTypes.IsNumeric(typeof(T)))
                throw new ValidationException("passed wrong generic type");

            decimal userNumber;
            string input;

            do
            {
                input = GetUserInput(prompt);
            } while (!decimal.TryParse(input, out userNumber));

            return UTypes.ConvertObject<T>(userNumber);
        }

        public static void Clear()
        {
            Console.ResetColor();
            Console.Clear();
        }

        public static ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public static void MoveCursorUp(int val = 1)
        {
            try
            {
                Console.CursorTop -= val;
            }
            catch
            {
                Console.WriteLine("Can't move cursor up!");
            }
        }

        public static void MoveCursorDown(int val = 1)
        {
            try
            {
                Console.CursorTop += val;
            }
            catch
            {
                Console.WriteLine("Can't move cursor down!");
            }
        }

        public static void MoveCursorLeft(int val)
        {
            try
            {
                Console.CursorLeft -= val;
            }
            catch
            {
                Console.WriteLine("Can't move cursor left!");
            }
        }

        public static void MoveCursorRight(int val)
        {
            try
            {
                Console.CursorLeft += val;
            }
            catch
            {
                Console.WriteLine("Can't move cursor right!");
            }
        }


        public static string WrapLine(string paragraph = "", int tabSize = 8)
        {
            var lines = paragraph
                .Replace("\t", new string(' ', tabSize))
                .Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.None);

            var result = "";
            for (var i = 0; i < lines.Length; i++)
            {
                var process = lines[i];
                var wrapped = new List<string>();

                while (process.Length > Console.WindowWidth)
                {
                    var wrapAt = process.LastIndexOf(' ', Math.Min(Console.WindowWidth - 1, process.Length));
                    if (wrapAt <= 0) break;

                    wrapped.Add(process.Substring(0, wrapAt));
                    process = process.Remove(0, wrapAt + 1);
                }

                foreach (var wrap in wrapped) result += wrap + "\n";
                result += process + (i < lines.Length - 1 ? "\n" : "");
            }

            return result;
        }


        public static void WriteCentered(string paragraph, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            var lines = paragraph.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.None);
            foreach (var text in lines)
            {
                if ((Console.WindowWidth - text.Length) / 2 >= 0)
                    Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
                Console.WriteLine(text);
            }

            Console.ResetColor();
        }


        public static int GetUserOption(string? question, List<string> options)
        {
            var option = 0;
            var startingPosition = 0;
            var endingPosition = options.Count;

            var selected = false;
            Console.CursorVisible = false;

            if (question != null && options.Count > 0)
            {
                Console.Clear();
                WriteCentered(WrapLine(question + "\n"));

                var top = Console.CursorTop;
                for (var i = 0; i < options.Count; i++)
                    if (option == i)
                        WriteCentered(WrapLine(options[i]), ConsoleColor.Green);
                    else
                        WriteCentered(WrapLine(options[i]));
                Console.CursorTop = top;

                while (!selected)
                {
                    var key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        default:
                            break;
                        case ConsoleKey.DownArrow:
                            if (option + 1 < endingPosition)
                            {
                                WriteCentered(WrapLine(options[option]));
                                option++;
                                var selectedText = WrapLine(options[option]);
                                WriteCentered(selectedText, ConsoleColor.Green);
                                Console.CursorTop -= selectedText.Count(x => x == '\n') + 1;
                            }

                            break;
                        case ConsoleKey.UpArrow:
                            if (option - 1 >= startingPosition)
                            {
                                var deselected = WrapLine(options[option]);
                                WriteCentered(deselected);
                                option--;
                                var selectedText = WrapLine(options[option]);
                                Console.CursorTop -= selectedText.Count(x => x == '\n') +
                                                     deselected.Count(x => x == '\n') + 2;
                                WriteCentered(selectedText, ConsoleColor.Green);
                                Console.CursorTop -= selectedText.Count(x => x == '\n') + 1;
                            }

                            break;
                        case ConsoleKey.Enter:
                            selected = true;
                            break;
                    }
                }

                Console.CursorVisible = true;
                Console.Clear();
                return option;
            }
            else
            {
                WriteCentered("Wrong selection menu inputs", ConsoleColor.Red);
                return -1;
            }
        }

        public static bool GetUserBoolOption(string? question, List<string>? options)
        {
            if (options == null) options = new List<string>() { "Yes", "No" };

            var userOption = GetUserOption(question, options);

            // if userOption is 0 (yes) then return true, else false
            return userOption == 0;
        }

        public static void GetEnterConfirmation()
        {
            while (true)
            {
                Console.WriteLine("Press ENTER to continue:");
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.Enter) return;
            }
        }

        private const string WasKeyPressedPromptMultiple = "Press one of these buttons to break: ";
        private const string WasKeyPressedPromptSingle = "Press this button to break: ";

        public static bool WasKeyPressed(ConsoleKey[] breakKeys, string prompt = WasKeyPressedPromptMultiple)
        {
            ArgumentNullException.ThrowIfNull(breakKeys);

            Console.Write(prompt);
            foreach (var key in breakKeys) Console.Write(key + (key == breakKeys.Last() ? "" : " / "));
            Console.Write("\n");

            return Console.KeyAvailable && breakKeys.Contains(Console.ReadKey(true).Key);
        }

        public static bool WasKeyPressed(ConsoleKey breakKey, string prompt = WasKeyPressedPromptSingle)
        {
            ArgumentNullException.ThrowIfNull(breakKey);
            Console.WriteLine(prompt + breakKey);

            return Console.KeyAvailable && breakKey == Console.ReadKey(true).Key;
        }
    }
}