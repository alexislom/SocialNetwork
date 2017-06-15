using DAL.Interfaces.DTO;
using BLL.Interface.Entities;

namespace BLL.Mappers
{
    public static class UserProfileMapper
    {
        public static BllUserProfile ToBllUserProfile(this DalUserProfile dalProfile)
        {
            if (dalProfile == null)
                return null;

            return new BllUserProfile
            {
                Id = dalProfile.Id,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                DateOfBirth = dalProfile.DateOfBirth,
                PhotoId = dalProfile.PhotoId,
                NickName = dalProfile.NickName,
                Photo = dalProfile.Photo.ToBllPhoto(),
                Status = dalProfile.Status,
                City = dalProfile.City,
                Gender = dalProfile.Gender,
                MobilePhoneNumber = dalProfile.MobilePhoneNumber
            };
        }

        public static DalUserProfile ToDalUserProfile(this BllUserProfile bllProfile)
        {
            if (bllProfile == null)
                return null;

            return new DalUserProfile
            {
                Id = bllProfile.Id,
                FirstName = bllProfile.FirstName,
                LastName = bllProfile.LastName,
                DateOfBirth = bllProfile.DateOfBirth,
                PhotoId = bllProfile.PhotoId,
                NickName = bllProfile.NickName,
                Photo = bllProfile.Photo.ToDalPhoto(),
                Status = bllProfile.Status,
                City= bllProfile.City,
                MobilePhoneNumber = bllProfile.MobilePhoneNumber,
                Gender = bllProfile.Gender
            };
        }
    }
}
