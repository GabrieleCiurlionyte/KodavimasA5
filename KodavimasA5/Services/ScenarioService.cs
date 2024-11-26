using KodavimasA5.Helpers;
using System.Diagnostics;

namespace KodavimasA5.Services
{
    public class ScenarioService
    {
        private static Random _random;
        public ScenarioService(Random random)
        {
            _random = random;
        }

        public void ExecuteFirstScenario(int m, double percentageOfMistake) 
        {
            var binaryVector = ConsoleWriteHelper.EnterBinaryVector(m);
            var binaryVectorWithAdditionalZeroes = ValidatorHelper.AddAdditionBitsIfNeeded(binaryVector, m);

            var encodedVector = Encoder.Encode(binaryVectorWithAdditionalZeroes, m);
            Console.WriteLine("Encoded vector:\n" + encodedVector);
            string channelVector = Channel.SendThroughChannel(encodedVector, percentageOfMistake);
            ConsoleWriteHelper.PrintBinaryVectorMistakes(encodedVector, channelVector);
            var fixedVector = ConsoleWriteHelper.FixBinaryVectorMistakes(encodedVector, channelVector);
            var decodedVector = Decoder.Decode(_random, fixedVector, m);

            decodedVector = ValidatorHelper.RemoveAdditionalBitsIfNeeded(binaryVector, decodedVector, m);
            Console.WriteLine("Decoded vector:\n" + decodedVector);
        }

        public void ExecuteSecondScenario(int m, double percentageOfMistake)
        {
            var input = ConsoleWriteHelper.InputUserText();
            if (input == null) {
                return;
            }

            ExecuteSecondScenarioPart1(m, percentageOfMistake, input);
            ExecuteSecondScenarioPart2(m, percentageOfMistake, input);
        }

        private static void ExecuteSecondScenarioPart1(int m, double percentageOfMistake, string input)
        {
            Console.WriteLine("\nSCENARIO 2 PART 1\n Sending text without encoding to channel....");

            //Conversion to binary
            var binaryInput = ConversionHelper.ConvertStringToBinary(input);

            //Sending through channel
            var channelInputWihoutEncoding = Channel.SendThroughChannel(binaryInput, percentageOfMistake);

            //Converting binary back to string
            var stringReceivedFromChannel = ConversionHelper.ConvertBinaryToString(channelInputWihoutEncoding);

            Console.WriteLine("Result wihout encoding");
            Console.WriteLine(stringReceivedFromChannel);
        }

        private static void ExecuteSecondScenarioPart2(int m, double percentageOfMistake, string input)
        {
            Console.WriteLine("\n SCENARIO 2 PART 2\n Sending text with encoding to channel....");
            var binaryInput = ConversionHelper.ConvertStringToBinary(input);

            if (!ValidatorHelper.IsBinaryVectorLengthCorrect(input, m))
            {
                Console.WriteLine("Incorrect length, we will add additional '0' bits for encoding and decoding");
            }

            var binaryStringWithAdditionalZeroes = ValidatorHelper.AddAdditionBitsIfNeeded(binaryInput, m);
            var encodedVector = Encoder.Encode(binaryStringWithAdditionalZeroes, m);
            var channelOutputWithEncoding = Channel.SendThroughChannel(encodedVector, percentageOfMistake);
            var decodedVector = Decoder.Decode(_random, channelOutputWithEncoding, m);
            decodedVector = ValidatorHelper.RemoveAdditionalBitsIfNeeded(binaryInput, decodedVector, m);

            var decodedString = ConversionHelper.ConvertBinaryToString(decodedVector);
            Console.WriteLine("Result with encoding");
            Console.WriteLine(decodedString);
        }

        public void ExecuteThirdScenario(int m, double percentageOfMistake)
        {
            string fullPath = GetBMPViewerPath();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = fullPath,
                Arguments = $"{m} {percentageOfMistake}",
                UseShellExecute = true              
            };

            using (Process process = Process.Start(startInfo))
            {
                // Wait for the process to exit
                Console.WriteLine("Close the BMP Image Viewer window, to return to new scenario selection");
                process.WaitForExit();
            }
        }

        private string GetBMPViewerPath()
        {
            // Construct the relative path to BMPApp.exe
            string relativePath = @"..\..\..\..\BMPApp\bin\Debug\net8.0-windows\BMPApp.exe";

            string fullPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), relativePath));

            return fullPath;
        }
    }
}
