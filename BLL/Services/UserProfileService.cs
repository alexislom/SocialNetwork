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
    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileService(IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
        {
            _unitOfWork = unitOfWork;
            _userProfileRepository = userProfileRepository;
        }

        #region CRUD operations

        public void Create(BllUserProfile item)
        {
            _userProfileRepository.Create(item.ToDalUserProfile());
            _unitOfWork.Commit();
        }

        public void Update(BllUserProfile item)
        {
            _userProfileRepository.Update(item.ToDalUserProfile());
            _unitOfWork.Commit();
        }

        public void Delete(BllUserProfile item)
        {
            _userProfileRepository.Delete(item.ToDalUserProfile());
            _unitOfWork.Commit();
        }

        #endregion

        #region Get profiles

        public BllUserProfile GetById(int id)
        {
            var profile = _userProfileRepository.GetById(id);
            return profile == null ? null : profile.ToBllUserProfile();
        }

        public IEnumerable<BllUserProfile> GetAll()
        {
            return _userProfileRepository.GetAll().Select(p => p.ToBllUserProfile());
        }

        public BllUserProfile GetOneByPredicate(Expression<Func<BllUserProfile, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllUserProfile> GetAllByPredicate(Expression<Func<BllUserProfile, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllUserProfile, DalUserProfile>
                (Expression.Parameter(typeof(DalUserProfile), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalUserProfile, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            return _userProfileRepository.GetAllByPredicate(exp).Select(p => p.ToBllUserProfile()).ToList();
        }

        #endregion

        #region  Search

        public IEnumerable<BllUserProfile> Search(BllUserProfile profile)
        {
            var result = _userProfileRepository.GetAll();
            if (!string.IsNullOrEmpty(profile.FirstName))
                result = result.Where(c => (c.FirstName.ToLower()).Contains(profile.FirstName.ToLower()));
            if (!string.IsNullOrEmpty(profile.LastName))
                result = result.Where(c => (c.LastName.ToLower()).Contains(profile.LastName.ToLower()));
            if (!string.IsNullOrEmpty(profile.City))
                result = result.Where(c => (c.City.ToLower()).Contains(profile.City.ToLower()));

            return result.Select(x => x.ToBllUserProfile());
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

        #endregion
    }
}
