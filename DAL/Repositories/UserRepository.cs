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
    public class UserRepository : IUserRepository
    {
        private readonly DbContext unitOfWork;

        public UserRepository(DbContext unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<DalUser> GetAll()
        {
            var users = unitOfWork.Set<User>().Select(user => user).ToList();
            return users.Select(u => u.ToDalUser()).ToList();
        }

        public DalUser GetById(int key)
        {
            var ormUser = unitOfWork.Set<User>().Find(key);
            return ormUser.ToDalUser();
        }

        public void Create(DalUser e)
        {
            var user = e.ToOrmUser();
            unitOfWork.Set<User>().Add(user);
        }

        public void Delete(DalUser e)
        {
            var ormUser = e.ToOrmUser();
            var user = unitOfWork.Set<User>().FirstOrDefault(u => u.Id == ormUser.Id);
            unitOfWork.Set<User>().Attach(user);
            unitOfWork.Set<User>().Remove(user);
            unitOfWork.Entry(user).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(DalUser e)
        {
            if (e != null)
            {
                var userToUpdate = unitOfWork.Set<User>().FirstOrDefault(u => u.Id == e.Id);
                unitOfWork.Set<User>().Attach(userToUpdate);
                userToUpdate.RoleId = e.RoleId;
                userToUpdate.Email = e.Email;
                userToUpdate.Password = e.Password;
                unitOfWork.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public DalUser GetUserByEmail(string email)
        {
            var user = unitOfWork.Set<User>().Where(u => u.Email == email).FirstOrDefault();
            if (user == null)
                return null;
            return user.ToDalUser();
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
            var final = unitOfWork.Set<User>().Where(express).Select(u => u).ToList();
            return final.Select(u => u.ToDalUser()).ToList();
        }
    }
}
