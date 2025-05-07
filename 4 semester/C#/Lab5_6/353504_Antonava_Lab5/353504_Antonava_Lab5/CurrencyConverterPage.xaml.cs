using _353504_Antonava_Lab5.Services;
using _353504_Antonava_Lab5.Entities;
namespace _353504_Antonava_Lab5;

public partial class CurrencyConverterPage : ContentPage
{
    private readonly IRateService _rateService;
    private Dictionary<string, Rate> _currentRates = new Dictionary<string, Rate>();

    public CurrencyConverterPage(IRateService rateService)
    {
        InitializeComponent();
        _rateService = rateService;
        currencyPicker.ItemsSource = new List<string> { "RUB", "EUR", "USD", "CHF", "CNY", "GBP" };
    }

    private async void OnLoadRatesClicked(object sender, EventArgs e)
    {
        var selectedDate = datePicker.Date;
        var rates = await _rateService.GetRates(selectedDate);
        ratesCollectionView.ItemsSource = rates;

        _currentRates = rates.ToDictionary(r => r.Cur_Abbreviation, StringComparer.OrdinalIgnoreCase);
    }

    private void OnConvertToForeignClicked(object sender, EventArgs e)
    {
        if (!decimal.TryParse(bynEntry.Text, out var amountByn))
        {
            DisplayAlert("Ошибка", "Введите корректную сумму в белорусских рублях", "OK");
            return;
        }

        if (currencyPicker.SelectedItem is string currencyCode &&
            _currentRates.TryGetValue(currencyCode, out Rate rate))
        {
            decimal conversionFactor = rate.Cur_OfficialRate / rate.Cur_Scale;
            decimal foreignAmount = amountByn / conversionFactor;
            resultEntry.Text = foreignAmount.ToString("F2");
        }
        else
        {
            DisplayAlert("Ошибка", "Выберите валюту и загрузите курсы", "OK");
        }
    }

    private void OnConvertToBynClicked(object sender, EventArgs e)
    {
        if (!decimal.TryParse(resultEntry.Text, out var foreignAmount))
        {
            DisplayAlert("Ошибка", "Введите корректную сумму в иностранной валюте в поле Результат", "OK");
            return;
        }

        if (currencyPicker.SelectedItem is string currencyCode &&
            _currentRates.TryGetValue(currencyCode, out Rate rate))
        {
            decimal conversionFactor = rate.Cur_OfficialRate / rate.Cur_Scale;
            decimal amountByn = foreignAmount * conversionFactor;
            bynEntry.Text = amountByn.ToString("F2");
        }
        else
        {
            DisplayAlert("Ошибка", "Выберите валюту и загрузите курсы", "OK");
        }
    }
}