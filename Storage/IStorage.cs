namespace Services.Storage;

public interface IStorage<in T>
{
    public void Add(T item);
    public void Update(T item);
    public void Remove(T item);
    
}