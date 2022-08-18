namespace APi_MyCar_POS.Models
{
    public class AutoUser
    {
        public int AutoID { get; set; }
        public Auto? Auto { get; set; }

        public int UserID { get; set; }
        public User? User { get; set; }
    }
}
