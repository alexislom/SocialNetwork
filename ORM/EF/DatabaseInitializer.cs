using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace ORM.EF
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<SocialNetworkContext>
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

            var admin = new User
            {
                UserName = "admin",
                Password = "qwerty",
                Email = "alexislomako@gmail.com",
                RoleId = 1,
                UserProfile = new UserProfile
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

            var user1 = new User
            {
                UserName = "putin",
                Password = "123456",
                Email = "putin@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "putin",
                    Photo = new Photo(),
                    FirstName = "Vladimir",
                    LastName = "Vladimirovich",
                    Status = "The most powerful man!",
                    DateOfBirth = new System.DateTime(1952, 10, 07),
                    City = "Moscow",
                    Gender = true,
                    MobilePhoneNumber = "+495-985-60-60"
                }
            };

            var user2 = new User
            {
                UserName = "poroshenko",
                Password = "123456",
                Email = "poroshenko@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "poroshenko",
                    Photo = new Photo(),
                    FirstName = "Petr",
                    LastName = "Poroshenko",
                    Status = "I'm just loving sweets...",
                    DateOfBirth = new System.DateTime(1965, 09, 26),
                    City = "Kiev",
                    Gender = true,
                    MobilePhoneNumber = "+380-44-255-7333"
                }
            };

            var user3 = new User
            {
                UserName = "merkel",
                Password = "123456",
                Email = "merkel@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "merkel",
                    Photo = new Photo(),
                    FirstName = "Angela",
                    LastName = "Merkel",
                    Status = "Tolerance is our all",
                    DateOfBirth = new System.DateTime(1954, 07, 17),
                    City = "Berlin",
                    Gender = false,
                    MobilePhoneNumber = "+301778826937"
                }
            };

            var user4 = new User
            {
                UserName = "trump",
                Password = "123456",
                Email = "trump@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "trump",
                    Photo = new Photo(),
                    FirstName = "Donald",
                    LastName = "Trump",
                    Status = "Make America great again!",
                    DateOfBirth = new System.DateTime(1946, 06, 14),
                    City = "New York",
                    Gender = true,
                    MobilePhoneNumber = "+1-800-469-92-69"
                }
            };

            var user5 = new User
            {
                UserName = "luka",
                Password = "123456",
                Email = "luka@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "luka",
                    Photo = new Photo(),
                    FirstName = "Alexander",
                    LastName = "Lukashenko",
                    Status = "The crisis is in our heads!",
                    DateOfBirth = new System.DateTime(1954, 08, 30),
                    City = "Minsk",
                    Gender = true,
                    MobilePhoneNumber = "8017-222-36-23"
                }
            };

            var user6 = new User
            {
                UserName = "obama",
                Password = "123456",
                Email = "obama@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "obama",
                    Photo = new Photo(),
                    FirstName = "Barak",
                    LastName = "Obama",
                    Status = "I'm fine",
                    DateOfBirth = new System.DateTime(1961, 08, 4),
                    City = "Washington",
                    Gender = true,
                    MobilePhoneNumber = "+1-824-455-92-69"
                }
            };

            var user7 = new User
            {
                UserName = "medvedev",
                Password = "123456",
                Email = "medvedev@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "medvedev",
                    Photo = new Photo(),
                    FirstName = "Dmitrii",
                    LastName = "Medvedev",
                    Status = "There is no money, but you are holding!",
                    DateOfBirth = new System.DateTime(1965, 09, 14),
                    City = "Moscow",
                    Gender = true,
                    MobilePhoneNumber = "+495-935-40-60"
                }
            };

            var user8 = new User
            {
                UserName = "arnold",
                Password = "123456",
                Email = "arnold@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "arnold",
                    Photo = new Photo(),
                    FirstName = "Arnold",
                    LastName = "Schwarzenegger",
                    Status = "I'll be back!",
                    DateOfBirth = new System.DateTime(1990, 5, 15),
                    City = "Paris",
                    Gender = true,
                    MobilePhoneNumber = "+385896747455"
                }
            };

            var user9 = new User()
            {
                UserName = "eltcen",
                Password = "123456",
                Email = "eltcen@gmail.com",
                RoleId = 4,
                UserProfile = new UserProfile
                {
                    NickName = "eltcen",
                    Photo = new Photo(),
                    FirstName = "Boris",
                    LastName = "Nikolaevich",
                    Status = ":(",
                    DateOfBirth = new System.DateTime(1931, 02, 01),
                    City = "Moscow",
                    Gender = true,
                    MobilePhoneNumber = "+495-335-50-60"
                }
            };

            var user10 = new User
            {
                UserName = "tetcher",
                Password = "123456",
                Email = "tetcher@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "tetcher",
                    Photo = new Photo(),
                    FirstName = "Margaret",
                    LastName = "Tetcher",
                    Status = "The Iron Lady",
                    DateOfBirth = new System.DateTime(1925, 10, 13),
                    City = "London",
                    Gender = false,
                    MobilePhoneNumber = "+7-495-123-45-67"
                }
            };

            var user11 = new User
            {
                UserName = "kennedy",
                Password = "123456",
                Email = "kennedy@gmail.com",
                RoleId = 2,
                UserProfile = new UserProfile
                {
                    NickName = "kennedy",
                    Photo = new Photo(),
                    FirstName = "John",
                    LastName = "Kenedy",
                    Status = "35th US President",
                    DateOfBirth = new System.DateTime(1963, 11, 22),
                    City = "Bruklin",
                    Gender = true,
                    MobilePhoneNumber = "+1-670-459-92-49"
                }
            };

            #endregion

            #region Friendships

            var frienship1 = new FriendRequest
            {
                UserFromId = 1,
                UserToId = 2,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship2 = new FriendRequest
            {
                UserFromId = 4,
                UserToId = 2,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship3 = new FriendRequest
            {
                UserFromId = 2,
                UserToId = 3,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship4 = new FriendRequest
            {
                UserFromId = 3,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship5 = new FriendRequest
            {
                UserFromId = 1,
                UserToId = 4,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship6 = new FriendRequest
            {
                UserFromId = 1,
                UserToId = 5,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship7 = new FriendRequest
            {
                UserFromId = 2,
                UserToId = 5,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship8 = new FriendRequest
            {
                UserFromId = 10,
                UserToId = 6,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship9 = new FriendRequest
            {
                UserFromId = 1,
                UserToId = 8,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };

            var frienship10 = new FriendRequest
            {
                UserFromId = 4,
                UserToId = 9,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };

            var frienship11 = new FriendRequest
            {
                UserFromId = 4,
                UserToId = 3,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };

            var frienship12 = new FriendRequest
            {
                UserFromId = 10,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = false
            };

            var frienship13 = new FriendRequest
            {
                UserFromId = 11,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = false
            };

            var frienship14 = new FriendRequest
            {
                UserFromId = 12,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = false
            };
            var frienship15 = new FriendRequest
            {
                UserFromId = 9,
                UserToId = 1,
                RequestDate = System.DateTime.Now,
                IsConfirmed = false
            };

            var frienship16 = new FriendRequest
            {
                UserFromId = 6,
                UserToId = 7,
                RequestDate = System.DateTime.Now,
                IsConfirmed = true
            };
            var frienship17 = new FriendRequest
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
