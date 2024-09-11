namespace BlazorApp.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
