using System;

namespace JH.Applications
{
    public class SolveNbyN
    {
        int N;
        double[,] matrix;
        double[] vector;
        int[] permutation;
        double[] result;

        public void Solve(int N, double[,] matrix, double[] vector, out double[] result)
        {
            this.N = N;
            this.matrix = matrix;
            this.vector = vector;
            this.permutation = new int[N];
            this.result = new double[N];

            int pivotColumn;
            double pivotElement;

            for (int r = 0; r < N; r++)
            {
                FindPivotElement(r, out pivotColumn, out pivotElement);
                Normalize(r, pivotElement);
                Pivoting(r, pivotColumn);
            }

            CalculateResult();

            result = this.result;
        }

        private void FindPivotElement(int R, out int pivotColumn, out double pivotElement)
        {
            pivotColumn = 0;
            pivotElement = 0;

            for (int c = 0; c < N; c++)
            {
                if (Math.Abs(matrix[R, c]) > Math.Abs(pivotElement))
                {
                    pivotElement = matrix[R, c];
                    pivotColumn = c;
                }
            }

            permutation[R] = pivotColumn;

            if (pivotElement == 0)
                    throw new InvalidOperationException("Determinant is zero");
        }

        private void Normalize(int R, double pivotElement)
        {
            double norm = 1 / pivotElement;

            for (int c = 0; c < N; c++)
                matrix[R, c] *= norm;
            vector[R] *= norm;
        }

        private void Pivoting(int R, int pivotColumn)
        {
            for (int r = R + 1; r < N; r++)
            {
                double norm =  matrix[r, pivotColumn];
                if (norm == 0)
                    continue;
                norm = 1 / norm;
                for (int c = 0; c < N; c++)
                {
                    matrix[r, c] = matrix[r, c] * norm - matrix[R, c];
                }
                vector[r] = vector[r] * norm - vector[R];
            }
        }

        private void CalculateResult()
        {
            for (int r = N - 1; r >= 0; r--)
            {
                for (int c = r + 1; c < N; c++)
                {
                    vector[r] -= vector[c] * matrix[r, permutation[c]];
                }
            }

            for (int r = 0; r < N; r++)
            {
                result[permutation[r]] = vector[r];
            }

        }
    }
}
