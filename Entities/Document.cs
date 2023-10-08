namespace ACBAbankTask.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string IssuedBy { get; set; }
        public string Number { get; set; }      
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CustomerId {get; set; }
        public Customer customer { get; set; }

    }
}
