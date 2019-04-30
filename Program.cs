using System;
using System.Collections.Generic;
using static System.Console;
using static System.Environment;
using static System.Math;

namespace Class_Generator
{
    static class Toolkit
    {
        /// <summary> Gets a value to parse, and the variable type for x to be parse to </summary>
        /// <param name="x"> The variable to parse </param>
        /// <param name="type"> The variable type for x to be parsed to </param>
        /// <returns> The parsed value </returns>
        /// <remarks>
        /// Variable types names in C#:
        /// System.Char - char, System.String - string, System.Boolean - bool,
        /// System.Byte - byte, System.SByte - sbyte, System.Int16 - short, System.UInt16 - ushort,
        /// System.Int32 - int, System.UInt32 - uint, System.Int64 - long, System.UInt64 - ulong,
        /// System.Single - float, System.Double - double, System.Decimal - decimal
        /// </remarks>
        public static dynamic ReturnParsedValue(dynamic x, string type)
        {
            switch (type)
            {
                case "byte":
                    x = byte.TryParse($"{x}", out byte resultByte) ? (byte?)byte.Parse($"{x}") : null;
                    break;
                case "sbyte":
                    x = sbyte.TryParse($"{x}", out sbyte resultSByte) ? (sbyte?)sbyte.Parse($"{x}") : null;
                    break;
                case "ushort":
                    x = ushort.TryParse($"{x}", out ushort resultUShort) ? (ushort?)ushort.Parse($"{x}") : null;
                    break;
                case "short":
                    x = short.TryParse($"{x}", out short resultShort) ? (short?)short.Parse($"{x}") : null;
                    break;
                case "uint":
                    x = uint.TryParse($"{x}", out uint resultUInt) ? (uint?)uint.Parse($"{x}") : null;
                    break;
                case "int":
                    x = int.TryParse($"{x}", out int resultInt) ? (int?)int.Parse($"{x}") : null;
                    break;
                case "ulong":
                    x = ulong.TryParse($"{x}", out ulong resultULong) ? (ulong?)ulong.Parse($"{x}") : null;
                    break;
                case "long":
                    x = long.TryParse($"{x}", out long resultLong) ? (long?)long.Parse($"{x}") : null;
                    break;
                case "float":
                    x = float.TryParse($"{x}", out float resultFloat) ? (float?)float.Parse($"{x}") : null;
                    break;
                case "double":
                    x = double.TryParse($"{x}", out double resultDouble) ? (double?)double.Parse($"{x}") : null;
                    break;
                case "decimal":
                    x = decimal.TryParse($"{x}", out decimal resultDecimal) ? (decimal?)decimal.Parse($"{x}") : null;
                    break;
                case "bool":
                    x = bool.TryParse($"{x}", out bool resultBool) ? (bool?)bool.Parse($"{x}") : null;
                    break;
                case "char":
                    x = char.TryParse($"{x}", out char resultChar) ? (char?)char.Parse($"{x}") : null;
                    break;
                case null:
                    x = $"{x}";
                    break;
                default:
                    WriteLine("ERROR - VARIABLE TYPE DOESN'T EXIST. EXITING APPLICATION.");
                    Exit(0);
                    break;
            }
            return x;
        }

        /// <summary> Calls for user input and parsing it using ReturnParsedValue method </summary>
        /// <param name="message"> The message to print </param>
        /// <param name="type"> The variable type for x to be parsed to </param>
        /// <returns> The parsed inputed value </returns>
        public static dynamic Input(string message, string type = null)
        {
            Write(message);
            dynamic x = ReadLine();
            x = ReturnParsedValue(x, type);
            while (x == null)
            {
                Write("Invalid value. Please try again: ");
                x = ReadLine();
                x = ReturnParsedValue(x, type);
            }
            return x;
        }

        /// <summary> Parsing a given value to a given type </summary>
        /// <param name="x"> The value to parse </param>
        /// <param name="type"> The value to parse to </param>
        /// <returns> The parsed value </returns>
        public static dynamic Parse(dynamic x, string type = null)
        {
            x = ReturnParsedValue(x, type);
            if (x == null)
            {
                WriteLine("ERROR - PARSE FAILED. EXITING APPLICATION.");
                Exit(0);
            }
            return x;
        }

        /// <summary> Prints an array with one of several ways </summary>
        /// <param name="arr"> The array to print </param>
        /// <param name="printType"> Specifies the way the array's items should be printed </param>
        /// <remarks>
        /// To print every item in a line of its own: printType = "WriteLine"
        /// To print all items in one line: printType = "Write"
        /// To print all itmes in one line with commas in-between: printType = "WriteWithCommas"
        /// </remarks>
        public static void PrintArray<T>(IList<T> arr, string printType = "")
        {
            switch (printType)
            {
                case "WriteLine":
                    foreach (var x in arr)
                        WriteLine(x);
                    break;
                case "WriteWithCommas":
                    WriteLine(String.Join(", ", arr));
                    break;
                default:
                    foreach (T item in arr)
                        Write(item);
                    break;
            }
        }

        /// <summary> Prints a 2D array in a table format </summary>
        /// <param name="arr"> The array to print </param>
        /// <param name="cols"> Number of columns in the 2D array </param>
        /// <remarks>
        /// You'll have to cast when calling the method, like this:
        /// Print2DArray(x.Cast<type>().ToList(), cols);
        /// </remarks>
        public static void Print2DArray<T>(IList<T> arr, int cols)
        {
            for (int count = 0; count < arr.Count; count++)
            {
                if ((count + 1) % cols == 0) WriteLine(arr[count]);
                else Write($"{arr[count]}, ");
            }
        }

        /// <summary> Gets a number in binary basis (2 ^ x) and converts it to decimal basis (10 ^ x) </summary>
        /// <param name="numInStr"> The number in string format (in order to not remove the 0s) </param>
        /// <returns> The number in decimal basis </returns>
        public static int BinaryBasisToDecimalBasis(string numInStr)
        {
            for (int i = 0, num = Parse(numInStr, "int"), bit, sum = 0, mod = 10, divide = 1; i <= numInStr.Length; bit = num % mod / divide, sum += (int)(Math.Pow(2, i) * bit), i++, mod *= 10, divide *= 10)
                if (i == numInStr.Length) return sum - num % mod / divide;
            return 0;
        }

        /// <summary> Makes an array of random int values </summary>
        /// <param name="arr"> The array to fill </param>
        /// <param name="min"> Minimum possible random value </param>
        /// <param name="max"> Maximum possible random value </param>
        /// <returns> An array of random int values </returns>
        public static int[] RandomIntArray(int[] arr, int min = int.MinValue, int max = int.MaxValue - 1)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rnd.Next(min, max + 1);
            return arr;
        }
        /// <summary> Makes an array of random char values </summary>
        /// <param name="arr"> The array to fill </param>
        /// <returns> An array of random char values </returns>
        public static char[] RandomCharArray(char[] arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
                arr[i] = (char)rnd.Next(33, 255);
            return arr;
        }
        /// <summary> Makes an array of random bool values </summary>
        /// <param name="arr"> The array to fill </param>
        /// <returns> An array of random bool values </returns>
        public static bool[] RandomBoolArray(bool[] arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                if (rnd.Next(1, 3) == 0) arr[i] = false;
                else arr[i] = true;
            }
            return arr;
        }
        /// <summary> Makes a 2D array of random int values </summary>
        /// <param name="arr"> The array to fill </param>
        /// <param name="min"> Minimum possible random value </param>
        /// <param name="max"> Maximum possible random value </param>
        /// <returns> A 2D array of random int values </returns>
        public static int[,] Random2DIntArray(int[,] arr, int min = int.MinValue, int max = int.MaxValue - 1)
        {
            Random rnd = new Random();
            int rows = arr.GetLength(0), cols = arr.GetLength(1);
            for (int row = 0; row < rows; row++)
                for (int col = 0; col < cols; col++)
                    arr[row, col] = rnd.Next(min, max + 1);
            return arr;
        }

        /// <summary> Takes an array and moves every value one cell to the right </summary>
        /// <param name="arr"> The array to change </param>
        /// <returns> The array after every value was moved one cell to the right </returns>
        /// <remarks> You'll have to cast when refrencing the array </remarks>
        public static IList<T> ShiftRightArray<T>(IList<T> arr)
        {
            dynamic last = arr[arr.Count - 1];
            for (int i = arr.Count - 1; i > 0; i--)
                arr[i] = arr[i - 1];
            arr[0] = last;
            return arr;
        }

        /// <summary> Takes an array and moves every value one cell to the left </summary>
        /// <param name="arr"> The array to change </param>
        /// <returns> The array after every value was moved one cell to the left </returns>
        /// <remarks> You'll have to cast when refrencing the array </remarks>
        public static IList<T> ShiftLeftArray<T>(IList<T> arr)
        {
            dynamic first = arr[0];
            for (int i = 0; i < arr.Count - 1; i++)
                arr[i] = arr[i + 1];
            arr[arr.Count - 1] = first;
            return arr;
        }

        /// <summary> Gets a number and calculates its length (number of digits) </summary>
        /// <param name="num"> The number to check </param>
        /// <returns> The number's length (number of digits) </returns>
        public static int LengthOfNum(int num) => num == 0 ? 1 : (int)Log10(Abs(num)) + 1;

        /// <summary> Checks if a string is a palindrom </summary>
        /// <param name="str"> The string to check </param>
        /// <returns> True if the entered string is a palindrom, otherwise false </returns>
        public static bool Palindrom(string str)
        {
            for (int i = 0; i < str.Length / 2; i++)
                if (!(str[i] == str[str.Length - 1 - i])) return false;
            return true;
        }

        /// <summary> Calculates the factorial value from 1 until n - 1 </summary>
        /// <param name="n"> Indicates where to stop </param>
        /// <param name="factorialArr"> The array with all factorial results </param>
        public static int[] Factorial(int n)
        {
            int[] factorialArr = new int[n - 1];
            factorialArr[0] = 1;
            for (int i = 1; i < factorialArr.Length; i++)
                factorialArr[i] = factorialArr[i - 1] * i;
            return factorialArr;
        }
        
        /// <summary> Prints a chess board </summary>
        public static void PrintChessBoard()
        {
            int[,] board = new int[8, 8];
            for (int row = 0; row < board.GetLength(0); row++)
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if ((row + col) % 2 == 0)
                    {
                        BackgroundColor = ConsoleColor.Black;
                        ForegroundColor = ConsoleColor.White;
                        if (col == board.GetLength(1) - 1) WriteLine("  ");
                        else Write("  ");
                    }
                    else
                    {
                        BackgroundColor = ConsoleColor.White;
                        ForegroundColor = ConsoleColor.Black;
                        if (col == board.GetLength(1) - 1) WriteLine("  ");
                        else Write("  ");
                    }
                }
        }
    }
}
