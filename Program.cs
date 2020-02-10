using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
	class Program
	{
		static void Main(string[] args)
		{
			List<Matrix> l = new List<Matrix> ();
			double[,] ar = new double[,] { { 1 }, { 0 }, { 1 } };
			double[,] ar1 = new double[,] { {0 }, {0 }, {1 } };
			double[,] ar2 = new double[,] { { 0}, { 1}, { 0} };
			double[,] ar3 = new double[,] { { 0}, {1 }, {1 } };
			double[,] ar4 = new double[,] { { 1}, { 0}, { 0} };
			double[,] ar5 = new double[,] { { 0}, { 0}, { 0} };
			double[,] ar6 = new double[,] { { 1}, { 1}, { 0} };
			double[,] ar7 = new double[,] { { 1}, { 1}, { 1} };

			l.Add(new Matrix(ar));
			l.Add(new Matrix(ar1));
			l.Add(new Matrix(ar2));
			l.Add(new Matrix(ar3));
			l.Add(new Matrix(ar4));
			l.Add(new Matrix(ar5));
			l.Add(new Matrix(ar6));
			l.Add(new Matrix(ar7));

			List<double> res = new List<double> { 1,1,0,0,1,0,0,1};

			PartyNN p = new PartyNN(0.1);
			int epoch = 3000;
			for (int i = 0; i < epoch; i++)
			{
				for (int j = 0; j < l.Count; j++)
				{
					p.train(l[j], res[j]);
				}
			

			}


			Console.ReadKey(true);

		}
	}
}
