using InterceptorTest;
using Microsoft.EntityFrameworkCore;

var db = new MyDbContext();
        
await db.Database.EnsureDeletedAsync();
await db.Database.EnsureCreatedAsync();

var entity1 = new MyEntity
{
    Name = "Entity1 Created"
};

db.Add(entity1);
await db.SaveChangesAsync();

var entityAdded = await db.Set<MyEntity>().FirstOrDefaultAsync();
entityAdded.ToString();


entityAdded.Name = "Entity1 Updated";
await db.SaveChangesAsync();

var entityUpdated = await db.Set<MyEntity>().FirstOrDefaultAsync();
entityUpdated.ToString();