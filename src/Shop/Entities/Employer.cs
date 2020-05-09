namespace Shop.Entities
{
    public class Employer : BaseEntity
    {
        public string UnicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CapatalisedFullName { get; set; }
        public string UserName { get; set; }
        public string CapatalisedUserName { get; set; }
        public string Email { get; set; }
        public string CapatalisedEmail { get; set; }
        public long MobileNo { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
    }
}
