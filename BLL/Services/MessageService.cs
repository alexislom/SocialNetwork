﻿using BLL.Interface.Entities;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IMessageRepository messageRepository;

        public MessageService(IUnitOfWork unitOfWork, IMessageRepository messageRepository)
        {
            this.unitOfWork = unitOfWork;
            this.messageRepository = messageRepository;
        }
        public BllMessage GetById(int id)
        {
            var message = messageRepository.GetById(id);
            if (message == null)
                return null;

            return message.ToBllMessage();
        }

        public IEnumerable<BllMessage> GetAll()
        {
            return messageRepository.GetAll().Select(m => m.ToBllMessage());
        }
        public IEnumerable<BllMessage> GetMessages(int UserFrom, int UserTo)
        {
            return messageRepository.GetMessages(UserFrom, UserTo).Select(m => m.ToBllMessage());
        }
        public void Create(BllMessage item)
        {
            messageRepository.Create(item.ToDalMessage());
            unitOfWork.Commit();
        }

        public void Delete(BllMessage item)
        {
            messageRepository.Delete(item.ToDalMessage());
            unitOfWork.Commit();
        }

        public void Update(BllMessage item)
        {
            messageRepository.Update(item.ToDalMessage());
            unitOfWork.Commit();
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

            return messageRepository.GetAllByPredicate(exp).Select(user => user.ToBllMessage()).ToList();
        }


        public IEnumerable<BllMessage> GetAllChatsWith(int userId)
        {
            var messages = GetAllByPredicate(m => m.FromUserId == userId || m.ToUserId == userId);
            HashSet<int?> usersId = new HashSet<int?>();
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
            messageRepository.DeleteAllUserMessagesById(id);
            unitOfWork.Commit();
        }
    }
}