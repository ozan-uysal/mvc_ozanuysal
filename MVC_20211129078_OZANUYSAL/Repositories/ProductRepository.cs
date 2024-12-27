using AutoMapper;
using MVC_20211129078_OZANUYSAL.Models;
using MVC_20211129078_OZANUYSAL.ViewModels;

namespace MVC_20211129078_OZANUYSAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
