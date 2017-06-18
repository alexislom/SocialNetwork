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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        #region CRUD operations

        public void Create(BllUser item)
        {
            _userRepository.Create(item.ToDalUser());
            _unitOfWork.Commit();
        }

        public void Update(BllUser item)
        {
            _userRepository.Update(item.ToDalUser());
            _unitOfWork.Commit();
        }

        public void Delete(BllUser item)
        {
            _userRepository.Delete(item.ToDalUser());
            _unitOfWork.Commit();
        }

        #endregion

        #region Get users

        public BllUser GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return user == null ? null : user.ToBllUser();
        }

        public IEnumerable<BllUser> GetAll()
        {
            return _userRepository.GetAll().Select(u => u.ToBllUser());
        }

        public BllUser GetOneByPredicate(Expression<Func<BllUser, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllUser> GetAllByPredicate(Expression<Func<BllUser, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllUser, DalUser>
                (Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            return _userRepository.GetAllByPredicate(exp).Select(user => user.ToBllUser()).ToList();
        }

        #endregion
    }
}
