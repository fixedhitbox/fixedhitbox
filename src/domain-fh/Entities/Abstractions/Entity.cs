namespace domain_fh.Entities.Abstractions;

public abstract class Entity
{
    public long Id { get; private set; }
    public Guid PublicId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset ModifiedAt { get; private set; }

    protected Entity()
    {
        PublicId = Guid.CreateVersion7();
        CreatedAt = DateTimeOffset.UtcNow;
    }

    protected Entity(Guid publicId)
    {
        PublicId = publicId;
        CreatedAt = DateTimeOffset.UtcNow;
    }
}