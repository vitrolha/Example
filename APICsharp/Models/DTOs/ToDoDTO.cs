namespace APICSharpToDoList.Models.DTOs
{
    public class ToDoDTO
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now;
    }
}
