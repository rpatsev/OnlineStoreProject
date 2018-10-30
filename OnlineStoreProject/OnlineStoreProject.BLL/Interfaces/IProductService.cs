using OnlineStoreProject.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetAllProducts();
        IEnumerable<CategoryModel> GetAllCategories();
        CategoryModel GetCategory(int id);
        ProductModel GetProduct(int id);
        IEnumerable<ProductModel> GetOrderedProductList(bool orderByPoints, bool orderByPurchase);
        IEnumerable<ProductModel> GetProductsByCategory(int catid);
        void SaveProduct(ProductModel product);
        void SaveCategory(CategoryModel category);
        void DeleteProductById(int id);
        void DeleteCategoryById(int id);
        void UpdateProductData(int id, ProductModel product);
        void UpdateCategory(int id, CategoryModel category);
    }
}
