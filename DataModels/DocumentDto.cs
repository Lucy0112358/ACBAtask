using System.ComponentModel.DataAnnotations.Schema;

namespace ACBAbankTask.DataModels
{
    public class DocumentDto
    {
        public string Title { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string IssuedBy { get; set; }
        public string Number { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CustomerId { get; set; }
    }
}
