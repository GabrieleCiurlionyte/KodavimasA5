﻿using KodavimasA5.Helpers;

namespace KodavimasA5.Services
{
    public class ScenarioService
    {
        public void ExecuteFirstScenario(int m, int percentageOfMistake) 
        {
            var binaryVector = ConsoleWriteHelper.EnterBinaryVector(m);
            var encodedVector = Encoder.Encode(binaryVector, m);
            Console.WriteLine(encodedVector);
            string channelVector = Channel.SendThroughChannel(encodedVector, percentageOfMistake);
            ConsoleWriteHelper.PrintBinaryVectorMistakes(encodedVector, channelVector);
            ConsoleWriteHelper.FixBinaryVectorMistakes(encodedVector, channelVector);
            var decodedVector = Decoder.Decode(channelVector, m);
            Console.WriteLine(decodedVector);
        }

        public void ExecuteSecondScenario(int m, int percentageOfMistake)
        {
            var input = GetInputText(m);
            if (input == null) {
                return;
            }

            ExecuteSecondScenarioPart1(m, percentageOfMistake, input);
            ExecuteSecondScenarioPart2(m, percentageOfMistake, input);
        }

        private static string? GetInputText(int m) 
        {
            var input = ConsoleWriteHelper.InputUserText();
            if (!ValidatorHelper.IsStringVectorLengthCorrect(input, m))
            {
                // TODO: write why incorrect
                Console.WriteLine("The input is not correct length");
                return null;
            }
            return input;
        }

        private static void ExecuteSecondScenarioPart1(int m, int percentageOfMistake, string input)
        {
            Console.WriteLine("Sending text without encoding to channel....");

            //Conversion to binary
            var binaryInput = ConversionHelper.ConvertStringToBinary(input);

            //Sending through channel
            var channelInputWihoutEncoding = Channel.SendThroughChannel(binaryInput, percentageOfMistake);

            //Converting binary back to string
            var stringReceivedFromChannel = ConversionHelper.ConvertBinaryToString(channelInputWihoutEncoding);

            Console.WriteLine("Result wihout encoding");
            Console.WriteLine(stringReceivedFromChannel);
        }

        private static void ExecuteSecondScenarioPart2(int m, int percentageOfMistake, string input)
        {
            Console.WriteLine("Sending text with encoding to channel....");
            var binaryInput = ConversionHelper.ConvertStringToBinary(input);
            if (ValidatorHelper.IsBinaryVectorLengthCorrect(input, m)) {
                Console.WriteLine("Incorrect length");
            }

            var encodedVector = Encoder.Encode(binaryInput, m);
            var channelInputWithEncoding = Channel.SendThroughChannel(encodedVector, percentageOfMistake);
            var decodedVector = Decoder.Decode(channelInputWithEncoding, m);
            var decodedString = ConversionHelper.ConvertBinaryToString(decodedVector);

            Console.WriteLine("Result with encoding");
            Console.WriteLine(decodedString);
        }

        public void ExecuteThirdScenario(int m, int percentageOfMistake)
        {
            throw new NotImplementedException();
        }
    }
}
