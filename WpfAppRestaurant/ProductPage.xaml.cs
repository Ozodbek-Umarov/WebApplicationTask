using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfAppRestaurant;

public partial class ProductsPage : Page
{
    public ProductsPage()
    {
        InitializeComponent();
    }

    private void DecreaseQuantity_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is TextBlock textBlock)
        {
            var parent = (StackPanel)textBlock.Parent;
            var quantityTextBox = (TextBox)parent.FindName("QuantityTextBox");
            if (int.TryParse(quantityTextBox.Text, out int quantity) && quantity > 1)
            {
                quantityTextBox.Text = (quantity - 1).ToString();
            }
        }
    }

    private void IncreaseQuantity_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is TextBlock textBlock)
        {
            var parent = (StackPanel)textBlock.Parent;
            var quantityTextBox = (TextBox)parent.FindName("QuantityTextBox");
            if (int.TryParse(quantityTextBox.Text, out int quantity))
            {
                quantityTextBox.Text = (quantity + 1).ToString();
            }
        }
    }
}
