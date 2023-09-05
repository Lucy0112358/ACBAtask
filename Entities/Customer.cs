namespace ACBAbankTask.Entities
{
    public class Customer
    {
       

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mobile { get; set; }
        public string Passport { get; set; }
        public string Password { get; set; }
        public string IssuedBy { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime CratedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime Birthday { get; set; }
    }
}
