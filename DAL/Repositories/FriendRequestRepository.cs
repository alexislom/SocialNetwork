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
        private readonly DbContext unitOfWorkContext;

        public FriendRequestRepository(DbContext unitOfWorkContext)
        {
            this.unitOfWorkContext = unitOfWorkContext;
        }
        public void Create(DalFriendRequest e)
        {
            unitOfWorkContext.Set<FriendRequest>().Add(e.ToOrmFriendRequest());
        }

        public void Delete(DalFriendRequest e)
        {
            if (e != null)
            {
                var ormFriendRequest = e.ToOrmFriendRequest();
                var friendship = unitOfWorkContext.Set<FriendRequest>().FirstOrDefault(f => f.Id == ormFriendRequest.Id);
                unitOfWorkContext.Set<FriendRequest>().Attach(friendship);
                unitOfWorkContext.Set<FriendRequest>().Remove(friendship);
                unitOfWorkContext.Entry(friendship).State = EntityState.Deleted;
            }
        }

        public IEnumerable<DalFriendRequest> GetAll()
        {
            return unitOfWorkContext.Set<FriendRequest>().Select(m => m.ToDalFriendRequest());
        }

        public IEnumerable<DalFriendRequest> GetAllByPredicate(Expression<Func<DalFriendRequest, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalFriendRequest, FriendRequest>
                (Expression.Parameter(typeof(FriendRequest), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<FriendRequest, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = unitOfWorkContext.Set<FriendRequest>().Where(express).ToList();
            return final.Select(f => f.ToDalFriendRequest());
        }

        public DalFriendRequest GetById(int key)
        {
            var friendship = unitOfWorkContext.Set<FriendRequest>().Find(key);
            if (friendship == null)
                return null;
            return friendship.ToDalFriendRequest();
        }

        public DalFriendRequest GetOneByPredicate(Expression<Func<DalFriendRequest, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public void Update(DalFriendRequest e)
        {
            if (e != null)
            {
                unitOfWorkContext.Set<FriendRequest>().AddOrUpdate(e.ToOrmFriendRequest());
            }

        }

        public void DeleteAllUserRelationById(int id)
        {
            var friendships = unitOfWorkContext.Set<FriendRequest>()
                                        .Where(f => f.UserFromId == id || f.UserToId == id);

            foreach (var item in friendships)
            {
                unitOfWorkContext.Set<FriendRequest>().Attach(item);
                unitOfWorkContext.Set<FriendRequest>().Remove(item);
                unitOfWorkContext.Entry(item).State = EntityState.Deleted;
            }
        }
    }
}
