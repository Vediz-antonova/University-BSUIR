using System.Diagnostics;

namespace IntegralCalculator
{
	public class IntegralCalc
	{
		private static SemaphoreSlim semaphore = new SemaphoreSlim(2);

		public event Action<double> CalculationCompleted;
		public event Action<double> ProgressChanged;
		public double CalculateIntegral()
		{
			semaphore.Wait();

			try
			{
				double integral = 0.0;
				double x = 0.0;
				double step = 0.00000001;

				double totalSteps = 1.0 / step;

				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();

				while (x < 1.0)
				{
					integral += Math.Sin(x) * step;
					x += step;

					double progress = (x / 1.0) * 100;
					ProgressChanged?.Invoke(progress);

					for (int i = 0; i < 100000; i++)
					{
						double temp = 2 * 5;
					}
				}

				stopwatch.Stop();
				CalculationCompleted?.Invoke(stopwatch.Elapsed.Ticks);

				return integral;
			}
			finally
			{
				semaphore.Release();
			}
		}
	}
}
