using DAL.Interfaces.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class UserProfileMapper
    {
        public static UserProfile ToOrmUserProfile(this DalUserProfile dalProfile)
        {
            if (dalProfile == null)
                return null;

            return new UserProfile
            {
                Id = dalProfile.Id,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                DateOfBirth = dalProfile.DateOfBirth,
                PhotoId = dalProfile.PhotoId,
                NickName = dalProfile.NickName,
                Photo = dalProfile.Photo.ToOrmPhoto(),
                Status = dalProfile.Status,
                MobilePhoneNumber = dalProfile.MobilePhoneNumber,
                Gender = dalProfile.Gender,
                City = dalProfile.City
            };
        }

        public static DalUserProfile ToDalUserProfile(this UserProfile ormProfile)
        {
            if (ormProfile == null)
                return null;

            return new DalUserProfile
            {
                Id = ormProfile.Id,
                FirstName = ormProfile.FirstName,
                NickName = ormProfile.NickName,
                LastName = ormProfile.LastName,
                DateOfBirth = ormProfile.DateOfBirth,
                PhotoId = ormProfile.PhotoId,
                Photo = ormProfile.Photo.ToDalPhoto(),
                City = ormProfile.City,
                Gender = ormProfile.Gender,
                Status = ormProfile.Status,
                MobilePhoneNumber = ormProfile.MobilePhoneNumber
            };

        }
    }
}
