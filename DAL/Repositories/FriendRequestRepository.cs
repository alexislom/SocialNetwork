using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ORM.Entities;
using System.Linq.Expressions;
using DAL.Interface;
using DAL.Mappers;
using System.Data.Entity.Migrations;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Interfaces;

namespace DAL.Repositories
{
    public class FriendRequestRepository : IFriendRequestRepository
    {
        private readonly DbContext _unitOfWork;

        public FriendRequestRepository(DbContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CRUD operations

        public void Create(DalFriendRequest e) => 
            _unitOfWork.Set<FriendRequest>().Add(e.ToOrmFriendRequest());

        public void Update(DalFriendRequest e)
        {
            if (e != null)
            {
                _unitOfWork.Set<FriendRequest>().AddOrUpdate(e.ToOrmFriendRequest());
            }
        }

        public void Delete(DalFriendRequest e)
        {
            if (e != null)
            {
                var ormFriendRequest = e.ToOrmFriendRequest();
                var friendship = _unitOfWork.Set<FriendRequest>().FirstOrDefault(f => f.Id == ormFriendRequest.Id);
                _unitOfWork.Set<FriendRequest>().Attach(friendship);
                _unitOfWork.Set<FriendRequest>().Remove(friendship);
                _unitOfWork.Entry(friendship).State = EntityState.Deleted;
            }
        }

        #endregion

        #region Get requests

        public IEnumerable<DalFriendRequest> GetAll()
        {
            return _unitOfWork.Set<FriendRequest>().Select(m => m.ToDalFriendRequest());
        }

        public DalFriendRequest GetById(int key)
        {
            var friendship = _unitOfWork.Set<FriendRequest>().Find(key);
            return friendship == null ? null : friendship.ToDalFriendRequest();
        }

        public DalFriendRequest GetOneByPredicate(Expression<Func<DalFriendRequest, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalFriendRequest> GetAllByPredicate(Expression<Func<DalFriendRequest, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalFriendRequest, FriendRequest>
                (Expression.Parameter(typeof(FriendRequest), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<FriendRequest, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = _unitOfWork.Set<FriendRequest>().Where(express).ToList();
            return final.Select(f => f.ToDalFriendRequest());
        }

        #endregion

        public void DeleteAllUserRelationById(int id)
        {
            var friendships = _unitOfWork.Set<FriendRequest>()
                .Where(f => f.UserFromId == id || f.UserToId == id);

            foreach (var item in friendships)
            {
                _unitOfWork.Set<FriendRequest>().Attach(item);
                _unitOfWork.Set<FriendRequest>().Remove(item);
                _unitOfWork.Entry(item).State = EntityState.Deleted;
            }
        }
    }
}
