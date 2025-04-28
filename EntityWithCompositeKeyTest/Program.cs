using BlazarTech.QueryableValues;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTests;

internal class CompositeKeyEf
{
    public int Id1 { get; set; }
    public int Id2 { get; set; }
}

public class Program
{    
    public async static Task Main(string[] args)
    {
        var db = new MyDbContext();
        
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();

        db.Add(new EntityWithCompositeKey() { Id1 = 1, Id2 = 1 });
        db.Add(new EntityWithCompositeKey() { Id1 = 1, Id2 = 2 });
        db.Add(new EntityWithCompositeKey() { Id1 = 2, Id2 = 3 });

        await db.SaveChangesAsync();
        
        var list = new HashSet<CompositeKeyEf>
        {
            new CompositeKeyEf { Id1 = 1, Id2 = 1 },
            new CompositeKeyEf { Id1 = 1, Id2 = 2 },
        };
        
        var queryableValues = db.AsQueryableValues(list);

        try
        {
            // works
            var requestWithQueryableValues = await db.Set<EntityWithCompositeKey>()
                .Where(e => queryableValues.Any(l => l.Id1 == e.Id1 && l.Id2 == e.Id2))
                .ToListAsync();
        
            // ef cannot translate
            var requestWithoutQueryableValues = await db.Set<EntityWithCompositeKey>()
                .Where(e => list.Any(l => l.Id1 == e.Id1 && l.Id2 == e.Id2))
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}