namespace MVC_20211129078_OZANUYSAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

    }
}
