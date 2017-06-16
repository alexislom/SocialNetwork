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
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _unitOfWork;

        public UserRepository(DbContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CRUD operations

        public void Create(DalUser e)
        {
            var user = e.ToOrmUser();
            _unitOfWork.Set<User>().Add(user);
        }

        public void Update(DalUser e)
        {
            if (e != null)
            {
                var userToUpdate = _unitOfWork.Set<User>().FirstOrDefault(u => u.Id == e.Id);
                _unitOfWork.Set<User>().Attach(userToUpdate);
                userToUpdate.RoleId = e.RoleId;
                userToUpdate.Email = e.Email;
                userToUpdate.Password = e.Password;
                _unitOfWork.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(DalUser e)
        {
            var ormUser = e.ToOrmUser();
            var user = _unitOfWork.Set<User>().FirstOrDefault(u => u.Id == ormUser.Id);
            _unitOfWork.Set<User>().Attach(user);
            _unitOfWork.Set<User>().Remove(user);
            _unitOfWork.Entry(user).State = System.Data.Entity.EntityState.Deleted;
        }

        #endregion

        #region Get users

        public IEnumerable<DalUser> GetAll()
        {
            var users = _unitOfWork.Set<User>().Select(user => user).ToList();
            return users.Select(u => u.ToDalUser()).ToList();
        }

        public DalUser GetById(int key)
        {
            var ormUser = _unitOfWork.Set<User>().Find(key);
            return ormUser.ToDalUser();
        }

        public DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalUser, User>
                (Expression.Parameter(typeof(User), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<User, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = _unitOfWork.Set<User>().Where(express).Select(u => u).ToList();
            return final.Select(u => u.ToDalUser()).ToList();
        }

        #endregion

        public DalUser GetUserByEmail(string email)
        {
            var user = _unitOfWork.Set<User>().FirstOrDefault(u => u.Email == email);
            return user == null ? null : user.ToDalUser();
        }
    }
}
