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
        private readonly IPhotoRepository photoRepository;
        private readonly IUnitOfWork unitOfWork;

        public PhotoService(IUnitOfWork unitOfWork, IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(BllPhoto item)
        {
            photoRepository.Create(item.ToDalPhoto());
            unitOfWork.Commit();
        }

        public void Delete(BllPhoto item)
        {
            photoRepository.Delete(item.ToDalPhoto());
            unitOfWork.Commit();
        }

        public IEnumerable<BllPhoto> GetAll()
        {
            return photoRepository.GetAll().Select(p => p.ToBllPhoto()).ToList();
        }

        public IEnumerable<BllPhoto> GetAllByPredicate(Expression<Func<BllPhoto, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllPhoto, DalPhoto>
                (Expression.Parameter(typeof(DalPhoto), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalPhoto, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            return photoRepository.GetAllByPredicate(exp).Select(p => p.ToBllPhoto()).ToList();
        }

        public BllPhoto GetById(int id)
        {
            return photoRepository.GetById(id).ToBllPhoto();
        }

        public BllPhoto GetOneByPredicate(Expression<Func<BllPhoto, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public void Update(BllPhoto item)
        {
            photoRepository.Update(item.ToDalPhoto());
            unitOfWork.Commit();
        }
    }
}
