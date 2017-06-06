using AutoMapper;
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
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRoleRepository roleRepository;

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.roleRepository = roleRepository;
        }

        public BllRole GetById(int id)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DalRole, BllRole>());
            return Mapper.Map<BllRole>(roleRepository.GetById(id)); 
            //return roleRepository.GetById(id).ToBllRole();
        }

        public IEnumerable<BllRole> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DalRole, BllRole>());
            //return roleRepository.GetAll().Select(r => r.ToBllRole());
            return roleRepository.GetAll().Select(r => Mapper.Map<BllRole>(r));
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

            Mapper.Initialize(cfg => cfg.CreateMap<DalRole, BllRole>());
            return roleRepository.GetAllByPredicate(exp).Select(user => Mapper.Map<BllRole>(user)).ToList();
            //return roleRepository.GetAllByPredicate(exp).Select(user => user.ToBllRole()).ToList();
        }

        public void Create(BllRole item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllRole,DalRole>());
            var dalRole = Mapper.Map<DalRole>(item);
            roleRepository.Create(dalRole);
            //roleRepository.Create(item.ToDalRole());
            unitOfWork.Commit();
        }

        public void Delete(BllRole item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllRole, DalRole>());
            var dalRole = Mapper.Map<DalRole>(item);
            roleRepository.Delete(dalRole);
            //roleRepository.Delete(item.ToDalRole());
            unitOfWork.Commit();
        }

        public void Update(BllRole item)
        {
            //throw new NotImplementedException();
            Mapper.Initialize(cfg => cfg.CreateMap<BllRole, DalRole>());
            var dalRole = Mapper.Map<DalRole>(item);
            roleRepository.Update(dalRole);
            //roleRepository.Delete(item.ToDalRole());
            unitOfWork.Commit();
        }
    }
}
