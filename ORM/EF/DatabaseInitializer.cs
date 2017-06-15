using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ORM.EF
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<SocialNetworkContext>
    {
        protected override void Seed(SocialNetworkContext context)
        {
            #region Roles

            new List<Role>
            {
                new Role {Name = "Admin"},
                new Role {Name = "ActiveUser"},
                new Role {Name = "Moderator" },
                new Role {Name = "BannedUser" }
            }.ForEach(r => context.Roles.Add(r));

            #endregion

            #region Users,UserProfiles and Photo

            var admin = new User()
            {
                UserName = "admin",
                Password = "qwerty",
                Email = "alexislomako@gmail.com",
                RoleId = 1,
                UserProfile = new UserProfile()
                {
                    NickName = "admin",
                    Photo = new Photo(),
                    FirstName = "Alex",
                    LastName = "Lomako",
                    Status = "I'm watching you...",
                    Gender = true,
                    DateOfBirth = new DateTime(1996,09,24),
                    City = "Minsk",
                    MobilePhoneNumber = "+375333252732"
                }
            };

            var user1 = new User()
            {
                UserName = "affleck",
                Password = "passwordqwerty",
                Email = "affleck@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile()
                {
                    NickName = "affleck",
                    Photo = new Photo(),
                    FirstName = "Ben",
                    LastName = "Affleck",
                    Status = "I'm Batman",
                    DateOfBirth = new System.DateTime(1972, 8, 15),
                    City = "Bercli",
                    Gender = true,
                    MobilePhoneNumber = "+567437897651"
                }
            };

            var user2 = new User()
            {
                UserName = "perry",
                Password = "simplepassword",
                Email = "perry@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile()
                {
                    NickName = "perry",
                    Photo = new Photo(),
                    FirstName = "Katy",
                    LastName = "Perry",
                    Status = "Like music",
                    DateOfBirth = new System.DateTime(1984, 10, 25),
                    City = "Santa Barbara",
                    Gender = false,
                    MobilePhoneNumber = "+345977642433"
                }
            };

            var user3 = new User()
            {
                UserName = "jolie",
                Password = "simplepassword",
                Email = "jolie@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile()
                {
                    NickName = "jolie",
                    Photo = new Photo(),
                    FirstName = "Angelina",
                    LastName = "Jolie",
                    Status = "Like travelling",
                    DateOfBirth = new System.DateTime(1975, 6, 4),
                    City = "Los Angeles",
                    Gender = false,
                    MobilePhoneNumber = "+345977642433"
                }
            };

            var user4 = new User()
            {
                UserName = "maguire",
                Password = "simplepassword",
                Email = "maguire@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile()
                {
                    NickName = "maguire",
                    Photo = new Photo(),
                    FirstName = "Tobi",
                    LastName = "Maguire",
                    Status = "Big fan of \"Spider-man!\"",
                    DateOfBirth = new System.DateTime(1975, 6, 27),
                    City = "Santa Monica",
                    Gender = true,
                    MobilePhoneNumber = "+345876447433"
                }
            };

            var user5 = new User()
            {
                UserName = "jackman",
                Password = "simplepassword",
                Email = "jackman@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile()
                {
                    NickName = "jackman",
                    Photo = new Photo(),
                    FirstName = "Hugh",
                    LastName = "Jackman",
                    Status = "Big fan \"True Detective!\"",
                    DateOfBirth = new System.DateTime(1968, 10, 12),
                    City = "Sydney",
                    Gender = true,
                    MobilePhoneNumber = "+345875447432"
                }
            };

            var user6 = new User()
            {
                UserName = "craig",
                Password = "simplepassword",
                Email = "craig@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile()
                {
                    NickName = "craig",
                    Photo = new Photo(),
                    FirstName = "Daniel",
                    LastName = "Craig",
                    Status = "Bond, James Bond",
                    DateOfBirth = new System.DateTime(1968, 3, 2),
                    City = "Liverpool",
                    Gender = true,
                    MobilePhoneNumber = "+225876747433"
                }
            };

            var user7 = new User()
            {
                UserName = "gates",
                Password = "simplepassword",
                Email = "gates@gmail.com",
                RoleId = 3,
                UserProfile = new UserProfile()
                {
                    NickName = "gates",
                    Photo = new Photo(),
                    FirstName = "Bill",
                    LastName = "Gates",
                    Status = "Love Soft and Micro things!",
                    DateOfBirth = new System.DateTime(1955, 10, 28),
                    City = "Seattle",
                    Gender = true,
                    MobilePhoneNumber = "+225896747433"
                }
            };

            var user8 = new User()
            {
                UserName = "watson",
                Password = "simplepassword",
                Email = "watson@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile()
                {
                    NickName = "watson",
                    Photo = new Photo(),
                    FirstName = "Emma",
                    LastName = "Watson",
                    Status = "+10 points to Griffindor!",
                    DateOfBirth = new System.DateTime(1990, 5, 15),
                    City = "Paris",
                    Gender = false,
                    MobilePhoneNumber = "+385896747455"
                }
            };

            var user9 = new User()
            {
                UserName = "olsen",
                Password = "simplepassword",
                Email = "olsen@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile()
                {
                    NickName = "olsen",
                    Photo = new Photo(),
                    FirstName = "Elizabeth",
                    LastName = "Olsen",
                    Status = "Love is blind",
                    DateOfBirth = new System.DateTime(1989, 2, 16),
                    City = "Sherman Oaks",
                    Gender = false,
                    MobilePhoneNumber = "+385896700455"
                }
            };

            var user10 = new User()
            {
                UserName = "knightley",
                Password = "simplepassword",
                Email = "knightley@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile()
                {
                    NickName = "knightley",
                    Photo = new Photo(),
                    FirstName = "Keira",
                    LastName = "Knightley",
                    Status = "Like active people)",
                    DateOfBirth = new System.DateTime(1985, 3, 26),
                    City = "London",
                    Gender = false,
                    MobilePhoneNumber = "+387296700455"
                }
            };

            var user11 = new User()
            {
                UserName = "pitt",
                Password = "simplepassword",
                Email = "pitt@gmail.com",
                RoleId = 4,
                UserProfile = new UserProfile()
                {
                    NickName = "pitt",
                    Photo = new Photo(),
                    FirstName = "Brad",
                    LastName = "Pitt",
                    Status = "Join to my Fight club",
                    DateOfBirth = new System.DateTime(1963, 12, 18),
                    City = "Shawnee",
                    Gender = true,
                    MobilePhoneNumber = "+387796712455"
                }
            };

            #endregion

            #region Friendships

            var frienship1 = new FriendRequest()
            {
                UserFromId = 1,
                UserToId = 2,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship2 = new FriendRequest()
            {
                UserFromId = 4,
                UserToId = 2,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship3 = new FriendRequest()
            {
                UserFromId = 2,
                UserToId = 3,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship4 = new FriendRequest()
            {
                UserFromId = 3,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship5 = new FriendRequest()
            {
                UserFromId = 1,
                UserToId = 4,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship6 = new FriendRequest()
            {
                UserFromId = 1,
                UserToId = 5,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship7 = new FriendRequest()
            {
                UserFromId = 2,
                UserToId = 5,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship8 = new FriendRequest()
            {
                UserFromId = 10,
                UserToId = 6,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship9 = new FriendRequest()
            {
                UserFromId = 1,
                UserToId = 8,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };

            var frienship10 = new FriendRequest()
            {
                UserFromId = 4,
                UserToId = 9,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };

            var frienship11 = new FriendRequest()
            {
                UserFromId = 4,
                UserToId = 3,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };

            var frienship12 = new FriendRequest()
            {
                UserFromId = 10,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = false
            };

            var frienship13 = new FriendRequest()
            {
                UserFromId = 11,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = false
            };

            var frienship14 = new FriendRequest()
            {
                UserFromId = 12,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = false
            };
            var frienship15 = new FriendRequest()
            {
                UserFromId = 9,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = false
            };

            var frienship16 = new FriendRequest()
            {
                UserFromId = 6,
                UserToId = 7,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship17 = new FriendRequest()
            {
                UserFromId = 10,
                UserToId = 11,
                RequestDate = System.DateTime.Now,
                IsConfirmed = false
            };

            #endregion

            context.Users.Add(admin);
            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user4);
            context.Users.Add(user5);
            context.Users.Add(user6);
            context.Users.Add(user7);
            context.Users.Add(user8);
            context.Users.Add(user9);
            context.Users.Add(user10);
            context.Users.Add(user11);

            context.SaveChanges();

            context.FriendRequests.Add(frienship1);
            context.FriendRequests.Add(frienship2);
            context.FriendRequests.Add(frienship3);
            context.FriendRequests.Add(frienship4);
            context.FriendRequests.Add(frienship5);
            context.FriendRequests.Add(frienship6);
            context.FriendRequests.Add(frienship7);
            context.FriendRequests.Add(frienship8);
            context.FriendRequests.Add(frienship9);
            context.FriendRequests.Add(frienship10);
            context.FriendRequests.Add(frienship11);
            context.FriendRequests.Add(frienship12);
            context.FriendRequests.Add(frienship13);
            context.FriendRequests.Add(frienship14);
            context.FriendRequests.Add(frienship15);
            context.FriendRequests.Add(frienship16);
            context.FriendRequests.Add(frienship17);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
