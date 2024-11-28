using ll4;

namespace FilmDiary;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddTransient<FilmsViewModel>();

        builder.Services.AddTransient<MainPage>(serviceProvider =>
        {
            var viewModel = serviceProvider.GetRequiredService<FilmsViewModel>();
            return new MainPage(viewModel);
        });

        builder.Services.AddHttpClient<ProductService>(client =>
        {
            client.BaseAddress = new Uri("http://localhost:5018/api/");
        });

        return builder.Build();
    }
}