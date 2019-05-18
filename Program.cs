using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using static System.Environment;
using static System.Math;

namespace C_Sharp_Playground
{
    static class ToolkitGeneral
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
        public static dynamic Input(string message = "", string type = null)
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

        /// <summary>
            /// Generates a class
            /// </summary>
        public static void GenerateClass()
        {
            // Asking for the class properties
            string className = Input("Enter class name: ");
            string userNamespace = Input("Enter namespace: ");
            string newUserNamespace = userNamespace;

            // Remove spaces from the namespace for the file itself
            while (newUserNamespace.Contains(" "))
                newUserNamespace = newUserNamespace.Replace(" ", "_");

            // Create the file structure
            File.AppendAllText($"{className}.cs",
                "using System;" + NewLine +
                "" + NewLine +
                $"namespace {newUserNamespace}" + NewLine +
                "{" + NewLine +
                $"    public class {className}" + NewLine +
                "    {");

            string protectionLevel = "";
            List<string> attributeNameList = new List<string>();
            List<string> attributeTypeList = new List<string>();

            char input = Input("Add attributes? (y/n) ", "char");
            while (input != 'y' && input != 'n') input = Input("Invalid input. Add attributes? (y/n) ", "char");
            while (input == 'y') // Adding attrubutes
            {
            protectionLevelIndicator:
                byte protectionLevelIndicator = Input("Enter protection level " +
                    "(1 for public, 2 for private, " +
                    "3 for internal, 4 for protected internal, 5 for private protected): ", "byte");
                switch (protectionLevelIndicator)
                {
                    case 1:
                        protectionLevel = "public";
                        break;
                    case 2:
                        protectionLevel = "private";
                        break;
                    case 3:
                        protectionLevel = "internal";
                        break;
                    case 4:
                        protectionLevel = "protected internal";
                        break;
                    case 5:
                        protectionLevel = "private protected";
                        break;
                    default:
                        WriteLine("Invalid input. Try again");
                        goto protectionLevelIndicator;
                }

                string attributeType = Input("Enter attribute type: ");
                attributeTypeList.Add(attributeType);

                string attributeName = Input("Enter attribute name: ");
                attributeNameList.Add(attributeName);

                File.AppendAllText($"{className}.cs", NewLine +
                    $"		{protectionLevel} {attributeType} {attributeName};");

                input = Input("Continue? (y/n) ", "char");
                while (input != 'y' && input != 'n') input = Input("Invalid input. Continue? (y/n) ", "char");
            }

            if (attributeNameList.Count > 0) // Making constructors, gets and sets
            {
                input = Input("Add default constructor? (y/n) ", "char");
                while (input != 'y' && input != 'n') input = Input("Invalid input. Continue? (y/n) ", "char");
                // Default constructor
                if (input == 'y')
                {
                    File.AppendAllText($"{className}.cs",
                        NewLine + NewLine + $"        public {className}()" + " { }");
                }

                input = Input("Add values constructor? (y/n) ", "char");
                while (input != 'y' && input != 'n') input = Input("Invalid input. Continue? (y/n) ", "char");
                // Values constructor
                if (input == 'y')
                {
                    string signatureString = $"        public {className}(";
                    for (int i = 0; i < attributeNameList.Count; i++)
                    {
                        if (i != attributeNameList.Count - 1)
                            signatureString += $"{attributeTypeList[i]} {attributeNameList[i]}, ";
                        else
                            signatureString += $"{attributeTypeList[i]} {attributeNameList[i]})";
                    }
                    File.AppendAllText($"{className}.cs",
                        NewLine + NewLine + signatureString + NewLine + "        {");
                    foreach (string attributeName in attributeNameList)
                    {
                        File.AppendAllText($"{className}.cs",
                            NewLine + $"            this.{attributeName} = {attributeName};");
                    }
                    File.AppendAllText($"{className}.cs",
                        NewLine + "        }");
                }

                input = Input("Add copy constructor? (y/n) ", "char");
                while (input != 'y' && input != 'n') input = Input("Invalid input. Continue? (y/n) ", "char");
                // Copy constructor
                if (input == 'y')
                {
                    string signatureString = $"        public {className}({className} toCopy)";
                    File.AppendAllText($"{className}.cs",
                        NewLine + NewLine + signatureString + NewLine + "        {");
                    foreach (string attributeName in attributeNameList)
                    {
                        File.AppendAllText($"{className}.cs",
                            NewLine + $"            this.{attributeName} = toCopy.{attributeName};");
                    }
                    File.AppendAllText($"{className}.cs",
                        NewLine + "        }");
                }

                // Gets and sets
                for (int i = 0; i < attributeNameList.Count; i++)
                {
                    File.AppendAllText($"{className}.cs",
                            NewLine + NewLine + $"        public {attributeTypeList[i]} {attributeNameList[i].Substring(0, 1).ToUpper() + attributeNameList[i].Substring(1)}"
                            + NewLine + "        {"
                            + NewLine + "            get { return " + attributeNameList[i] + "; }"
                            + NewLine + "            set { " + attributeNameList[i] + " = value; }"
                            + NewLine + "        }");
                }
            }

            // Finishing the file
            File.AppendAllText($"{className}.cs", NewLine + "    }" + NewLine + "}");

            // Linking the class if the code runs from a VS project to the project if the user asks for it
            input = Input("Do you want to link the class to the VS project? (y/n) ", "char");
            while (input != 'y' && input != 'n') input = Input("Invalid input. Do you want the class to be linked? (y/n) ", "char");
            if (input == 'y')
            {
                List<string> newFile = new List<string>();
                bool added = false;
                string currentDirectory = Directory.GetCurrentDirectory();
                string newDirectory = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\"));
                string[] txtLines = File.ReadAllLines($@"{newDirectory}\{userNamespace}.csproj");
                try { File.Move($"{className}.cs", $@"{newDirectory}\{className}.cs"); }
                catch (Exception)
                {
                    WriteLine("File already exists - copying failed.");
                    Exit(0);
                }
                File.WriteAllText($@"{newDirectory}\{userNamespace}.csproj", string.Empty);
                foreach (string line in txtLines)
                {
                    if (line.Contains("<Compile Include=") && !added)
                    {
                        newFile.Add($"    <Compile Include=\"{className}.cs\" />");
                        added = true;
                    }
                    newFile.Add(line);
                }
                File.AppendAllLines($@"{newDirectory}\{userNamespace}.csproj", newFile);
            }
        }

        /// <summary> Gets a number in binary basis (2 ^ x) and converts it to decimal basis (10 ^ x) </summary>
        /// <param name="numInStr"> The number in string format (in order to not remove the 0s) </param>
        /// <returns> The number in decimal basis </returns>
        public static int BinaryBasisToDecimalBasis(this string numInStr)
        {
            for (int i = 0, num = Parse(numInStr, "int"), bit, sum = 0, mod = 10, divide = 1; i <= numInStr.Length; bit = num % mod / divide, sum += (int)(Math.Pow(2, i) * bit), i++, mod *= 10, divide *= 10)
                if (i == numInStr.Length) return sum - num % mod / divide;
            return 0;
        }
    }

    static class ToolkitMath
    {
        /// <summary> Gets a number and calculates its length (number of digits) </summary>
        /// <param name="num"> The number to check </param>
        /// <returns> The number's length (number of digits) </returns>
        public static int Length(this int num) => num == 0 ? 1 : (int)Log10(Abs(num)) + 1;

        /// <summary> Calculates the factorial value from 1 until n - 1 </summary>
        /// <param name="n"> Indicates where to stop </param>
        /// <param name="factorialArr"> The array with all factorial results </param>
        public static int Factorial(this int n)
        {
            if (n < 1) throw new Exception("N smaller than 1 in factorial");
            int factorial = 1;
            for (int i = 1; i <= n; i++)
                factorial *= i;
            return factorial;
        }
    }

    static class ToolkitListsAndArrays
    {
        /// <summary> Prints an array with one of several ways </summary>
        /// <param name="arr"> The array to print </param>
        /// <param name="printType"> Specifies the way the array's items should be printed </param>
        /// <remarks>
        /// To print every item in a line of its own: printType = "WriteLine"
        /// To print all items in one line: printType = "Write"
        /// To print all itmes in one line with commas in-between: printType = "WriteWithCommas"
        /// </remarks>
        public static void PrintArray<T>(this IList<T> arr, int printType = 0)
        {
            switch (printType)
            {
                case 1:
                    foreach (var x in arr)
                        WriteLine(x);
                    break;
                case 2:
                    foreach (T item in arr)
                        Write(item);
                    WriteLine();
                    break;
                default:
                    WriteLine(String.Join(", ", arr));
                    break;
            }
        }

        /// <summary> Sorts an array </summary>
        /// <param name="arr"> The array to sort </param>
        public static void Sort<T>(this IList<T> arr) => Array.Sort((Array)arr);

        /// <summary> Makes an array of random int values </summary>
        /// <param name="arr"> The array to fill </param>
        /// <param name="min"> Minimum possible random value </param>
        /// <param name="max"> Maximum possible random value </param>
        /// <returns> An array of random int values </returns>
        public static void RandomArray(this IList<int> arr, int min = int.MinValue, int max = int.MaxValue - 1)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Count; i++)
                arr[i] = rnd.Next(min, max + 1);
        }
        /// <summary> Makes an array of random char values </summary>
        /// <param name="arr"> The array to fill </param>
        /// <returns> An array of random char values </returns>
        public static void RandomArray(this IList<char> arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Count; i++)
                arr[i] = (char)rnd.Next(33, 255);
        }
        /// <summary> Makes an array of random bool values </summary>
        /// <param name="arr"> The array to fill </param>
        /// <returns> An array of random bool values </returns>
        public static void RandomArray(this IList<bool> arr)
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Count; i++)
            {
                if (rnd.Next(1, 3) == 0) arr[i] = false;
                else arr[i] = true;
            }
        }
    }
}
