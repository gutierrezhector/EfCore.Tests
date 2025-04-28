namespace InterceptorTest;

public interface ICreatableAndUpdatable
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}

public class MyEntity : ICreatableAndUpdatable
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public override string ToString()
    {
        Console.WriteLine($"Name : {Name}");
        Console.WriteLine($"CreatedAt : {CreatedAt}");
        Console.WriteLine($"UpdatedAt : {UpdatedAt}");
        return base.ToString();
    }
}