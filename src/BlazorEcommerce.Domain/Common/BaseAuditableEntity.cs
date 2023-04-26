namespace BlazorEcommerce.Domain.Common;

public abstract class BaseAuditableEntity<TKey> : BaseEntity<TKey> 
{
    public BaseAuditableEntity()
    {
        IsActive = true;
        IsDeleted = false;
        CreatedUtc = DateTime.UtcNow;
    }

    public  bool IsActive { get; set; }
    public  bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedUtc { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime LastModifiedUtc { get; set; }
}
