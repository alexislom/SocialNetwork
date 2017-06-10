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
                PhotoId = profile.PhotoId,
                City = profile.City,
                Gender = profile.Gender
            };
        }

        public static DialogProfile ToDialogProfile(this BllUserProfile profile)
        {
            return new DialogProfile()
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName
            };
        }
        public static FullProfileViewModel ToFullMvcProfile(this BllUserProfile profile)
        {
            return new FullProfileViewModel()
            {
                DateOfBirth = profile.DateOfBirth,
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                NickName = profile.NickName,
                PhotoId = profile.PhotoId,
                City = profile.City,
                Gender = profile.Gender,
                MobilePhoneNumber = profile.MobilePhoneNumber,
                Status = profile.Status
            };
        }

        public static BllUserProfile ToUpdatingBllProfile(this ProfileEditModel model)
        {
            return new BllUserProfile()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Status = model.Status,
                City = model.City,
                MobilePhoneNumber = model.MobilePhoneNumber
            };
        }

        public static ProfileEditModel ToEditUserProfile(this BllUserProfile model)
        {
            return new ProfileEditModel()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Status = model.Status,
                City = model.City,
                MobilePhoneNumber = model.MobilePhoneNumber
            };
        }

        //public static SearchUserModel ToSearchUserProfile(this BllUserProfile profile)
        //{
        //    return new SearchUserModel()
        //    {
        //        Id = profile.Id,
        //        FirstName = profile.FirstName,
        //        LastName = profile.LastName,
        //        NickName = profile.NickName,
        //        Gender = profile.Gender,
        //        City = profile.City,
        //        //EqualsCoeff = 0
        //    };
        //}
    }
}