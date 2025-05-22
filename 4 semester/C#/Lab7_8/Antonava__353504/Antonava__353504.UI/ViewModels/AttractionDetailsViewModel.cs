using Antonava__353504.Application.AttractionsUseCases.Commands;
using Antonava__353504.UI.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Xml.Linq;

namespace Antonava__353504.UI.ViewModels
{
    [QueryProperty(nameof(Attraction), "Attraction")]
    public partial class AttractionDetailsViewModel : ObservableObject
    {
        private readonly IMediator _mediator;

        public AttractionDetailsViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ObservableProperty]
        private Attraction attraction;

        [RelayCommand]
        async Task SelectAndSaveImage()
        {
            try
            {
                // Открываем диалог выбора файла (изображения)
                var result = await FilePicker.Default.PickAsync(new PickOptions
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Выберите изображение"
                });
                if (result != null)
                {
                    string imagesDirectory = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "Images");
                    if (!Directory.Exists(imagesDirectory))
                    {
                        Directory.CreateDirectory(imagesDirectory);
                    }
                    string filePath = System.IO.Path.Combine(imagesDirectory, $"{Attraction.Id}{System.IO.Path.GetExtension(result.FileName)}");

                    using (var stream = await result.OpenReadAsync())
                    using (var newStream = File.OpenWrite(filePath))
                    {
                        await stream.CopyToAsync(newStream);
                    }
                    OnPropertyChanged(nameof(Attraction));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка выбора изображения: {ex.Message}");
            }
        }

        [RelayCommand]
        async Task DeleteAttraction()
        {
            bool confirm = await Shell.Current.DisplayAlert("Подтверждение", "Вы уверены, что хотите удалить данную достопримечательность?", "Да", "Нет");
            if (!confirm) return;

            await _mediator.Send(new DeleteAttractionCommand(Attraction.Id));
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task EditAttraction()
        {
            await Shell.Current.GoToAsync($"{nameof(EditAttractionPage)}?Name={Uri.EscapeDataString(Attraction.Name)}");
        }

        [RelayCommand]
        async Task MoveAttraction()
        {
            await Shell.Current.GoToAsync($"{nameof(MoveAttractionPage)}?AttractionName={Uri.EscapeDataString(Attraction.Name)}");

        }
    }
}
