using DAL.Interfaces.DTO;
using ORM.Entities;

namespace DAL.Mappers
{
    public static class PhotoMapper
    {
        public static Photo ToOrmPhoto(this DalPhoto dalPhoto)
        {
            if (dalPhoto == null)
                return null;

            return new Photo()
            {
                Id = dalPhoto.Id,
                Data = dalPhoto.Data,
                Date = dalPhoto.Date,
                MimeType = dalPhoto.MimeType,

            };
        }
        public static DalPhoto ToDalPhoto(this Photo ormPhoto)
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
