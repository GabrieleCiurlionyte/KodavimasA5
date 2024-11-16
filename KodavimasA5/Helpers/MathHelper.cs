using KodavimasA5.Models;

namespace KodavimasA5.Helpers
{
    public static class MathHelper
    {
        private static Matrix _hMatrix = new Matrix(new int[,]{
            {1, 1},
            {1,-1}
        });

        public static Matrix CalculateH(int i, int m)
        {
            var firstIdentityMatrix = GenerateIdentitityMatrix((int)Math.Pow(2, m - i));
            var secondIdentityMatrix = GenerateIdentitityMatrix((int)Math.Pow(2, i - 1));

            var resultMatrix = MultipleMatrixMultiplication(firstIdentityMatrix, _hMatrix);
            resultMatrix = MultipleMatrixMultiplication(resultMatrix, secondIdentityMatrix);
            return resultMatrix;
        }

        public static Matrix GenerateIdentitityMatrix(int length)
        {
            int[,] identityMatrix = new int[length, length];
            for (int i = 0; i < length; i++)
            {
                identityMatrix[i, i] = 1;
            }
            return new Matrix(identityMatrix);
        }

        public static int[] MultiplyMatrixWithOneDimensionArray(Matrix matrix, int[] array) 
        {
            var resultMatrix = new int[matrix.Width];
            for (int i = 0; i < matrix.Width; i++)
            {
                int resultValue = 0;
                for (int j = 0; j < matrix.Heigth; j++)
                {
                    resultValue += matrix.Value[j, i] * array[j];
                }
                resultMatrix[i] = resultValue;
            }
            return resultMatrix;
        }

        private static Matrix MultipleMatrixMultiplication(Matrix leftMatrix, Matrix rigthMatrix)
        {
            var resultMatrix = CreateEmptyResultMatrix(leftMatrix, rigthMatrix);

            for (int i = 0; i < leftMatrix.Width; i++)
            {
                for (int j = 0; j < leftMatrix.Heigth; j++)
                {
                    //We iterate through matrix to multiple items

                    var matrixValueAtIndex = leftMatrix.Value[i, j];

                    var matrixMultiplied = NewMultipliedMatrixWithInteger(rigthMatrix, matrixValueAtIndex);

                    var coordinates = new Coordinates(i, j);
                    resultMatrix = CopyMatrixToResultMatrix(resultMatrix, matrixMultiplied, coordinates);
                }
            }
            return resultMatrix;
        }

        private static Matrix CopyMatrixToResultMatrix(Matrix resultMatrix, Matrix matrixToAdd, Coordinates coordinates)
        {

            var startHeigth = coordinates.GetHeigth() * matrixToAdd.Heigth;
            var startWidth = coordinates.GetWidth() * matrixToAdd.Width;

            for (int i = 0; i < matrixToAdd.Heigth; i++)
            {

                for (int j = 0; j < matrixToAdd.Width; j++)
                {
                    resultMatrix.Value[startHeigth + i, startWidth + j] = matrixToAdd.Value[i, j];
                }

            }
            return resultMatrix;
        }

        private static Matrix CreateEmptyResultMatrix(Matrix leftMatrix, Matrix rigthMatrix)
        {
            var elemCount = leftMatrix.Value.Length;
            int[,] resultMatrixValue = new int[leftMatrix.Heigth * rigthMatrix.Heigth,
                leftMatrix.Width * rigthMatrix.Width];
            return new Matrix(resultMatrixValue);
        }

        private static Matrix CreateEmptyResultMatrixForH(Matrix leftMatrix, Matrix middleMatrix, Matrix rigthMatrix)
        {
            var firstActionResult = CreateEmptyResultMatrix(leftMatrix, middleMatrix);
            return CreateEmptyResultMatrix(firstActionResult, rigthMatrix);
        }

        private static Matrix NewMultipliedMatrixWithInteger(Matrix matrix, int number)
        {
            var resultMatrix = new Matrix(new int[matrix.Heigth, matrix.Width]);
            for (int i = 0; i < matrix.Width; i++)
            {
                for (int j = 0; j < matrix.Heigth; j++)
                {
                    resultMatrix.Value[i, j] = matrix.Value[i, j] * number;
                }
            }
            return resultMatrix;
        }

        //In Reed-Muller code task - r is always equal to 1.
        public static int CalculateBinomialCoeficient(int m, int i)
        {
            return CalculateFactorial(m) / (CalculateFactorial(i) * CalculateFactorial(m - i));
        }

        private static int CalculateFactorial(int number)
        {
            return number <= 1 ? 1 : number * CalculateFactorial(number - 1);
        }
    }
}
