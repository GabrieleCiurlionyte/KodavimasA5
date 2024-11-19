namespace KodavimasA5.Helpers
{
    public static class ValidatorHelper
    {
        private static readonly Dictionary<int, int> ChunkSizeCache = new Dictionary<int, int>();

        public static bool IsBinaryVectorLengthCorrect(string binaryVector, int m)
        {
            var chunkSize = GetCachedChunkSize(m);
            return binaryVector.Length % chunkSize == 0;
        }

        public static bool IsStringVectorLengthCorrect(string stringVector, int m)
        {
            var binaryVector = ConversionHelper.ConvertStringToBinary(stringVector);
            return IsBinaryVectorLengthCorrect(binaryVector, m);
        }

        public static int ZeroVectorsToAddCount(string binaryVector, int m)
        {
            var chunkSize = GetCachedChunkSize(m);
            int remainder = binaryVector.Length % chunkSize;
            return remainder == 0 ? 0 : chunkSize - remainder;
        }

        public static int GetCachedChunkSize(int m)
        {
            if (!ChunkSizeCache.TryGetValue(m, out int chunkSize))
            {
                chunkSize = GenerativeMatrixConstructor.GetReedMullerCodeDimension(m, 1);
                ChunkSizeCache[m] = chunkSize;
            }
            return chunkSize;
        }

        public static string AddAdditionBitsIfNeeded(string input, int m)
        {
                int amountOfZerosToAdd = ZeroVectorsToAddCount(input, m);
                return input + new string('0', amountOfZerosToAdd);
        }

        public static string RemoveAdditionalBitsIfNeeded(string originalInput, string newInput, int m) {

            int amountOfZerosToAdd = ZeroVectorsToAddCount(originalInput, m);
            if (amountOfZerosToAdd == 0)
            {
                return newInput;
            }
            return newInput.Substring(0, newInput.Length - amountOfZerosToAdd);
        }
    }
}
