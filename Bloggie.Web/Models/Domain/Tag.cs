namespace Bloggie.Web.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public Guid BlogPostId { get; set; }
    }
}
