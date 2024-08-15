using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfAppTask.Data.Entities;
using WpfAppTask.Services;

namespace WpfAppTask;

public partial class ProductPage : Page
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductPage()
    {
        InitializeComponent();
        _productService = new ProductService();
        _categoryService = new CategoryService();
        LoadProducts();
        LoadCategories();
    }

    private async void LoadProducts()
    {
        try
        {
            var products = await _productService.GetAllProductsAsync();
            ProductsGrid.ItemsSource = products;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading products: {ex.Message}");
        }
    }

    private async void LoadCategories()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedValuePath = "Id";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading categories: {ex.Message}");
        }
    }

    private async void OnAddProductClicked(object sender, RoutedEventArgs e)
    {
        try
        {
            var newProduct = new Product
            {
                Name = ProductName.Text,
                Description = ProductDescription.Text,
                CategoryId = (int)CategoryComboBox.SelectedValue
            };

            await _productService.CreateProductAsync(newProduct);
            MessageBox.Show("Product added successfully!");
            LoadProducts();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error adding product: {ex.Message}");
        }
    }

    private async void OnUpdateProductClicked(object sender, RoutedEventArgs e)
    {
        if (ProductsGrid.SelectedItem is Product selectedProduct)
        {
            try
            {
                selectedProduct.Name = ProductName.Text;
                selectedProduct.Description = ProductDescription.Text;
                selectedProduct.CategoryId = (int)CategoryComboBox.SelectedValue;

                await _productService.UpdateProductAsync(selectedProduct.Id, selectedProduct);
                MessageBox.Show("Product updated successfully!");
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Select a product to update.");
        }
    }

    private async void OnDeleteProductClicked(object sender, RoutedEventArgs e)
    {
        if (ProductsGrid.SelectedItem is Product selectedProduct)
        {
            try
            {
                await _productService.DeleteProductAsync(selectedProduct.Id);
                MessageBox.Show("Product deleted successfully!");
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}");
            }
        }
        else
        {
            MessageBox.Show("Select a product to delete.");
        }
    }
}
