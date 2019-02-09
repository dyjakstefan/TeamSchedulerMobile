namespace TSM.Models
{
    public class Jwt : Entity
    {
        public string Token { get; set; }

        public long Expires { get; set; }

        public int UserId { get; set; }
    }
}
