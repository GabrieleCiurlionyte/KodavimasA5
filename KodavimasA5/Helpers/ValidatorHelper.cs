namespace KodavimasA5.Helpers
{
    public static class ValidatorHelper
    {
        public static bool IsBinaryVectorLengthCorrect(string binaryVector, int m)
        {
            //The binary has to be of able to be split into code dimension chunks
            var chunkSize = GenerativeMatrixConstructor.GetReedMullerCodeDimension(m, 1);
            return binaryVector.Length % chunkSize == 0;
        }

        public static bool IsStringVectorLengthCorrect(string stringVector, int m)
        {
            var binaryVector = ConversionHelper.ConvertStringToBinary(stringVector);
            return IsBinaryVectorLengthCorrect(binaryVector, m);
        }
    }
}
