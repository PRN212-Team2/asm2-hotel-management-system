namespace RepositoryLayer.Models
{
    public class Customer
    {
        public int CustomerId {  get; set; }
        public string CustomerFullName {  get; set; }
        public string Telephone {  get; set; }
        public string EmailAddress { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public bool CustomerStatus { get; set; }
        public string Password { get; set; }
    }
}
