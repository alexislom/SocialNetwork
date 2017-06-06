using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.Interfaces;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces.DTO;
using ORM.Entities;
using DAL.Mappers;
using DAL.Interface;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly DbContext unitOfWork;

        public UserProfileRepository(DbContext unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void Create(DalUserProfile e)
        {
            unitOfWork.Set<UserProfile>().Add(e.ToOrmUserProfile());
        }

        public void Delete(DalUserProfile e)
        {
            var userProfile = unitOfWork.Set<UserProfile>().FirstOrDefault(p => p.Id == e.Id);
            unitOfWork.Set<UserProfile>().Attach(userProfile);
            unitOfWork.Set<UserProfile>().Remove(userProfile);
            unitOfWork.Entry(userProfile).State = System.Data.Entity.EntityState.Deleted;
        }

        public IEnumerable<DalUserProfile> GetAll()
        {
            return unitOfWork.Set<UserProfile>().Select(p => p.ToDalUserProfile());
        }

        public IEnumerable<DalUserProfile> GetAllByPredicate(Expression<Func<DalUserProfile, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalUserProfile, UserProfile>
                (Expression.Parameter(typeof(UserProfile), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<UserProfile, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = unitOfWork.Set<UserProfile>().Where(express).ToList();
            return final.Select(p => p.ToDalUserProfile());
        }

        public DalUserProfile GetById(int key)
        {
            var profile = unitOfWork.Set<UserProfile>().Find(key);
            if (profile == null)
                return null;
            return profile.ToDalUserProfile();
        }

        public DalUserProfile GetOneByPredicate(Expression<Func<DalUserProfile, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public void Update(DalUserProfile e)
        {
            if (e != null)
            {
                var profile = unitOfWork.Set<UserProfile>().FirstOrDefault(p => p.Id == e.Id);
                if (profile != null)
                {
                    unitOfWork.Set<UserProfile>().Attach(profile);

                    profile.FirstName = e.FirstName ?? profile.FirstName;
                    profile.LastName = e.LastName ?? profile.LastName;
                    profile.DateOfBirth = e.DateOfBirth ?? profile.DateOfBirth;
                    profile.Status = e.Status ?? profile.Status;
                    profile.MobilePhoneNumber = e.MobilePhoneNumber ?? profile.MobilePhoneNumber;
                    profile.City = e.City ?? profile.City;

                    unitOfWork.Entry(profile).State = EntityState.Modified;
                }
            }
        }
    }
}
