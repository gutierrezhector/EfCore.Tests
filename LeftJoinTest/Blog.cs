namespace LeftJoinTest;

public class Blog
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Post> Posts { get; set; } = [];
}

public class Post
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int? BlogId { get; set; }
    public Blog? Blog { get; set; }
}