using KodavimasA5.Models;
using KodavimasA5.Services;
using System.Text;

namespace KodavimasA5.Helpers
{
    public static class ConsoleWriteHelper
    {
        private static int EnterScenarioNumber() 
        {
            Console.WriteLine("Please select one of the following scenario options:");
            Console.WriteLine("1. Enter binary vector");
            Console.WriteLine("2. Enter text");
            Console.WriteLine("3. Enter name of .bmp file");

            string input = "";
            bool isValidInput = false;

            while (!isValidInput) 
            {
                // Prompt user for input
                Console.Write("Enter the scenario number: ");

                input = Console.ReadLine();

                // Check if the input is valid
                if (input == "1" || input == "2" || input == "3")
                {
                    Console.WriteLine("You entered: " + input);
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
                }
            }

            return int.Parse(input);
        }

        public static void StartCodingTask(Random random) 
        {
            int scenarioNumber = EnterScenarioNumber();

            var scenarioService = new ScenarioService(random);
            StartScenario(scenarioService, scenarioNumber);
        }

        private static void StartScenario(ScenarioService scenarioService, double scenarioNumber) 
        {
            double p = RetrievePercentageFromConsole();
            int m = RetrieveNumberFromConsole();

            switch (scenarioNumber)
            { 
                case 1:
                    scenarioService.ExecuteFirstScenario(m,p);
                    break;
                case 2:
                    scenarioService.ExecuteSecondScenario(m,p);
                    break;
                case 3:
                    scenarioService.ExecuteThirdScenario(m,p);
                    break;
                default:
                    Console.WriteLine("Scenario undefined");
                    break;
            }
        }

        private static double RetrievePercentageFromConsole()
        {
            Console.Write("Enter a percentage of mistakes in the channel from 0 to 1: ");
            var isEnteredNumberCorrect = false;
            double result = 0;
            while (!isEnteredNumberCorrect)
            {
                var input = Console.ReadLine()?.Replace(',', '.');
                if (double.TryParse(input, out double number))
                {
                    if (isValidPercentage(number)) {
                        result = number;
                        isEnteredNumberCorrect = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid whole number 'm'.");
                }
            }
            return result;
        }

        private static bool isValidPercentage(double number) {
            return number >= 0 & number >= 0;
        }

        private static int RetrieveNumberFromConsole()
        {
            Console.Write("Enter a whole number 'm': ");
            var isEnteredNumberCorrect = false;
            int result = 0;
            while (!isEnteredNumberCorrect)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    result = number;
                    isEnteredNumberCorrect = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid whole number 'm'.");
                }
            }
            return result;
        }

        public static void PrintMatrix(Matrix matrix) 
        {

            for (int i = 0; i < matrix.Heigth; i++) 
            {
                for (int j = 0; j < matrix.Width; j++)
                {
                    Console.Write(matrix.Value[i,j]);
                }
                Console.Write("\n");
            }
            Console.WriteLine();
        }

        public static void PrintOneDimensionalArray(int[] array) 
        {
            for (int i = 0; i < array.Length; i++)
            { 
                Console.Write(array[i]);
            }
            Console.Write("\n");
        }

        public static string EnterBinaryVector(int m) {
            
            bool isInputCorrect = false;
            string input = "";

            while (!isInputCorrect) {

                Console.Write("Enter a binary vector (e.g., 10101): ");
                input = Console.ReadLine();

                if (IsBinaryVector(input))
                {
                    if (ValidatorHelper.IsBinaryVectorLengthCorrect(input, m))
                    {
                        Console.WriteLine("Binary vector entered successfully!");
                        Console.WriteLine("Vector: " + input);
                        isInputCorrect = true;
                    }
                    else {
                        Console.WriteLine("Invalid input. We will add additional '0' bits while coding and decoding");
                        isInputCorrect = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter only 0s and 1s.");
                }
            }
            return input;
        }

        private static bool IsBinaryVector(string input)
        {
            return input.All(c => c == '0' || c == '1');
        }

        public static string FixBinaryVectorMistakes(string inputVector, string channelVector)
        {
            var isContinued = true;
            while (isContinued)
            {
                Console.WriteLine("Do you want to change the vector? Enter 'y' for yes or 'n' for no:");
                var cleanedInput = GetCleanedUpInput();

                switch (cleanedInput.ToLower())
                {
                    case "n":
                        isContinued = false;
                        break;

                    case "y":
                        channelVector = FixMistake(channelVector, inputVector);
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                        break;
                }
            }
            return channelVector;
        }

        private static string FixMistake(string channelVector, string inputVector)
        {
            var stringIndexes = Enumerable.Range(0, channelVector.Length).ToList();
            var continueFixing = true;

            while (continueFixing)
            {
                Console.WriteLine("Enter the index to change: ");
                var indexInput = GetCleanedUpInput();

                if (int.TryParse(indexInput, out int index) && stringIndexes.Contains(index))
                {
                    channelVector = ReplaceIndex(index, channelVector);
                    Console.WriteLine($"Changed index: {index}");
                    Console.WriteLine($"Changed vector: {channelVector}");
                    PrintBinaryVectorMistakes(inputVector, channelVector);

                    // Ask if the user wants to fix another mistake
                    Console.WriteLine("Do you want to change another index? Enter 'y' for yes or 'n' for no:");
                    var fixAnotherInput = GetCleanedUpInput();

                    if (fixAnotherInput.ToLower() == "n")
                    {
                        continueFixing = false;
                    }
                }
                else
                {
                    if (!int.TryParse(indexInput, out _))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer.\n");
                    }
                    else if (!stringIndexes.Contains(index))
                    {
                        Console.WriteLine("The index entered does not correspond to a valid position in the vector.\n");
                    }
                }
            }
            return channelVector;
        }

        private static string ReplaceIndex(int index, string channelVector) 
        {
            //Because string are immutable, we have to convert to char array
            char[] chars = channelVector.ToCharArray();
            chars[index] = GetOppositeBinaryValue(channelVector[index]);
            return new string(chars);
        }

        private static string GetCleanedUpInput() 
        {
            var input = Console.ReadLine();
            return StringHelper.EliminateAllWhiteSpaces(input);
        }

        private static bool CheckIfIndexBelongsToIndexList(int index, List<int> indexList) 
        {
            return indexList.Contains(index);
        }

        public static void PrintBinaryVectorMistakes(string inputVector, string channelVector)
        {
            if (inputVector.Length != channelVector.Length)
            {
                Console.WriteLine("Error: Vectors must be of the same length.");
                return;
            }

            var mistakeIndexes = GetMistakeIndexes(inputVector, channelVector);

            for (int i = 0; i < mistakeIndexes.Count; i++) {
                var mistakeIndex = mistakeIndexes[i];
                Console.WriteLine($"Mistake at Index {mistakeIndex}." +
                    $" Current value: {inputVector[mistakeIndex]}," +
                    $" should be: {GetOppositeBinaryValue(inputVector[mistakeIndex])}");
            }

            Console.WriteLine($"Total number of mistakes: {mistakeIndexes.Count}");
        }

        private static char GetOppositeBinaryValue(char value)
        {
            if (value == '1') return '0';
            else return '1';
        }

        private static List<int> GetMistakeIndexes(string inputVector, string channelVector) 
        {
            var mistakeIndexes = new List<int>();
            for (int i = 0; i < inputVector.Length; i++)
            {
                if (inputVector[i] != channelVector[i])
                {
                    mistakeIndexes.Add(i);
                }
            }
            return mistakeIndexes;
        }

        private static void PrinMistakeIndexes(List<int> indexes) {
            Console.WriteLine("Mistakes were made in indexes: ");
            foreach (int i in indexes) 
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine();
        }

        public static string InputUserText()
        {
            StringBuilder inputText = new StringBuilder();
            string line;

            Console.WriteLine("Enter your text (type 'END' on a new line to finish):");

            while ((line = Console.ReadLine()) != "END")
            {
                inputText.AppendLine(line);
            }

            // Convert to string and remove the trailing new line if it exists
            string result = inputText.ToString().TrimEnd('\r', '\n');
            Console.WriteLine("\nYou entered:\n" + result);
            return result;
        }
    }
}
