using System;
using System.Globalization;

namespace _353504_Antonava_Lab5
{
    public partial class Calculator : ContentPage
    {
        private string input = string.Empty;
        private string operatorUsed = string.Empty;
        private double firstNumber;

        public Calculator()
        {
            InitializeComponent();
        }

        private void OnDigitButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                input += button.Text;
                UpdateDisplay(input);
            }
        }

        private void OnOperatorButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                operatorUsed = button.Text;
                if (double.TryParse(input, out firstNumber))
                {
                    input = string.Empty;
                }
                UpdateDisplay("0");
            }
        }

        private void OnEqualButtonClicked(object sender, EventArgs e)
        {
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double secondNumber))
            {
                double result = operatorUsed switch
                {
                    "+" => firstNumber + secondNumber,
                    "-" => firstNumber - secondNumber,
                    "*" => firstNumber * secondNumber,
                    "/" => firstNumber / secondNumber,
                    "%" => firstNumber % secondNumber,
                    _ => 0
                };
                input = result.ToString(CultureInfo.InvariantCulture);
                UpdateDisplay(input);
            }
        }

        private void OnClearButtonClicked(object sender, EventArgs e)
        {
            input = string.Empty;
            operatorUsed = string.Empty;
            firstNumber = 0;
            UpdateDisplay("0");
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = input.Remove(input.Length - 1);
                UpdateDisplay(string.IsNullOrEmpty(input) ? "0" : input);
            }
        }

        private void UpdateDisplay(string value)
        {
            ((Entry)Content.FindByName("DisplayEntry")).Text = value;
        }
    }
}
