namespace ZL.Blog.Models
{
    public class Collect
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Category { get; set; }
        public string Color { get;set; }
    }

    public class CollectConfig
    {
        public List<Collect> Collects { get; set; } = new List<Collect>();
    }
}
