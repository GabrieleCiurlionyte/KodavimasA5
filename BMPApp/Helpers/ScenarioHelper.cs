using KodavimasA5;

namespace BMPApp.Helpers
{
    public static class ScenarioHelper
    {
        private static int _bmpHeaderSize = 54;

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

            // Encode only the image data
            var encodedBinaryString = Encoder.Encode(imageData, m);

            var channelOutputWithEncoding = Channel.SendThroughChannel(encodedBinaryString, percentageOfMistake, _bmpHeaderSize);

            // Decode the received data
            var decodedBinaryInput = Decoder.Decode(channelOutputWithEncoding, m);

            // Reassemble the binary string with the original header
            var completeBinaryString = header + decodedBinaryInput;

            return ImageHelper.ConvertBinaryToImage(completeBinaryString);
        }
    }
}
