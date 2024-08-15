using System.Windows;
using System.Windows.Controls;
using WpfAppTask.Data.Entities;
using WpfAppTask.Services;

namespace WpfAppTask
{
    public partial class CategoryPage : Page
    {
        private readonly ICategoryService _categoryService;

        public CategoryPage()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                CategoriesGrid.ItemsSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}");
            }
        }

        private async void OnAddCategoryClicked(object sender, RoutedEventArgs e)
        {
            try
            {
                var newCategory = new Category
                {
                    Name = CategoryName.Text
                };

                await _categoryService.CreateCategoryAsync(newCategory);
                MessageBox.Show("Category added successfully!");
                LoadCategories(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding category: {ex.Message}");
            }
        }

        private async void OnUpdateCategoryClicked(object sender, RoutedEventArgs e)
        {
            if (CategoriesGrid.SelectedItem is Category selectedCategory)
            {
                try
                {
                    selectedCategory.Name = CategoryName.Text;

                    await _categoryService.UpdateCategoryAsync(selectedCategory.Id, selectedCategory);
                    MessageBox.Show("Category updated successfully!");
                    LoadCategories(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating category: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Select a category to update.");
            }
        }

        private async void OnDeleteCategoryClicked(object sender, RoutedEventArgs e)
        {
            if (CategoriesGrid.SelectedItem is Category selectedCategory)
            {
                try
                {
                    await _categoryService.DeleteCategoryAsync(selectedCategory.Id);
                    MessageBox.Show("Category deleted successfully!");
                    LoadCategories(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting category: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Select a category to delete.");
            }
        }
    }
}
