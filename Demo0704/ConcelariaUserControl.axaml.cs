using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Demo0704.Models;
using SkiaSharp;

namespace Demo0704;

public partial class ConcelariaUserControl : UserControl
{
    public Concelarium _concelaria {  get; set; } 
    public ConcelariaUserControl()
    {
        InitializeComponent();
    }
    public void SetData(Concelarium concelarium)
    {
        _concelaria = concelarium;
        Naming.Text = $"Наименование: {concelarium.Naming.Name}";
        Description.Text = $"Описание: {concelarium.Description}";
        Manufacturer.Text = $"Производитель: {concelarium.Manufacturer.Name}";
        Price.Text = $"Цена: {concelarium.Price}";
        Discount.Text = $"{concelarium.CurrentDiscount}%";
        if (concelarium.CurrentDiscount > 0)
        {
            PriceNew.Text = (concelarium.Price - (concelarium.Price / 100 * concelarium.CurrentDiscount)).ToString();
            Price.TextDecorations = TextDecorations.Strikethrough;
            PriceNew.IsVisible = true;
        }
        if (concelarium.CurrentDiscount >= 15)
        {
            Back.Background = new SolidColorBrush(Color.Parse("#7fff00"));
        }

        var fallback = new Bitmap(AssetLoader.Open(new System.Uri("avares://Demo0704/Assets/picture.png")));
        if (concelarium.Image != null)
        {
            try
            {
                Picture.Source = new Bitmap(AssetLoader.Open(new System.Uri($"avares://Demo0704/Assets/{concelarium.Image}")));
            }
            catch
            {
                Picture.Source = fallback;
            }
        }
        else
        {
            Picture.Source = fallback;
        }
    }
    public Concelarium GetCurrent()
    {
        return _concelaria;
    }
}