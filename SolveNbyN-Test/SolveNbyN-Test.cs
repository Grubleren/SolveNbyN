using System;
using System.Windows.Forms;

namespace JH.Applications
{
    public partial class SolveNbyN_Test: Form
    {
        public SolveNbyN_Test()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int N = 5;
            double[,] matrix = { 
                               { 11, 18, 25,  2,  9 }, 
                               { 10, 12, 19, 21,  3 }, 
                               {  4,  6, 13, 20, 22 }, 
                               { 23,  5,  7, 14, 16 }, 
                               { 17, 24,  1,  8, 15 } 
                               };
            double[] vector = { 1, 4, 9, 16, 25 };
            double[] result;
            double[] expectedResult = { 0.3846, 0.4923, -0.6308, 0.2462, 0.3538 };
            SolveNbyN solve = new SolveNbyN();

            solve.Solve(N, matrix, vector, out result);

            for (int i = 0; i < N; i++)
                if (Math.Abs(result[i] - expectedResult[i]) > 0.0001)
                    throw new InvalidProgramException("Error in calculation");

            N = 50;
            matrix = new double[N, N];
            vector = new double[N];
            double[,] matrix1;
            double[] vector1;
            Random random = new Random();

            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                    matrix[r, c] = random.NextDouble();
                vector[r] = (r + 1) * (r + 1) * random.NextDouble();
            }
            matrix1 = (double[,])matrix.Clone();
            vector1 = (double[])vector.Clone();

            solve.Solve(N, matrix, vector, out result);

            for (int r = 0; r < N; r++)
            {
                double sum = 0;
                for (int c = 0; c < N; c++)
                    sum += matrix1[r, c] * result[c];
                if (Math.Abs(vector1[r] - sum) > 1e-11)
                    throw new InvalidProgramException("Error in calculation");
            }

            this.Close();
        }
    }
}
