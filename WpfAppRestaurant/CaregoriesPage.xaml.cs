using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfAppRestaurant.Data.Entities;
using WpfAppRestaurant.Data.Service;

namespace WpfAppRestaurant
{
    public partial class CategoriesPage : Page
    {
        private readonly ApiService _apiService;
        private List<Product> _allProducts; // Store all products

        public CategoriesPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
            Loaded += CategoriesPage_Loaded;
        }

        private async void CategoriesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategoriesAsync();
            await LoadAllProductsAsync(); // Load all products on page load
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _apiService.GetCategoriesAsync();
            CategoriesItemsControl.ItemsSource = categories;
        }

        private async Task LoadAllProductsAsync()
        {
            _allProducts = await _apiService.GetAllProductsAsync();

            foreach (var product in _allProducts)
            {
                if (!string.IsNullOrWhiteSpace(product.ImagePath))
                {
                    // Rasm URL'ini to'g'ri formatlash
                    product.ImagePath = $"https://localhost:7146/images/{product.ImagePath.TrimStart('~', '/')}";
                }
            }
        }



        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var selectedCategory = button.DataContext as Category;

            if (selectedCategory != null)
            {
                LoadProductsByCategory(selectedCategory.Id);
            }
        }

        private void LoadProductsByCategory(int categoryId)
        {
            if (_allProducts == null)
            {
                MessageBox.Show("Mahsulot ma'lumotlari mavjud emas.");
                return;
            }

            var filteredProducts = _allProducts.Where(p => p.CategoryId == categoryId).ToList();

            if (filteredProducts.Any())
            {
                foreach (var product in filteredProducts)
                {
                    if (!string.IsNullOrWhiteSpace(product.ImagePath))
                    {
                        try
                        {
                            var bitmap = new BitmapImage();
                            bitmap.BeginInit();
                            bitmap.UriSource = new Uri(product.ImagePath, UriKind.Absolute);
                            bitmap.CacheOption = BitmapCacheOption.OnLoad;
                            bitmap.EndInit();
                            bitmap.Freeze();

                            product.ImageBitmap = bitmap;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Rasmni yuklashda xatolik: {ex.Message}", "Xato", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }

                ProductsItemsControl.ItemsSource = filteredProducts;
            }
            else
            {
                ProductsItemsControl.ItemsSource = null;
                MessageBox.Show("Tanlangan kategoriya uchun mahsulotlar topilmadi.");
            }
        }



    }
}
