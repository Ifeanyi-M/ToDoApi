namespace WebAPI.Models
{
    public class AddToDoRequest
    {
        public string ToDo { get; set; }

        public string Email { get; set; }

        public int PageNumber { get; set; }
    }
}
