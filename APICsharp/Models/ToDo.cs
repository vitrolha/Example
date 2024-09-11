namespace APICSharpToDoList.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Start { get; set; } = DateTime.Now;
        public DateTime End { get; set; } = DateTime.Now;
    }
}
