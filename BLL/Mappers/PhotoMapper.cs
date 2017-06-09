using DAL.Interfaces.DTO;
using BLL.Interface.Entities;

namespace BLL.Mappers
{
    public static class PhotoMapper
    {
        public static BllPhoto ToBllPhoto(this DalPhoto dalPhoto)
        {
            var ormPhoto = new BllPhoto()
            {
                Id = dalPhoto.Id,
                Data = dalPhoto.Data,
                Date = dalPhoto.Date,
                MimeType = dalPhoto.MimeType
            };
            return ormPhoto;
        }
        public static DalPhoto ToDalPhoto(this BllPhoto ormPhoto)
        {
            if (ormPhoto == null)
                return null;
            return new DalPhoto()
            {
                Id = ormPhoto.Id,
                Data = ormPhoto.Data,
                Date = ormPhoto.Date,
                MimeType = ormPhoto.MimeType,
            };
        }
    }
}
