using AutoMapper;
using MVC_20211129078_OZANUYSAL.Models;
using MVC_20211129078_OZANUYSAL.ViewModels;

namespace MVC_20211129078_OZANUYSAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
