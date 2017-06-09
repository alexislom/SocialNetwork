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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        public void Create(BllUser item)
        {
            userRepository.Create(item.ToDalUser());
            unitOfWork.Commit();
        }

        public void Delete(BllUser item)
        {
            userRepository.Delete(item.ToDalUser());
            unitOfWork.Commit();
        }

        public IEnumerable<BllUser> GetAll()
        {
            return userRepository.GetAll().Select(u => u.ToBllUser());
        }

        public IEnumerable<BllUser> GetAllByPredicate(Expression<Func<BllUser, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllUser, DalUser>
                (Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            return userRepository.GetAllByPredicate(exp).Select(user => user.ToBllUser()).ToList();
        }

        public BllUser GetById(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
                return null;

            return user.ToBllUser();
        }

        public BllUser GetOneByPredicate(Expression<Func<BllUser, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public void Update(BllUser item)
        {
            userRepository.Update(item.ToDalUser());
            unitOfWork.Commit();
        }
    }
}
