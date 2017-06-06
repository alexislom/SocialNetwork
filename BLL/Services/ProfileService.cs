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
    public class ProfileService : IUserProfileService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository profileRepository;

        public ProfileService(IUnitOfWork unitOfWork, IUserProfileRepository profileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.profileRepository = profileRepository;
        }
        public void Create(BllUserProfile item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllUserProfile, DalUserProfile>());
            var dalUserProfile = Mapper.Map<DalUserProfile>(item);
            profileRepository.Create(dalUserProfile);
            //profileRepository.Create(item.ToDalProfile());
            unitOfWork.Commit();
        }

        public void Delete(BllUserProfile item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllUserProfile, DalUserProfile>());
            var dalUserProfile = Mapper.Map<DalUserProfile>(item);
            profileRepository.Delete(dalUserProfile);
            //profileRepository.Delete(item.ToDalProfile());
            unitOfWork.Commit();
        }

        public IEnumerable<BllUserProfile> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DalUserProfile,BllUserProfile>());
            return profileRepository.GetAll().Select(p => Mapper.Map<BllUserProfile>(p));
            //return profileRepository.GetAll().Select(p => p.ToBllProfile());
        }

        public IEnumerable<BllUserProfile> GetAllByPredicate(Expression<Func<BllUserProfile, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllUserProfile, DalUserProfile>
                (Expression.Parameter(typeof(DalUserProfile), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalUserProfile, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            Mapper.Initialize(cfg => cfg.CreateMap<DalUserProfile, BllUserProfile>());
            return profileRepository.GetAllByPredicate(exp).Select(p => Mapper.Map<BllUserProfile>(p)).ToList();
            //return profileRepository.GetAllByPredicate(exp).Select(p => p.ToBllProfile()).ToList();
        }

        public BllUserProfile GetById(int id)
        {
            var profile = profileRepository.GetById(id);
            if (profile == null)
                return null;

            Mapper.Initialize(cfg => cfg.CreateMap<DalUserProfile, BllUserProfile>());
            return Mapper.Map<BllUserProfile>(profile);
            //return profile.ToBllProfile();
        }

        public BllUserProfile GetOneByPredicate(Expression<Func<BllUserProfile, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public void Update(BllUserProfile item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllUserProfile,DalUserProfile>());
            var bllUserProfile =  Mapper.Map<DalUserProfile>(item);

            profileRepository.Update(bllUserProfile);
            //profileRepository.Update(item.ToDalProfile());
            unitOfWork.Commit();
        }

        public IEnumerable<BllUserProfile> Search(BllUserProfile profile)
        {
            //var resultSet = new HashSet<BllUserProfile>(new BllProfileComparer());
            //SearchByStringParameter(profile.NickName, ref resultSet, p => p.NickName.Contains(profile.NickName));
            //SearchByStringParameter(profile.FirstName, ref resultSet, p => p.FirstName.Contains(profile.FirstName));
            //SearchByStringParameter(profile.LastName, ref resultSet, p => p.LastName.Contains(profile.LastName));
            //SearchByStringParameter(profile.City, ref resultSet, p => p.City.Contains(profile.City));
            //var result = resultSet.Where(p => p.Gender == profile.Gender).Select(p => p).ToList();
            return null;
        }

        private void SearchByStringParameter(string parameter, ref HashSet<BllUserProfile> collection, 
                                                Expression<Func<DalUserProfile, bool>> predicates)
        {
            //if (parameter != null)
            //{
            //    var dalProfileList = profileRepository.GetAllByPredicate(predicates).ToList();
            //    var bllProfileList = dalProfileList.Select(p => p.ToBllProfile()).ToList();
            //    if (collection.Count == 0)
            //        foreach (var item in bllProfileList)
            //            collection.Add(item);
            //    else
            //        collection.IntersectWith(bllProfileList);
            //}
        }
    }
}
