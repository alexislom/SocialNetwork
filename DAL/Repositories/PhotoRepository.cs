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

namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DbContext _unitOfWork;

        public PhotoRepository(DbContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CRUD operations

        public void Create(DalPhoto e)
        {
            _unitOfWork.Set<Photo>().Add(e.ToOrmPhoto());
        }

        public void Update(DalPhoto e)
        {
            var photo = _unitOfWork.Set<Photo>().First(x => x.Id == e.Id);
            _unitOfWork.Set<Photo>().Attach(photo);
            photo.Data = e.Data;
            photo.MimeType = e.MimeType;
            photo.Date = e.Date;
            _unitOfWork.Entry(photo).State = EntityState.Modified;
        }

        public void Delete(DalPhoto e)
        {
            var userPhoto = _unitOfWork.Set<Photo>().FirstOrDefault(p => p.Id == e.Id);
            _unitOfWork.Set<Photo>().Attach(userPhoto);
            _unitOfWork.Set<Photo>().Remove(userPhoto);
            _unitOfWork.Entry(userPhoto).State = EntityState.Deleted;
        }

        #endregion

        #region Get photos

        public IEnumerable<DalPhoto> GetAll()
        {
            var photos = _unitOfWork.Set<Photo>().Select(u => u).ToList();
            return photos.Select(p => p.ToDalPhoto()).ToList();
        }

        public DalPhoto GetById(int key)
        {
            var photo = _unitOfWork.Set<Photo>().Find(key);
            return photo == null ? null : photo.ToDalPhoto();
        }

        public DalPhoto GetOneByPredicate(Expression<Func<DalPhoto, bool>> predicate)
        {
            var photo = GetAllByPredicate(predicate).FirstOrDefault();
            return photo;
        }

        public IEnumerable<DalPhoto> GetAllByPredicate(Expression<Func<DalPhoto, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalPhoto, Photo>(Expression.Parameter(typeof(Photo), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Photo, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = _unitOfWork.Set<Photo>().Where(express).ToList();
            return final.Select(p => p.ToDalPhoto()).ToList();
        }

        #endregion
    }
}
