using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.EF
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<SocialNetworkContext>
    {
        protected override void Seed(SocialNetworkContext context)
        {
            #region Roles

            new List<Role>()
            {
                new Role() {Name = "Admin"},
                new Role() {Name = "ActiveUser"},
                new Role() {Name = "Moderator" },
                new Role() {Name = "BannedUser" }
            }.ForEach(r => context.Roles.Add(r));

            #endregion

            var admin = new User()
            {
                UserName = "admin",
                Password = "lomiklomik",
                Email = "alexislomako@gmail.com",
                UserProfile = new UserProfile()
                {
                    NickName = "admin",
                    Photo = new Photo(),
                    FirstName = "Alexey",
                    LastName = "Lomako",
                    Status = "I'm watching you...",
                    Gender = true,
                    DateOfBirth = new DateTime(1996,09,24),
                    City = "Minsk",
                    MobilePhoneNumber = "+375333252732"
                }
            };
            
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
