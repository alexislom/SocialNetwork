using DAL.Interface;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Interfaces;
using DAL.Mappers;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DbContext _unitOfWork;

        public MessageRepository(DbContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region CRUD operations

        public void Create(DalMessage e)
        {
            var ormMessage = e.ToOrmMessage();
            _unitOfWork.Set<Message>().Add(ormMessage);
        }

        public void Update(DalMessage e)
        {
            var ormMessage = e.ToOrmMessage();
            var message = _unitOfWork.Set<Message>().FirstOrDefault(m => m.Id == e.Id);
            if (message != null)
            {
                message.TextMessage = ormMessage.TextMessage;
            }
        }

        public void Delete(DalMessage e)
        {
            var message = _unitOfWork.Set<Message>().FirstOrDefault(m => m.Id == e.Id);

            _unitOfWork.Set<Message>().Attach(message);
            _unitOfWork.Set<Message>().Remove(message);
            _unitOfWork.Entry(message).State = EntityState.Deleted;
        }

        #endregion

        #region Get messages

        public IEnumerable<DalMessage> GetAll()
        {
            return _unitOfWork.Set<Message>().Select(m => m.ToDalMessage()).ToList();
        }

        public DalMessage GetById(int key)
        {
            var message = _unitOfWork.Set<Message>().Find(key);
            if (message == null)
                return null;
            return message.ToDalMessage();
        }

        public DalMessage GetOneByPredicate(Expression<Func<DalMessage, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalMessage> GetAllByPredicate(Expression<Func<DalMessage, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalMessage, Message>
                (Expression.Parameter(typeof(Message), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Message, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = _unitOfWork.Set<Message>().Where(express).ToList();
            return final.Select(m => m.ToDalMessage());
        }

        #endregion

        public List<DalMessage> GetMessages(int UserFromId, int UserToId)
        {
            return GetAll().Where(m => (m.FromUserId == UserFromId && m.ToUserId == UserToId) ||
                (m.FromUserId == UserToId && m.ToUserId == UserFromId)).
                OrderBy(m => m.Date).ToList();
        }

        public void DeleteAllUserMessagesById(int id)
        {
            var messages = _unitOfWork.Set<Message>().Where(m => m.FromUserId == id || m.ToUserId == id);
            foreach (var item in messages)
            {
                _unitOfWork.Set<Message>().Attach(item);
                _unitOfWork.Set<Message>().Remove(item);
                _unitOfWork.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }
        }
    }
}
