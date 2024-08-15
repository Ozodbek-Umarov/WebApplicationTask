using System.Windows;
using System.Windows.Controls;

namespace WpfAppTask;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnRadioButtonChecked(object sender, RoutedEventArgs e)
    {
        if (ProductRadioButton.IsChecked == true)
        {
            ContentFrame.Navigate(new ProductPage());
        }
        else if (CategoryRadioButton.IsChecked == true)
        {
            ContentFrame.Navigate(new CategoryPage());
        }
    }
}
