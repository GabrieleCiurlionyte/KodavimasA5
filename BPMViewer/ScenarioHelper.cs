using KodavimasA5;
using KodavimasA5.Helpers;

namespace BPMViewer
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
            //Convert image to binary
            var binaryString = ImageHelper.ConvertImageToBinary(imageInput);

            //Encode
            var encodedBinaryString = Encoder.Encode(binaryString, m);

            //Sending through channel
            var channelOutputWithEncoding = Channel.SendThroughChannel(binaryString, percentageOfMistake, _bmpHeaderSize);

            //Decode
            var decodedBinaryInput = Decoder.Decode(channelOutputWithEncoding, m);

            //Converting binary back to string
            var imageReceivedFromChannel = ImageHelper.ConvertBinaryToImage(decodedBinaryInput);

            return imageReceivedFromChannel;
        }
    }
}
