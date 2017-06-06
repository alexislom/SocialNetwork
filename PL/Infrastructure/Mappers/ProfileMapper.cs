using BLL.Interface.Entities;
using PL.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.Infrastructure.Mappers
{
    public static class ProfileMapper
    {
        public static BllUserProfile ToBllProfile(this ProfileViewModel model)
        {
            return new BllUserProfile()
            {
                DateOfBirth = model.DateOfBirth,
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhotoId = model.PhotoId,
                NickName = model.NickName
            };
        }

        public static ProfileViewModel ToMvcProfile(this BllUserProfile profile)
        {
            return new ProfileViewModel()
            {
                DateOfBirth = profile.DateOfBirth,
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                NickName = profile.NickName,
                PhotoId = profile.PhotoId
            };
        }

        //public static DialogProfile ToDialogProfile(this BllProfile profile)
        //{
        //    return new DialogProfile()
        //    {
        //        Id = profile.Id,
        //        FirstName = profile.FirstName,
        //        LastName = profile.LastName
        //    };
        //}
        //public static FullProfileViewModel ToFullMvcProfile(this BllProfile profile)
        //{
        //    return new FullProfileViewModel()
        //    {
        //        BirthDate = profile.BirthDate,
        //        Id = profile.Id,
        //        FirstName = profile.FirstName,
        //        LastName = profile.LastName,
        //        UserName = profile.UserName,
        //        PhotoId = profile.PhotoId,
        //        City = profile.City,
        //        Gender = profile.Gender,
        //        ContactPhone = profile.ContactPhone,
        //        AboutMe = profile.AboutMe
        //    };
        //}

        //public static BllProfile ToUpdatingBllProfile(this ProfileEditModel model)
        //{
        //    return new BllProfile()
        //    {
        //        Id = model.Id,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        BirthDate = model.BirthDate,
        //        AboutMe = model.AboutMe,
        //        City = model.City,
        //        ContactPhone = model.ContactPhone
        //    };
        //}
    }
}