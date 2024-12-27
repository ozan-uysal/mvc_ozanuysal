using System.ComponentModel.DataAnnotations;

namespace MVC_20211129078_OZANUYSAL.ViewModels
{
    public class CategoryModel : BaseModel
    {

        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Kategori Adı Giriniz!")]
        public string Name { get; set; }

    }
}
