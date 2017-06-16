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
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFriendRequestRepository _friendRequestRepository;

        public FriendRequestService(IUnitOfWork unitOfWork, IFriendRequestRepository friendRequestRepository)
        {
            _unitOfWork = unitOfWork;
            _friendRequestRepository = friendRequestRepository;
        }

        #region CRUD operations

        public void Create(BllFriendRequest item)
        {
            _friendRequestRepository.Create(item.ToDalFriendRequest());
            _unitOfWork.Commit();
        }

        public void Update(BllFriendRequest item)
        {
            _friendRequestRepository.Update(item.ToDalFriendRequest());
            _unitOfWork.Commit();
        }

        public void Delete(BllFriendRequest item)
        {
            _friendRequestRepository.Delete(item.ToDalFriendRequest());
            _unitOfWork.Commit();
        }

        #endregion

        #region Get profiles

        public BllFriendRequest GetById(int id)
        {
            var friendRequest = _friendRequestRepository.GetById(id);
            return friendRequest == null ? null : friendRequest.ToBllFriendRequest();
        }

        public IEnumerable<BllFriendRequest> GetAll()
        {
            return _friendRequestRepository.GetAll().Select(f => f.ToBllFriendRequest());
        }

        public BllFriendRequest GetOneByPredicate(Expression<Func<BllFriendRequest, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllFriendRequest> GetAllByPredicate(Expression<Func<BllFriendRequest, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllFriendRequest, DalFriendRequest>
                (Expression.Parameter(typeof(DalFriendRequest), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalFriendRequest, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            return _friendRequestRepository.GetAllByPredicate(exp).Select(f => f.ToBllFriendRequest()).ToList();
        }

        #endregion

        #region IFriendRequestService

        public bool IsFriend(int currentUser, int otherUser)
        {
            return IsRelationExists(currentUser, otherUser, true);
        }

        public void AddFriend(int userId, int otherUserId)
        {
            var requestsСoincidence = _friendRequestRepository.GetAllByPredicate(f =>
                                            ((f.UserFromId == userId && f.UserToId == otherUserId)
                                            || (f.UserToId == userId && f.UserFromId == otherUserId))).ToList();

            if (requestsСoincidence.Count() != 0)
                return;
            var friendRequest = new BllFriendRequest
            {
                UserFromId = userId,
                RequestDate = DateTime.Now,
                UserToId = otherUserId,
                IsConfirmed = false
            };
            Create(friendRequest);
        }

        public bool IsRequested(int currentUser, int otherUser)
        {
            return IsRelationExists(currentUser, otherUser, false);
        }

        public void DeleteAllUserRelationById(int id)
        {
            _friendRequestRepository.DeleteAllUserRelationById(id);
            _unitOfWork.Commit();
        }

        #endregion

        #region Private methods

        private bool IsRelationExists(int currentUser, int otherUser, bool isConfirmedState)
        {
            var relation = _friendRequestRepository.GetOneByPredicate(f =>
                (f.UserFromId == currentUser && f.UserToId == otherUser && f.IsConfirmed == isConfirmedState) ||
                (f.UserFromId == otherUser && f.UserToId == currentUser && f.IsConfirmed == isConfirmedState));
            return relation != null;
        }

        #endregion
    }
}
