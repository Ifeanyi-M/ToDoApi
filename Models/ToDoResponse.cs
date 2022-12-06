namespace WebAPI.Models
{
    public class ToDoResponse
    {
        public List<ToDoItems> toDoItems { get; set; } = new List<ToDoItems>();
        public int Pages { get; set; }

        public int CurrentPage { get; set; }



    }
}
