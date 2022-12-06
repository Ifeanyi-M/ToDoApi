namespace WebAPI.Models
{
    public class ToDoItems
    {
        public Guid Id { get; set; }
        public string ToDo { get; set; }

        public string Email { get; set; }

        public int PageNumber { get; set; }

    }
}
