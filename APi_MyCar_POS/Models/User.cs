using System.Collections.Generic;

namespace APi_MyCar_POS.Models
{

    public class User : UserForApp
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public List<AutoUser> AutoUsers { get; set; } = new();
        public List<Auto> Autos { get; set; } = new();

        public UserForApp CovertToBase()
        {
            return new UserForApp
            {
                ID = ID,
                Login = Login,
                Password = Password,
                Email = Email
            };
        }

        public void Update(UserForApp userBase)
        {
            Login = userBase.Login;
            Password = userBase.Password;
            Email = userBase.Email;
        }
    }
}