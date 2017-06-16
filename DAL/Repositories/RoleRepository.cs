using DAL.Interface;
using DAL.Interface.Interfaces;
using DAL.Interfaces.DTO;
using DAL.Mappers;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext _unitOfWork;

        public RoleRepository(DbContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CRUD operations

        public void Create(DalRole e) => _unitOfWork.Set<Role>().Add(e.ToOrmRole());

        public void Update(DalRole e)
        {
            throw new NotImplementedException();
        }

        public void Delete(DalRole e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Get roles

        public IEnumerable<DalRole> GetAll()
        {
            var roles = _unitOfWork.Set<Role>().ToList();
            return roles.Select(r => r.ToDalRole()).ToList();
        }

        public DalRole GetById(int key) => 
            _unitOfWork.Set<Role>().Where(r => r.Id == key).FirstOrDefault().ToDalRole();

        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalRole, Role>
                (Expression.Parameter(typeof(Role), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Role, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = _unitOfWork.Set<Role>().Where(express).ToList();
            return final.Select(r => r.ToDalRole());
        }

        #endregion
    }
}
