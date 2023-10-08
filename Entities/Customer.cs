namespace ACBAbankTask.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Gender { get; set; }       
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime Birthday { get; set; }
        public List<Document> Documents { get; set; } = new List<Document>();
        public List<Address> Address { get; set; } = new List<Address>();
        public List<Mobile> Mobile { get; set; } = new List<Mobile>();

    }
}
