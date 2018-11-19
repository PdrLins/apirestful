namespace DesafioPitang.Models
{
    public class UserContact
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int CodeArea { get; set; }
        public string CountryCode { get; set; }
        public User User { get; set; }

    }
}
