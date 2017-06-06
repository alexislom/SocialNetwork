using DAL.Interface;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Interfaces;
using DAL.Mappers;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DbContext unitOfWork;

        public PhotoRepository(DbContext unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void Create(DalPhoto e)
        {
            unitOfWork.Set<Photo>().Add(e.ToOrmPhoto());
        }

        public void Delete(DalPhoto e)
        {
            var userPhoto = unitOfWork.Set<Photo>().FirstOrDefault(p => p.Id == e.Id);
            unitOfWork.Set<Photo>().Attach(userPhoto);
            unitOfWork.Set<Photo>().Remove(userPhoto);
            unitOfWork.Entry(userPhoto).State = System.Data.Entity.EntityState.Deleted;
        }

        public IEnumerable<DalPhoto> GetAll()
        {
            var photos = unitOfWork.Set<Photo>().Select(u => u).ToList();
            return photos.Select(p => p.ToDalPhoto()).ToList();
        }

        public IEnumerable<DalPhoto> GetAllByPredicate(Expression<Func<DalPhoto, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalPhoto, Photo>(Expression.Parameter(typeof(Photo), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Photo, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = unitOfWork.Set<Photo>().Where(express).ToList();
            return final.Select(p => p.ToDalPhoto()).ToList();
        }

        public DalPhoto GetById(int key)
        {
            var photo = unitOfWork.Set<Photo>().Find(key);
            if (photo == null)
                return null;
            return photo.ToDalPhoto();
        }

        public DalPhoto GetOneByPredicate(Expression<Func<DalPhoto, bool>> predicate)
        {
            var photo = GetAllByPredicate(predicate).FirstOrDefault();
            return photo;
        }

        public void Update(DalPhoto e)
        {
            var photo = unitOfWork.Set<Photo>().First(x => x.Id == e.Id);
            unitOfWork.Set<Photo>().Attach(photo);
            photo.Data = e.Data;
            photo.MimeType = e.MimeType;
            photo.Date = e.Date;
            unitOfWork.Entry(photo).State = EntityState.Modified;
        }
    }
}
