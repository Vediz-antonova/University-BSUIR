using System;
using System.Diagnostics;
using System.Threading;
using IntegralCalculator;

class Program
{
	static void Main(string[] args)
	{
		IntegralCalc calculator = new IntegralCalc();
		int numThreads = 0;

		calculator.CalculationCompleted += (ticks) =>
		{
			Console.WriteLine($"Поток {Thread.CurrentThread.ManagedThreadId}: Завершен с результатом: {ticks} ticks");
		};

		calculator.ProgressChanged += (progress) =>
		{
			int threadId = Thread.CurrentThread.ManagedThreadId;

			int totalBars = 10; 
			int filledBars = (int)(progress / 10); 
			string indicator = new string('=', filledBars) + new string(' ', totalBars - filledBars);

			string message = $"Поток {threadId}: [{indicator}] {progress:F2}%\n";
			Console.SetCursorPosition(0, Console.CursorTop);
			Console.Write(message);
		};

		for (int i = 0; i < 5; i++)
		{
			Thread calculationThread = new Thread(() =>
			{
				double result = calculator.CalculateIntegral();
				numThreads++;
			});

			calculationThread.Start();
		}
	}
}