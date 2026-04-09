using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Demo0704.Context;
using Demo0704.Models;
using MsBox.Avalonia;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Demo0704;

public partial class AddEditWindow : Window
{
    public ObservableCollection<Unit> Units { get; set; } = new ObservableCollection<Unit>();
    public ObservableCollection<Manufacturer> Manufacturers { get; set; } = new ObservableCollection<Manufacturer>();
    public ObservableCollection<Postavshiki> Postavshiki { get; set; } = new ObservableCollection<Postavshiki>();
    public ObservableCollection<Category> Categories { get; set; } = new ObservableCollection<Category>();
    public ObservableCollection<Naming> Namings { get; set; } = new ObservableCollection<Naming>();
    
    private Concelarium _editconcelaria = null;
    public AddEditWindow()
    {
       
        InitializeComponent();
        FillLists();
        DataContext = new Concelarium();
    }
    public AddEditWindow(Concelarium editItem)
    {
        
        _editconcelaria = editItem;
        InitializeComponent();
        FillLists();
        Title.Text = $"Редактирование товара #{editItem.Article}";
        DataContext = editItem;
       
        Naming.SelectedItem = editItem.Naming;
        Unit.SelectedItem = editItem.Unit;
        Price.Text = editItem.Price.ToString();
        MaxDiscount.Text = editItem.MaxDiscount.ToString();
        Manufacturer.SelectedItem = editItem.Manufacturer;
        Postavshik.SelectedItem = editItem.Postavshik;
        Category.SelectedItem = editItem.Category;
        CurrentDiscount.Text = editItem.CurrentDiscount.ToString();
        QuantityInStorage.Text = editItem.QuantityInStorage.ToString();
        Description.Text = editItem.Description;
        

    }

    private async void AddOrEdit(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if(_editconcelaria != null)
        {
            try{
                _editconcelaria.Article = Article.Text;
                _editconcelaria.Naming = (Naming?)Naming.SelectedItem;
                _editconcelaria.Unit = (Unit?)Unit.SelectedItem;
                _editconcelaria.Manufacturer = (Manufacturer?)Manufacturer.SelectedItem;
                _editconcelaria.Postavshik = (Postavshiki?)Postavshik.SelectedItem;
                _editconcelaria.Category = (Category?)Category.SelectedItem;
                _editconcelaria.Price = Convert.ToDouble(Price.Text);
                _editconcelaria.CurrentDiscount = Convert.ToDouble(CurrentDiscount.Text);
                _editconcelaria.MaxDiscount = Convert.ToDouble(MaxDiscount.Text);
                _editconcelaria.QuantityInStorage = Convert.ToInt32(QuantityInStorage.Text);
                _editconcelaria.Description = Description.Text;
                _editconcelaria.Image = null;
                Helper.Database.Concelaria.Update(_editconcelaria);
                await Helper.Database.SaveChangesAsync();
            }
            catch
            {
                var dialogWindowError = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Ошибка при обновлении товара \n проверьте правильность введенных данных", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await dialogWindowError.ShowWindowDialogAsync(this);
                return;
            }
            var dialogWindow = MessageBoxManager.GetMessageBoxStandard("Успешно", "Товар успешно обновлен", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Success);
            var result = await dialogWindow.ShowWindowDialogAsync(this);
            if(result == MsBox.Avalonia.Enums.ButtonResult.Ok || result == MsBox.Avalonia.Enums.ButtonResult.Abort)
            {
                Close();
            }
        }
        else
        {
            try
            {
                var item = DataContext as Concelarium;
                _editconcelaria = new Concelarium();
                _editconcelaria.Article = Article.Text;
                _editconcelaria.Naming = (Naming?)Naming.SelectedItem;
                _editconcelaria.Unit = (Unit?)Unit.SelectedItem;
                _editconcelaria.Manufacturer = (Manufacturer?)Manufacturer.SelectedItem;
                _editconcelaria.Postavshik = (Postavshiki?)Postavshik.SelectedItem;
                _editconcelaria.Category = (Category?)Category.SelectedItem;
                _editconcelaria.Price = Convert.ToDouble(Price.Text);
                _editconcelaria.CurrentDiscount = Convert.ToDouble(CurrentDiscount.Text);
                _editconcelaria.MaxDiscount = Convert.ToDouble(MaxDiscount.Text);
                _editconcelaria.QuantityInStorage = Convert.ToInt32(QuantityInStorage.Text);
                _editconcelaria.Description = Description.Text;
                _editconcelaria.Image = null;
                Helper.Database.Concelaria.Add(_editconcelaria);
                await Helper.Database.SaveChangesAsync();
            }
            catch
            {
                var dialogWindowError = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Ошибка при добавлении товара \n проверьте правильность введенных данных", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await dialogWindowError.ShowWindowDialogAsync(this);
                return;
            }
            var dialogWindow = MessageBoxManager.GetMessageBoxStandard("Успешно", "Товар успешно добавлен", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Success);
            var result = await dialogWindow.ShowWindowDialogAsync(this);
            if (result == MsBox.Avalonia.Enums.ButtonResult.Ok || result == MsBox.Avalonia.Enums.ButtonResult.Abort)
            {
                Close();
            }
        }
    }

    private void Cancel(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Close();
    }
    private void FillLists()
    {
        foreach(var unit in Helper.Database.Units.ToList())
        {
            Units.Add(unit);
        }
        foreach(var manufacturer in Helper.Database.Manufacturers.ToList())
        {
            Manufacturers.Add(manufacturer);
        }
        foreach(var postavshik in Helper.Database.Postavshikis.ToList())
        {
            Postavshiki.Add(postavshik);
        }
        foreach(var category in Helper.Database.Categories.ToList())
        {
            Categories.Add(category);
        }
        foreach(var naming in Helper.Database.Namings.ToList())
        {
            Namings.Add(naming);
        } 
        Unit.ItemsSource = Units;
        Manufacturer.ItemsSource = Manufacturers;
        Postavshik.ItemsSource = Postavshiki;
        Category.ItemsSource = Categories;
        Naming.ItemsSource = Namings;
    }
}