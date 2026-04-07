using Avalonia.Controls;
using Demo0704.Context;
using Demo0704.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Demo0704
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<ConcelariaUserControl> concelariaUserControls { get; set; } = new ObservableCollection<ConcelariaUserControl>();
        public MainWindow()
        {
            InitializeComponent();
            UpdateItems(null);

            PriceSort.ItemsSource = new List<string>
            {
                "По умолчанию",
                "По убыванию цены",
                "По возрастанию цены"

            };
            DiscountSort.ItemsSource = new List<string>
            {
                "Все диапазоны",
                "0-9,99%",
                "10-14,99%",
                "15% и более"


            };
            DiscountSort.SelectedIndex = 0;
            PriceSort.SelectedIndex = 0;
        }

        private async void ListBox_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            if (ConcelariaListBox.SelectedItem is ConcelariaUserControl Cus)
            {
                var selectedItem = Cus.GetCurrent();
                var window = new AddEditWindow(selectedItem);
                await window.ShowDialog(this);
            }
            UpdateItems(null);
        }

        private void PriceSort_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            UpdateItems(null);
        }

        private void DiscountSort_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            UpdateItems(null);
        }

        private void Search_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            var search = Search.Text;

            UpdateItems(search);


        }
        private void UpdateItems(string? search)
        {
            var ConcelariaDB = Helper.Database.Concelaria.Include(c => c.Unit)
                .Include(c => c.Manufacturer)
                .Include(c => c.Naming)
                .Include(c => c.Postavshik)
                .Include(c => c.Category).ToList();
            var consas = ConcelariaDB;
            if (search != null)
            {
                ConcelariaDB = ConcelariaDB.Where(c => c.Manufacturer.Name.ToLower().Contains(search.ToLower()) || c.Naming.Name.ToLower().Contains(search.ToLower()) || c.Description.ToLower().Contains(search.ToLower())).ToList();
                if (consas == ConcelariaDB)
                {

                }
            }
            switch (PriceSort.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    ConcelariaDB = ConcelariaDB.OrderByDescending(c => c.Price - (c.Price / 100 * c.CurrentDiscount)).ToList();
                    break;
                case 2:
                    ConcelariaDB = ConcelariaDB.OrderBy(c => c.Price - (c.Price / 100 * c.CurrentDiscount)).ToList();
                    break;
            }
            switch (DiscountSort.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    ConcelariaDB = ConcelariaDB.Where(c => c.CurrentDiscount < 10).ToList();
                    break;
                case 2:
                    ConcelariaDB = ConcelariaDB.Where(c => c.CurrentDiscount >= 10 && c.CurrentDiscount < 15).ToList();
                    break;
                case 3:
                    ConcelariaDB = ConcelariaDB.Where(c => c.CurrentDiscount >= 15).ToList();
                    break;
            }
            concelariaUserControls.Clear();
            foreach (var concelaria in ConcelariaDB)
            {
                ConcelariaUserControl concelariaUserControl = new ConcelariaUserControl();
                concelariaUserControl.SetData(concelaria);
                concelariaUserControls.Add(concelariaUserControl);
            }
            ConcelariaListBox.ItemsSource = concelariaUserControls;
            QuantityConcelaria.Text = $"{ConcelariaDB.Count()} / {Helper.Database.Concelaria.Count()}";
        }

        private async void Add_Concelaria(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var window = new AddEditWindow();
            await window.ShowDialog(this);
            UpdateItems(null);

        }
    }
}