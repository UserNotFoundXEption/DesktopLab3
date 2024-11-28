using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Diagnostics;
using Shared;

namespace FilmDiary;

public partial class FilmsViewModel : ObservableObject
{
    private readonly IProductService _filmService;

    [ObservableProperty]
    private ObservableCollection<Product> _films = new ObservableCollection<Product>();

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private int _rating;

    public ICommand AddFilmCommand { get; }
    public ICommand DeleteFilmCommand { get; }
    public ICommand DecreaseRatingCommand { get; }
    public ICommand IncreaseRatingCommand { get; }

    public FilmsViewModel(IProductService filmService)
    {
        _filmService = filmService;
        AddFilmCommand = new RelayCommand(AddFilm);
        DeleteFilmCommand = new RelayCommand<Product>(DeleteFilm);
        DecreaseRatingCommand = new RelayCommand<Product>(DecreaseRating);
        IncreaseRatingCommand = new RelayCommand<Product>(IncreaseRating);
        LoadFilms();
    }

    private void LoadFilms()
    {
        Films = _filmService.GetFilms();
    }

    private void AddFilm()
    {
        if (!string.IsNullOrWhiteSpace(Title) && Rating > 0 && Rating <= 10)
        {
            var newFilm = new Product { Name = Title, Price = Rating };
            _filmService.AddFilm(newFilm);
            
            Title = string.Empty;
            Rating = 0;

            // Notify changes to UI to update input fields after adding the film
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Rating));
        }
        else if (Rating < 1 || Rating > 10)
        {
            Application.Current.MainPage.DisplayAlert("Score out of range", "Please enter a valid title and a rating between 1 and 10.", "OK");
        }
    }

    private void DeleteFilm(Product film)
    {
        if (film != null)
        {
            _filmService.DeleteFilm(film);  
        }
    }

    private void IncreaseRating(Product film)
    {
        if (film != null && film.Price < 10)
        {
            film.Price++;
            _filmService.UpdateFilms();
        }
    }

    private void DecreaseRating(Product film)
    {
        if (film != null && film.Price > 1)
        {
            film.Price--;
            _filmService.UpdateFilms();
        }
    }
}
