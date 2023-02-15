namespace KoboPay.Data.Models
{
    public class Auditable
    {
        public Guid Id { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.UtcNow;
        public DateTime? DateTimeModified { get; set; } = DateTime.UtcNow;
    }
}
