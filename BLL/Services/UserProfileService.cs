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
using BLL.Mappers;

namespace BLL.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository profileRepository;

        public UserProfileService(IUnitOfWork unitOfWork, IUserProfileRepository profileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.profileRepository = profileRepository;
        }
        public void Create(BllUserProfile item)
        {
            profileRepository.Create(item.ToDalUserProfile());
            unitOfWork.Commit();
        }

        public void Delete(BllUserProfile item)
        {
            profileRepository.Delete(item.ToDalUserProfile());
            unitOfWork.Commit();
        }

        public IEnumerable<BllUserProfile> GetAll()
        {
            return profileRepository.GetAll().Select(p => p.ToBllUserProfile());
        }

        public IEnumerable<BllUserProfile> GetAllByPredicate(Expression<Func<BllUserProfile, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllUserProfile, DalUserProfile>
                (Expression.Parameter(typeof(DalUserProfile), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalUserProfile, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            return profileRepository.GetAllByPredicate(exp).Select(p => p.ToBllUserProfile()).ToList();
        }

        public BllUserProfile GetById(int id)
        {
            var profile = profileRepository.GetById(id);
            if (profile == null)
                return null;

            return profile.ToBllUserProfile();
        }

        public BllUserProfile GetOneByPredicate(Expression<Func<BllUserProfile, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public void Update(BllUserProfile item)
        {
            profileRepository.Update(item.ToDalUserProfile());
            unitOfWork.Commit();
        }

        public IEnumerable<BllUserProfile> Search(BllUserProfile profile)
        {
            var result = profileRepository.GetAllByPredicate(x => x.FirstName == profile.FirstName ||
                                                     x.LastName == profile.LastName || x.City==profile.City);
            return result.Select(x=>x.ToBllUserProfile());
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
