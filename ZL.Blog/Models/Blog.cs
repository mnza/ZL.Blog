namespace ZL.Blog.Models
{
    public class Blog
    {
        public string? Category { get; set; }
        public string? Name { get; set; }
        public string? Path { get; set; }
        public DateTime? LastModifyDateTime { get; set; }
        public FileType FileType { get; set; }
    }

    public enum FileType
    {
        Blog,
        Image,
        Text,
        Pdf,
        Word,
        Excel,
        Markdown,
        Other
    }

}
