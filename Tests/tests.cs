namespace KodavimasA5
{

    public class tests
    {
        [Theory]
        [InlineData("10101011","1100")] // Basic decoding example
        [InlineData("10001111","0001")]
        public void Decode_BasicDecoding_ReturnsCorrectOutput(string decoded, string result) {
            var decodedString = Decoder.Decode(new Random(),decoded, 3);
            Assert.Equal(result, decodedString);
        }
    }
}
