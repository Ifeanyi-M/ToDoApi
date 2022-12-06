namespace WebAPI.Models
{
    public class UpdateTodoRequest
    {
        public string ToDo { get; set; }

        public string Email { get; set; }

        public int PageNumber { get; set; }
    }
}
