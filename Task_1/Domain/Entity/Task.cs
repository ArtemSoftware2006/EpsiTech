namespace Task_1.Domain.Entity
{
    public class Task
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime DatePublish { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}