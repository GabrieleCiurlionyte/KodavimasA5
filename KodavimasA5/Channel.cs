namespace KodavimasA5
{
    public static class Channel
    {
        private static readonly Random random = new Random();

        public static string SendThroughChannel(string input, int percentageOfMistake, int headerSize = 0)
        {
            char[] result = input.ToCharArray();

            // Skip the header portion
            for (int i = headerSize * 8; i < result.Length; i++)
            {
                // Simulate mistakes in pixel data only
                if (random.Next(100) < percentageOfMistake)
                {
                    result[i] = result[i] == '0' ? '1' : '0';
                }
            }

            var resultString = new string(result);
            Console.WriteLine("Vector received from channel:\n" + resultString);
            return resultString;
        }
    }
}
