namespace finOps.Core.Entities
{
    public class Entity
    {
        public int Id { get; set; } = 0; // Default value for Id
        public Guid GuidId { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public void UpdateTimestamps()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}