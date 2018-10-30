public class Solution
    {
        public int[][] FlipAndInvertImage(int[][] A)
        {
            var rowCount = A.Length - 1;
            var columnCount = A[0].Length - 1;
            for (var index = 0; index <= rowCount; index++)
            {
                ReverseRow(A[index], columnCount);
            }

            for (var index = 0; index <= rowCount; index++)
            {
                InvertImage(A[index], columnCount);
            }

            return A;
        }

        private void ReverseRow(int[] row, int columnCount)
        {
            for (var i = 0; i <= columnCount / 2; i++)
            {
                var temp = row[i];
                row[i] = row[columnCount - i];
                row[columnCount - i] = temp;
            }
        }

        private void InvertImage(int[] row, int columnCount)
        {
            for (var i = 0; i <= columnCount; i++)
            {
                row[i] ^= 1;
            }
        }
    }