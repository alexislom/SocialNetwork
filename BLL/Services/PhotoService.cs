using AutoMapper;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DAL.Interface;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            Mapper.Initialize(cfg => cfg.CreateMap<BllPhoto, DalPhoto>());
            var dalPhoto = Mapper.Map<DalPhoto>(item);

            photoRepository.Create(dalPhoto);
            //photoRepository.Create(item.ToDalPhoto());
            unitOfWork.Commit();
        }

        public void Delete(BllPhoto item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllPhoto, DalPhoto>());
            var dalPhoto = Mapper.Map<DalPhoto>(item);

            photoRepository.Delete(dalPhoto);
            //photoRepository.Delete(item.ToDalPhoto());
            unitOfWork.Commit();
        }

        public IEnumerable<BllPhoto> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DalPhoto,BllPhoto>());

            return photoRepository.GetAll().Select(p => Mapper.Map<BllPhoto>(p)).ToList();
            //return photoRepository.GetAll().Select(p => p.ToBllPhoto()).ToList();
        }

        public IEnumerable<BllPhoto> GetAllByPredicate(Expression<Func<BllPhoto, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllPhoto, DalPhoto>
                (Expression.Parameter(typeof(DalPhoto), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalPhoto, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            Mapper.Initialize(cfg => cfg.CreateMap<DalPhoto, BllPhoto>());
            return photoRepository.GetAllByPredicate(exp).Select(p => Mapper.Map<BllPhoto>(p)).ToList();

            //return photoRepository.GetAllByPredicate(exp).Select(p => p.ToBllPhoto()).ToList();
        }

        public BllPhoto GetById(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DalPhoto, BllPhoto>());
            return Mapper.Map<BllPhoto>(photoRepository.GetById(id));
            //return photoRepository.GetById(id).ToBllPhoto();
        }

        public BllPhoto GetOneByPredicate(Expression<Func<BllPhoto, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public void Update(BllPhoto item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllPhoto,DalPhoto>());
            var dalPhoto = Mapper.Map<DalPhoto>(item);
            photoRepository.Update(dalPhoto);
            //photoRepository.Update(item.ToDalPhoto());
            unitOfWork.Commit();
        }
    }
}
