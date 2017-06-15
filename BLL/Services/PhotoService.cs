using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DAL.Interface;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Mappers;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IUnitOfWork unitOfWork, IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
            _unitOfWork = unitOfWork;
        }

        #region CRUD operations

        public void Create(BllPhoto item)
        {
            _photoRepository.Create(item.ToDalPhoto());
            _unitOfWork.Commit();
        }

        public void Update(BllPhoto item)
        {
            _photoRepository.Update(item.ToDalPhoto());
            _unitOfWork.Commit();
        }

        public void Delete(BllPhoto item)
        {
            _photoRepository.Delete(item.ToDalPhoto());
            _unitOfWork.Commit();
        }

        #endregion

        #region Get profiles

        public BllPhoto GetById(int id)
        {
            return _photoRepository.GetById(id).ToBllPhoto();
        }

        public IEnumerable<BllPhoto> GetAll()
        {
            return _photoRepository.GetAll().Select(p => p.ToBllPhoto()).ToList();
        }

        public BllPhoto GetOneByPredicate(Expression<Func<BllPhoto, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllPhoto> GetAllByPredicate(Expression<Func<BllPhoto, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllPhoto, DalPhoto>
                (Expression.Parameter(typeof(DalPhoto), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalPhoto, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            return _photoRepository.GetAllByPredicate(exp).Select(p => p.ToBllPhoto()).ToList();
        }

        #endregion
    }
}
