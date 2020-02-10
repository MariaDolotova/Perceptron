using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
	class PartyNN
	{
		Matrix weights_0_1=new Matrix(2,3, true);
		Matrix weights_1_2=new Matrix(1,2,true);
		Matrix outputs_1=new Matrix(2,1,false);
		Matrix layer2_helper = new Matrix(2, 1, false);
		Matrix layer1_error = new Matrix(1, 2, false);
		Matrix gradient_layer1 = new Matrix(1, 2, false);
		Matrix weights_delta_layer1= new Matrix(1, 2, false);
		Matrix result = new Matrix(1, 1, false);
		double m_learning_rate;
		double actual_predict;


		public PartyNN(double learning_rate) { m_learning_rate = learning_rate; }

		double sigmoid(double x) { return 1 / (1 + Math.Exp(-x)); }


		public double predict(Matrix inputs) {

			outputs_1 = weights_0_1 * inputs;
			for (int i = 0; i < outputs_1.Rows; i++)
			{
				for (int j = 0; j < outputs_1.Columns; j++)
				{
					outputs_1[i,j] = sigmoid(outputs_1[i, j]);
				}
				
			}

			result= weights_1_2* outputs_1;

			actual_predict = sigmoid(result[0, 0]);

			//if (actual_predict >= 0.5) { return true; }
			return actual_predict;
			

		}




		public void train(Matrix inputs, double expected)
		{
			Console.WriteLine("actual_predict: " + predict(inputs)); 
			Console.WriteLine("expected: " + expected);
			Console.WriteLine("MSE: " + MSE(actual_predict, expected));
			double error_layer2 = actual_predict - expected;
			double gradient_layer_2 = actual_predict * (1 - actual_predict);
			double weights_delta_layer2 = error_layer2 * gradient_layer_2;
			layer2_helper = outputs_1 * (weights_delta_layer2 * m_learning_rate); // вспомогательная матрица
			weights_1_2 = weights_1_2 - layer2_helper.Transpose();


			layer1_error = weights_1_2 * weights_delta_layer2;
			for (int i = 0; i < outputs_1.Rows; i++)
			{
				for (int j = 0; j < outputs_1.Columns; j++)
				{
					gradient_layer1[j, i] = outputs_1[i, j] * (1- outputs_1[i, j]);
				}

			}

			for (int i = 0; i < weights_delta_layer1.Rows; i++)
			{
				for (int j = 0; j < weights_delta_layer1.Columns; j++)
				{
					weights_delta_layer1[i, j] = gradient_layer1[i, j] * layer1_error[i,j];
				}

			}



			for (int i = 0; i < weights_0_1.Rows; i++)
			{
				for (int j = 0; j < weights_0_1.Columns; j++)
				{
					weights_0_1[i, j] = weights_0_1[i, j] - (inputs[j, 0] * weights_delta_layer1[0,i] * m_learning_rate);
				}

			}



		}

		double MSE(double x, double y) { return Math.Pow((x - y), 2); }

	}
}
