namespace ACBAbankTask.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Title { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

    }
}
