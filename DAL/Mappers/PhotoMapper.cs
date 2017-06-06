using DAL.Interfaces.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class PhotoMapper
    {
        public static Photo ToOrmPhoto(this DalPhoto dalPhoto)
        {
            var ormPhoto = new Photo()
            {
                Id = dalPhoto.Id,
                Data = dalPhoto.Data,
                Date = dalPhoto.Date,
                MimeType = dalPhoto.MimeType,

            };
            return ormPhoto;
        }
        public static DalPhoto ToDalPhoto(this Photo ormPhoto)
        {
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
