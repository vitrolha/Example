namespace BlazorApp.Models
{
    public class ToDoCreate
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
