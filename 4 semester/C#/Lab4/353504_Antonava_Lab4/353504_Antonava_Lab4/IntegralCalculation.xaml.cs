using System;

namespace _353504_Antonava_Lab4;

public partial class IntegralCalculation : ContentPage
{
    private CancellationTokenSource _cancellationTokenSource;

    public IntegralCalculation()
    {
        InitializeComponent();
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        StatusLabel.Text = "Вычисление";

        try
        {
            double result = await CalculateIntegralAsync(0, 1, 0.00000001, _cancellationTokenSource.Token);
            StatusLabel.Text = $"Результат: {result}";
        }
        catch (OperationCanceledException)
        {
            StatusLabel.Text = "Задание отменено";
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Ошибка: {ex.Message}";
        }
        finally
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
    }

    private async Task<double> CalculateIntegralAsync(double start, double end, double step, CancellationToken token)
    {
        double result = 0.0;
        double totalSteps = (end - start) / step;
        double progress = 0.0;

        for (double x = start; x < end; x += step)
        {
            token.ThrowIfCancellationRequested();

            result += Math.Sin(x) * step;

            for (int i = 0; i < 1000; i++)
            {
                _ = 1.0 * 2.0; 
            }

            progress += 1;
            if (progress % 1000 == 0)
            {
                double percentage = progress / totalSteps;
                Progress.Progress = percentage;
                ProgressPercentageLabel.Text = $"{Math.Round(percentage * 100, 2)}%";

                await Task.Delay(10);
            }
        }

        return result;
    }
}