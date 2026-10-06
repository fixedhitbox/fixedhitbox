namespace domain_fh.Entities.Abstractions;

public abstract class Entity(Guid publicId)
{
    public long Id { get; private set; }
    public Guid PublicId { get; private set; } = publicId;
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ModifiedAt { get; private set; }

    protected Entity() : this(Guid.CreateVersion7())
    { }
}