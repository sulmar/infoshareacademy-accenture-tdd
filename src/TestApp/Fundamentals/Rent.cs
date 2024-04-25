using System;

namespace TestApp
{
    public class Rent
    {
        public User Rentee { get; set; }

        public bool CanReturn(User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            if (user.IsAdmin)
                return true;

            if (Rentee == user)
                return true;

            return false;
        }

    }


    public class User
    {
        public bool IsAdmin { get; set; }
        public string Name { get; set; }


    }

}
