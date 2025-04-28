using LeftJoinTest;
using Microsoft.EntityFrameworkCore;

var db = new MyDbContext();

await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();

var blog = new Blog
{
    Name = "Blog1",
};
var blog2 = new Blog
{
    Name = "Blog2",
};
var blog3 = new Blog
{
    Name = "Blog3",
};

db.Set<Blog>().Add(blog);
db.Set<Blog>().Add(blog2);
db.Set<Blog>().Add(blog3);
await db.SaveChangesAsync();

var post = new Post
{
    Title = "post1",
    BlogId = blog2.Id,
};

var post2 = new Post
{
    Title = "post2",
    BlogId = blog2.Id,
};

var post3 = new Post
{
    Title = "post3",
    BlogId = blog3.Id,
};

db.Set<Post>().Add(post);
db.Set<Post>().Add(post2);
db.Set<Post>().Add(post3);

await db.SaveChangesAsync();

var resultLeftJoin =
    await db.Set<Blog>()
        .GroupJoin(
        db.Set<Post>(),
        left => left.Id,
        right => right.BlogId,
        (left, right) => new { left, right }
    ).ToListAsync();