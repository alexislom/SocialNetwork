using AutoMapper;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using DAL.Interface;
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
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFriendRequestRepository friendRequestRepository;

        public FriendRequestService(IUnitOfWork unitOfWork, IFriendRequestRepository friendRequestRepository)
        {
            this.unitOfWork = unitOfWork;
            this.friendRequestRepository = friendRequestRepository;
        }
        public void Create(BllFriendRequest item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllFriendRequest, DalFriendRequest>());
            var dalFriendRequest = Mapper.Map<DalFriendRequest>(item);
            //friendRequestRepository.Create(item.ToDalFriendship());
            friendRequestRepository.Create(dalFriendRequest);
            unitOfWork.Commit();
        }

        public void Delete(BllFriendRequest item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllFriendRequest, DalFriendRequest>());
            var dalFriendRequest = Mapper.Map<DalFriendRequest>(item);
            //friendshipRepository.Delete(item.ToDalFriendship());
            friendRequestRepository.Delete(dalFriendRequest);
            unitOfWork.Commit();
        }

        public IEnumerable<BllFriendRequest> GetAll()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<DalFriendRequest,BllFriendRequest>());
            return friendRequestRepository.GetAll().Select(f => Mapper.Map<BllFriendRequest>(f));
            //return friendRequestRepository.GetAll().Select(f => f.ToBllFriendship());
        }

        public IEnumerable<BllFriendRequest> GetAllByPredicate(Expression<Func<BllFriendRequest, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllFriendRequest, DalFriendRequest>
                (Expression.Parameter(typeof(DalFriendRequest), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalFriendRequest, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            Mapper.Initialize(cfg => cfg.CreateMap<DalFriendRequest, BllFriendRequest>());
            return friendRequestRepository.GetAllByPredicate(exp).Select(f => Mapper.Map<BllFriendRequest>(f)).ToList();
            //return friendRequestRepository.GetAllByPredicate(exp).Select(f => f.ToBllFriendship()).ToList();
        }

        public BllFriendRequest GetById(int id)
        {
            var friendRequest = friendRequestRepository.GetById(id);
            if (friendRequest == null)
                return null;

            Mapper.Initialize(cfg => cfg.CreateMap<DalFriendRequest,BllFriendRequest>());
            var bllFriendRequest = Mapper.Map<BllFriendRequest>(friendRequest);
            return bllFriendRequest;
            //return friendRequest.ToBllFriendship();
        }

        public BllFriendRequest GetOneByPredicate(Expression<Func<BllFriendRequest, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public void Update(BllFriendRequest item)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<BllFriendRequest, DalFriendRequest>());
            var dalFriendRequest = Mapper.Map<DalFriendRequest>(item);

            friendRequestRepository.Update(dalFriendRequest);
            //friendRequestRepository.Update(item.ToDalFriendship());
            unitOfWork.Commit();
        }

        public bool IsFriend(int currentUser, int otherUser)
        {
            return IsRelationExists(currentUser, otherUser, true);
        }

        public bool IsRequested(int currentUser, int otherUser)
        {
            return IsRelationExists(currentUser, otherUser, false);
        }
        public void AddFriend(int userId, int otherUserId)
        {
            var requestsСoincidence = friendRequestRepository
                .GetAllByPredicate(f => ((f.UserFromId == userId && f.UserToId == otherUserId)
               || (f.UserToId == userId && f.UserFromId == otherUserId))).ToList();
            if (requestsСoincidence.Count() == 0)
            {
                var friendship = new BllFriendRequest()
                {
                    UserFromId = userId,
                    RequestDate = DateTime.Now,
                    UserToId = otherUserId,
                    IsConfirmed = false
                };
                Create(friendship);
            }

        }
        public void DeleteAllUserRelationById(int id)
        {
            friendRequestRepository.DeleteAllUserRelationById(id);
            unitOfWork.Commit();
        }

        private bool IsRelationExists(int currentUser, int otherUser, bool isConfirmedState)
        {
            var relation = friendRequestRepository.GetOneByPredicate(f =>
           (f.UserFromId == currentUser && f.UserToId == otherUser && f.IsConfirmed == isConfirmedState) ||
           (f.UserFromId == otherUser && f.UserToId == currentUser && f.IsConfirmed == isConfirmedState));
            if (relation != null)
                return true;
            return false;
        }
    }
}
