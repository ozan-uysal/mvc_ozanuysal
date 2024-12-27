namespace MVC_20211129078_OZANUYSAL.ViewModels
{
    public class TodoModel:BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IsOK { get; set; }
    }
}
