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
using BLL.Mappers;

namespace BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IUnitOfWork unitOfWork, IMessageRepository messageRepository)
        {
            _unitOfWork = unitOfWork;
            _messageRepository = messageRepository;
        }

        #region CRUD operations

        public void Create(BllMessage item)
        {
            _messageRepository.Create(item.ToDalMessage());
            _unitOfWork.Commit();
        }

        public void Update(BllMessage item)
        {
            _messageRepository.Update(item.ToDalMessage());
            _unitOfWork.Commit();
        }

        public void Delete(BllMessage item)
        {
            _messageRepository.Delete(item.ToDalMessage());
            _unitOfWork.Commit();
        }

        #endregion

        #region Get profiles

        public BllMessage GetById(int id)
        {
            var message = _messageRepository.GetById(id);
            return message == null ? null : message.ToBllMessage();
        }

        public IEnumerable<BllMessage> GetAll()
        {
            return _messageRepository.GetAll().Select(m => m.ToBllMessage());
        }

        public BllMessage GetOneByPredicate(Expression<Func<BllMessage, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllMessage> GetAllByPredicate(Expression<Func<BllMessage, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllMessage, DalMessage>
                (Expression.Parameter(typeof(DalMessage), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalMessage, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);

            return _messageRepository.GetAllByPredicate(exp).Select(user => user.ToBllMessage()).ToList();
        }

        #endregion

        #region IMessageService

        public IEnumerable<BllMessage> GetAllChatsWith(int userId)
        {
            var messages = GetAllByPredicate(m => m.FromUserId == userId || m.ToUserId == userId);
            var usersId = new HashSet<int?>();
            foreach (var item in messages)
            {
                if (item.FromUserId == userId)
                    usersId.Add(item.ToUserId);
                else usersId.Add(item.FromUserId);
            }
            var lastmsgList = new List<BllMessage>();
            foreach (var item in usersId)
            {
                var lastmsg = messages
                    .Where(m => (m.FromUserId == item && m.ToUserId == userId)
                                || (m.ToUserId == item && m.FromUserId == userId)).Last();
                lastmsgList.Add(lastmsg);
            }
            return lastmsgList;
        }

        public void DeleteAllUserMessagesById(int id)
        {
            _messageRepository.DeleteAllUserMessagesById(id);
            _unitOfWork.Commit();
        }

        #endregion

        public IEnumerable<BllMessage> GetMessages(int UserFrom, int UserTo)
        {
            return _messageRepository.GetMessages(UserFrom, UserTo).Select(m => m.ToBllMessage());
        }
    }
}
