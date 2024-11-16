using KodavimasA5.Models;

namespace KodavimasA5.Helpers
{
    public static class GenerativeMatrixConstructor
    {
        //Code length is number of columns
        public static int GetReedMullerCodeLength(int m)
        { 
            return (int)Math.Pow(2, m);
        }

        //Code dimension is number of rows
        public static int GetReedMullerCodeDimension(int m, int r)
        {
            int resultSum = 0;
            for (int i = 0; i <= r; i++)
            {
                resultSum += MathHelper.CalculateBinomialCoeficient(m, i);
            }
            return resultSum;
        }

        public static Matrix ConstructGenerativeMatrix(int r, int m)
        {
            if (r == 0)
            {
                return ConstructGenerativeMatrixBaseCase((int)Math.Pow(2, m));
            }
            if (r == 1 && m == 0) 
            {
                return new Matrix([1]);
            }

            Matrix topLeft = ConstructGenerativeMatrix(r, m - 1);
            Matrix topRight = ConstructGenerativeMatrix(r, m - 1);
            Matrix bottomRight = ConstructGenerativeMatrix(r - 1, m - 1);
            Matrix bottomLeft = ConstructZeroMatrix(bottomRight.Heigth, bottomRight.Width);

            return Matrix.Concatenate(topLeft, topRight, bottomLeft, bottomRight);
        }

        private static Matrix ConstructGenerativeMatrixBaseCase(int length)
        {
            int[,] baseMatrix = new int[1, length];
            for (int i = 0; i < length; i++)
            {
                baseMatrix[0, i] = 1;
            }
            return new Matrix(baseMatrix);
        }

        private static Matrix ConstructZeroMatrix(int rows, int cols)
        {
            return new Matrix(new int[rows, cols]);
        }
    }
}
