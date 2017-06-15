using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DAL.Interface;
using DAL.Interface.Interfaces;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Mappers;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }

        #region CRUD operations

        public void Create(BllRole item)
        {
            _roleRepository.Create(item.ToDalRole());
            _unitOfWork.Commit();
        }

        public void Update(BllRole item)
        {
            _roleRepository.Update(item.ToDalRole());
            _unitOfWork.Commit();
        }

        public void Delete(BllRole item)
        {
            _roleRepository.Delete(item.ToDalRole());
            _unitOfWork.Commit();
        }

        #endregion

        #region Get profiles

        public BllRole GetById(int id)
        {
            return _roleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<BllRole> GetAll()
        {
            return _roleRepository.GetAll().Select(r => r.ToBllRole());
        }

        public BllRole GetOneByPredicate(Expression<Func<BllRole, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllRole> GetAllByPredicate(Expression<Func<BllRole, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllRole, DalRole>
                (Expression.Parameter(typeof(DalRole), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            return _roleRepository.GetAllByPredicate(exp).Select(user => user.ToBllRole()).ToList();
        }

        #endregion
    }
}
