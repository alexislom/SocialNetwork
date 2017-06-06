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
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext unitOfWork;

        public RoleRepository(DbContext unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void Create(DalRole e)
        {
            unitOfWork.Set<Role>().Add(e.ToOrmRole());
        }

        public void Delete(DalRole e)
        {
            throw new NotImplementedException();
        }

        public void Update(DalRole e)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalRole> GetAll()
        {
            var roles = unitOfWork.Set<Role>().ToList();
            return roles.Select(r => r.ToDalRole()).ToList();
        }

        public DalRole GetById(int key)
        {
            return unitOfWork.Set<Role>().Where(r => r.Id == key).FirstOrDefault().ToDalRole();
        }

        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalRole, Role>
                (Expression.Parameter(typeof(Role), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Role, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = unitOfWork.Set<Role>().Where(express).ToList();
            return final.Select(r => r.ToDalRole());
        }
    }
}
