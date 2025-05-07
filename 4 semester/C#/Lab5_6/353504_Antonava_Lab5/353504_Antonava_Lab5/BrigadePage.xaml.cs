using System.Collections.ObjectModel;
using _353504_Antonava_Lab5.Services;
using _353504_Antonava_Lab5.Entities;
namespace _353504_Antonava_Lab5;

public partial class BrigadePage : ContentPage
{
    private readonly IDbService _dbService;

    public ObservableCollection<Brigade> Brigades { get; set; }
    public ObservableCollection<Work> Works { get; set; }

    public BrigadePage(IDbService dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        Brigades = new ObservableCollection<Brigade>();
        Works = new ObservableCollection<Work>();

        this.Loaded += BrigadePage_Loaded;
    }

    private void BrigadePage_Loaded(object sender, EventArgs e)
    {
        var brigades = _dbService.GetAllBrigades();
        Brigades.Clear();
        foreach (var brigade in brigades)
        {
            Brigades.Add(brigade);
        }
        brigadePicker.ItemsSource = Brigades;
        brigadePicker.ItemDisplayBinding = new Binding("Name");
    }

    private void BrigadePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (brigadePicker.SelectedItem is Brigade selectedBrigade)
        {
            var works = _dbService.GetWorksByBrigade(selectedBrigade.Id);
            Works.Clear();
            foreach (var work in works)
            {
                Works.Add(work);
            }
            worksCollectionView.ItemsSource = Works;
        }
    }
}