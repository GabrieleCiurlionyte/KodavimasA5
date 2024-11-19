using KodavimasA5;
using KodavimasA5.Helpers;

namespace BMPApp.Helpers
{
    public static class ScenarioHelper
    {
        private static int _bmpHeaderSize = 54;
        private static Random _random = new Random();

        public static Image? SendImageWithoutEncoding(Image imageInput, int m, int percentageOfMistake)
        {
            //Convert image to binary
            var binaryString = ImageHelper.ConvertImageToBinary(imageInput);

            //Sending through channel
            var channelOutputWihoutEncoding = Channel.SendThroughChannel(binaryString, percentageOfMistake, _bmpHeaderSize);

            //Converting binary back to string
            var imageReceivedFromChannel = ImageHelper.ConvertBinaryToImage(channelOutputWihoutEncoding);

            return imageReceivedFromChannel;
        }

        public static Image? SendImageWithEncoding(Image imageInput, int m, int percentageOfMistake)
        {

            var binaryString = ImageHelper.ConvertImageToBinary(imageInput);

            // Extract and retain header data
            var header = binaryString.Substring(0, _bmpHeaderSize * 8); // Assuming 8 bits per byte for header
            var imageData = binaryString.Substring(_bmpHeaderSize * 8);
            var imageDataWithAdditionalZeroes = ValidatorHelper.AddAdditionBitsIfNeeded(imageData, m);

            // Encode only the image data
            var encodedBinaryString = Encoder.Encode(imageDataWithAdditionalZeroes, m);

            var channelOutputWithEncoding = Channel.SendThroughChannel(encodedBinaryString, percentageOfMistake, _bmpHeaderSize);

            // Decode the received data
            var decodedBinaryInputWithAdditionalZeroes = Decoder.Decode(_random, channelOutputWithEncoding, m);
            var decodedBinary = ValidatorHelper.RemoveAdditionalBitsIfNeeded(imageData, decodedBinaryInputWithAdditionalZeroes, m);

            // Reassemble the binary string with the original header
            var completeBinaryString = header + decodedBinary;

            return ImageHelper.ConvertBinaryToImage(completeBinaryString);
        }
    }
}
