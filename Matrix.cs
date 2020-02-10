using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Matrix
    {

        double[,] data;

        public Matrix(int rows, int cols, bool b)
        {
		
            data = new double[rows, cols];
			if (b) {

				Random rnd = new Random();


				for (int i = 0; i < rows; i++)
				{
					for (int j = 0; j < cols; j++)
					{
						data[i, j] = rnd.NextDouble();
					}
				}

			}
        }

        public Matrix(double[,] initData)
        {
            int rows = initData.GetLength(0);
            int cols = initData.GetLength(1);
            data = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    data[i, j] = initData[i, j];
                }
            }
        }

        public double this[int i, int j]
        {

            get
            {
                return data[i, j];
            }

            set
            {
                data[i, j] = value;
            }

        }
	
        public int Rows { get { return data.GetLength(0); } }  // data.GetLength(0) - количество строк
        public int Columns { get { return data.GetLength(1); } }  // data.GetLength(1) - количество столбцов
        public int? Size
        {
            get
            {
                if (Rows == Columns)
                { return Rows; }
                return null;
            }

        }  // размер квадратной матрицы

        public bool IsSquared { get { return Size != null; } }
        public bool IsEmpty
        {

            get
            {

                foreach (double element in data) { if (element != 0) return false; }
                return true;
            }


        }


        public bool IsUnity  // является ли матрица единичной
        {
            get
            {
                if (IsSquared)
                {
                    int? x = Size;
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < x; j++)
                        {
                            if (i == j && data[i, j] != 1) { return false; }  // если элемент центральной диагонали не равен 1 - возвращаем false
                            if (i != j && data[i, j] != 0) { return false; } // если эл-т не центральной диагонали не равен 0 - возвращаем false
                        }

                    }

                    return true;

                }
                return false;
            } // если матрица не квадратная, возвращаем false
        }



        public bool IsDiagonal  // является ли матрица диагональной
        {
            get
            {

                if (IsSquared)
                {
                    int? x = Size;
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < x; j++)
                        {
                            if (i != j && data[i, j] != 0) { return false; } // если эл-т не центральной диагонали не равен 0 - возвращаем false
                        }

                    }

                    return true;

                }
                return false;
            }  // если матрица не квадратная, возвращаем false
        }



        public bool IsSymmetric  // является ли матрица симметричной
        {
            get
            {

                if (IsSquared)
                {
                    int? x = Size;
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < x; j++)
                        {
                            if (data[i, j] != data[j, i]) { return false; }  // если симметричные эл-ты не равны - возвращаем false

                        }

                    }

                    return true;

                }
                return false;
            } // если матрица не квадратная, возвращаем false
        }


        public static Matrix operator +(Matrix m1, Matrix m2)
        {

            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                throw new MatrixException("Sizes are not equal");
            }
            if (m1.IsEmpty) { return new Matrix(m2.data); }
            if (m2.IsEmpty) { return new Matrix(m1.data); }

            Matrix temp = new Matrix(m1.Rows, m1.Columns, false);
            if (m1.IsDiagonal && m2.IsDiagonal)
            {

                for (int i = 0; i < m1.Size; i++)
                {
                    temp[i, i] = m1[i, i] + m2[i, i];  // если матрицы диагональные, то складываем только эл-ты центральной диагонали

                }


            }
            else
            {
                for (int i = 0; i < m1.Rows; i++)
                {
                    for (int j = 0; j < m1.Columns; j++)
                    {
                        temp[i, j] = m1[i, j] + m2[i, j];
                    }
                }
            }

            return temp;
        }


        public static Matrix operator -(Matrix m1, Matrix m2)
        {

            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                throw new MatrixException("Sizes of matrixes are not equal");
            }
            if (m1.IsEmpty) { return new Matrix(m2.data); }
            if (m2.IsEmpty) { return new Matrix(m1.data); }

            Matrix temp = new Matrix(m1.Rows, m1.Columns, false);


            if (m1.IsDiagonal && m2.IsDiagonal)
            {

                for (int i = 0; i < m1.Size; i++)
                {
                    temp[i, i] = m1[i, i] - m2[i, i];  // если матрицы диагональные, то вычитаем только эл-ты центральной диагонали

                }


            }
            else
            {
                for (int i = 0; i < m1.Rows; i++)
                {
                    for (int j = 0; j < m1.Columns; j++)
                    {
                        temp[i, j] = m1[i, j] - m2[i, j];
                    }
                }
            }

            return temp;
        }

		public static Matrix operator *(Matrix m1, double d)
		{


			Matrix temp = new Matrix(m1.data);

			if (!m1.IsEmpty)    // если матрица нулевая, то просто возвращаем temp
			{
				if (m1.IsDiagonal)
				{
					for (int i = 0; i < m1.Size; i++)
					{
						temp[i, i] = m1[i, i] * d;  // если матрица диагональная, то умножаем только эл-ты центральной диагонали

					}

				}
				for (int i = 0; i < m1.Rows; i++)
				{
					for (int j = 0; j < m1.Columns; j++)
					{
						temp[i, j] = m1[i, j] * d;
					}
				}
			}
			return temp;
		}



		//public static bool operator *(Matrix m1, double d)
		//{




		//	if (!m1.IsEmpty)    // если матрица нулевая, то просто возвращаем temp
		//	{
		//		if (m1.IsDiagonal)
		//		{
		//			for (int i = 0; i < m1.Size; i++)
		//			{
		//				m1[i, i] = m1[i, i] * d;  // если матрица диагональная, то умножаем только эл-ты центральной диагонали

		//			}

		//		}
		//		for (int i = 0; i < m1.Rows; i++)
		//		{
		//			for (int j = 0; j < m1.Columns; j++)
		//			{
		//				m1[i, j] = m1[i, j] * d;
		//			}
		//		}
		//	}
		//	return true;
		//}



		public static Matrix operator *(Matrix m1, Matrix m2)
        {

            if (m1.Columns != m2.Rows)
            {
                throw new MatrixException("Columns of matrix1 is not equal with rows of matrix2");
            }
            Matrix temp = new Matrix(m1.Rows, m2.Columns, false);
            double res = 0;
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    res = m1[i, j];
                    for (int k = 0; k < m2.Columns; k++)
                    {
                        temp[i, k] += res * m2[j, k];

                    }
                }
            }
            return temp;



            //for (int i = 0; i < size; i++)
            //    for (int k = 0; k < size; k++)
            //    {
            //        r = A[i][k];
            //        for (int j = 0; j < size; j++)
            //            C[i][j] += r * B[k][j];
            //    }

        }

        public static explicit operator Matrix(double[,] arr)
        {

            return new Matrix(arr);

        }

        public Matrix Transpose()
        {


            Matrix temp = new Matrix(Columns, Rows, false);
            if (!IsSymmetric)  // если матрица симметричная, то ее и возвращаем
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        temp[j, i] = data[i, j];
                    }
                }
            }
            return temp;
        }

        public double Trace()
        {

            if (!IsSquared)
            {
                throw new MatrixException("Matrix is not squared - determining trace is not possible");
            }
            if (IsEmpty) { return 0; }
            if (IsUnity) { return (double)Size; }
            double sum = 0;
            for (int i = 0; i < Size; i++)
            {
                sum += data[i, i];
            }
            return sum;
        }

        public override string ToString()
        {
            // сборка в одну строку - все числа через запятую
            //string s;                           
            //string[] arr = new string[Rows * Columns];
            //int i = 0;
            //foreach (double x in data)
            //{

            //    arr[i] = x.ToString();
            //    i++;

            //}
            //s = string.Join(", ", arr);

            //return s;

            // числа одной строки через пробел, строки через запятую
            string s;
            string[] arr_cols=new string[Columns];
            string[] arr_rows = new string[Rows];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    arr_cols[j] = data[i, j].ToString();
                }
                arr_rows[i]= string.Join(" ", arr_cols);
            }
            s= string.Join(",", arr_rows);
            return s;

            //// через StringBuilder, сборка в одну строку - все числа через запятую
            //StringBuilder sb=new StringBuilder();
            //foreach (double x in data) {
            //    sb.Append(x);
            //    sb.Append(',');
            //}
            //sb.Remove((sb.Length - 1), 1);

            //return sb.ToString();
        }


        public static Matrix GetEmpty(int Size)
        {

            return new Matrix(Size, Size, false);
            

        }

        public static Matrix GetUnity(int Size)
        {

            Matrix temp = new Matrix(Size, Size, false);
            for (int i = 0; i < Size; i++)
            {
                temp.data[i, i] = 1;
            }

            return temp;

        }

        public static Matrix Parse(string s)
        {
            string[] result = s.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);  // массив строк
            string[] res;
            int rows = result.Length;
            int cols = result[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;  // количество колонок в первой строке
            Matrix temp = new Matrix(rows, cols, false);

            for (int i = 0; i < rows; i++)
            {
                res = result[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (res.Length != cols) { throw new FormatException("Inconsistent number of columns"); }

                for (int j = 0; j < cols; j++)
                {
                    try
                    {
                        temp[i, j] = double.Parse(res[j]);
                    }

                    catch (Exception e) { throw new FormatException("Incorrect format"); }

                }

            }


            return temp;
        }


        public static bool TryParse(string s, out Matrix m)
        {

            try
            {
                m = Parse(s);
                return true;
            }
            catch (FormatException e) { Console.WriteLine(e.Message); m = null; return false; }

        }

    }
}
