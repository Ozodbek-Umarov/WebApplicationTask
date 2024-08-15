using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppRestaurant.Data.Service;

namespace WpfAppRestaurant;

/// <summary>
/// Interaction logic for OrderPage.xaml
/// </summary>
public partial class OrderPage : Page
{
    private readonly int _productId;

    public OrderPage(int productId)
    {
        InitializeComponent();
        _productId = productId;
        Loaded += OrderPage_Loaded; // Ensure Load event is wired up
    }

    private async void OrderPage_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadProductDetailsAsync();
    }

    private async Task LoadProductDetailsAsync()
    {
        var apiService = new ApiService();
        var product = await apiService.GetProductByIdAsync(_productId);

        if (product != null)
        {
            ProductName.Text = product.Name;
            ProductPrice.Text = $"Price: {product.Price:C}";
            ProductDescription.Text = product.Description;
        }
    }

    private void ConfirmOrderButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Order Confirmed!");
    }
}
