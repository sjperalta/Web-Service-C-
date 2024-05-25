namespace TodoApiRestfull.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public bool IsComplete { get; set; }
    }
}