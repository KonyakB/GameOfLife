using System.ComponentModel.DataAnnotations;

namespace GameOfLife;

public class Utilities
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


        public static int GetUserOption(string[] options, string text = "Choose an option:")
        {
            // WARNING: it's buggy when the text is multiple lines
            // TODO: I have to fix it one day
            
            var selectedOption = 0;

            Console.WriteLine(text);


            for (var i = 0; i < options.Length; ++i)
                Console.WriteLine(
                    $"{(i == selectedOption ? "\U000027A4 " : " ")} {options[i]}"
                );

            MoveCursorUp(options.Length);

            while (true)
            {
                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow when selectedOption > 0:
                        Console.Write($"\r {options[selectedOption]}".PadRight(10));
                        MoveCursorUp();
                        selectedOption = Math.Max(0, selectedOption - 1);
                        Console.Write($"\r\U000027A4 {options[selectedOption]}".PadRight(10));

                        break;

                    case ConsoleKey.DownArrow when selectedOption < options.Length - 1:
                        Console.Write($"\r {options[selectedOption]}".PadRight(10));
                        MoveCursorDown();
                        selectedOption = Math.Min(options.Length - 1, selectedOption + 1);
                        Console.Write($"\r\U000027A4 {options[selectedOption]}".PadRight(10));

                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        // Console.Write($"You chose: {options[selectedOption]}".PadRight(11));
                        return selectedOption;
                }
            }
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
    }
 
}