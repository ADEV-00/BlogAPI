namespace Blog.Models
{
    public class BlogPost
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Boolean isRemoved { get; set; }

    }

    public class BlogPostDTO
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
