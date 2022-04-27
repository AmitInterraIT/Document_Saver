

using Document_Saver.Models;

namespace Document_Saver
{
    public class UserRepository 
    {
        private readonly List<User> users = new List<User>();

        public UserRepository()
        {
            users.Add(new User
            {
                User_Name = "joydipkanjilal",
                User_Password = "joydip123",
                Role = "manager"
            });
            users.Add(new User
            {
                User_Name = "michaelsanders",
                User_Password = "michael321",
                Role = "developer"
            });
            users.Add(new User
            {
                User_Name = "stephensmith",
                User_Password = "stephen123",
                Role = "tester"
            });
            users.Add(new User
            {
                User_Name = "rodpaddock",
                User_Password = "rod123",
                Role = "admin"
            });
            users.Add(new User
            {
                User_Name = "rexwills",
                User_Password = "rex321",
                Role = "admin"
            });
        }
        public User GetUser(User userInfo)
        {
            return users.Where(x => x.User_Name.ToLower() == userInfo.User_Name.ToLower()
                && x.User_Password == userInfo.User_Password).FirstOrDefault();
        }
    }
}