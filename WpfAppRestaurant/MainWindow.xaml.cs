using System.Windows;
using System.Windows.Controls;

namespace WpfAppRestaurant
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new HomePage();
        }

        private void SidebarListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SidebarListBox.SelectedIndex == 0)
            {
                MainFrame.Navigate(new HomePage());
            }
            else if (SidebarListBox.SelectedIndex == 1)
            {
                MainFrame.Navigate(new CategoriesPage());
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
