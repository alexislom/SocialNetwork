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
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DbContext unitOfWorkContext;

        public MessageRepository(DbContext unitOfWorkContext)
        {
            this.unitOfWorkContext = unitOfWorkContext;
        }
       
        public void Create(DalMessage e)
        {
            var ormMessage = e.ToOrmMessage();
            unitOfWorkContext.Set<Message>().Add(ormMessage);
    
        }

        public void Delete(DalMessage e)
        {
            var message = unitOfWorkContext.Set<Message>().FirstOrDefault(m => m.Id == e.Id);

            unitOfWorkContext.Set<Message>().Attach(message);
            unitOfWorkContext.Set<Message>().Remove(message);
            unitOfWorkContext.Entry(message).State = System.Data.Entity.EntityState.Deleted;
        }

        public IEnumerable<DalMessage> GetAll()
        {
            return unitOfWorkContext.Set<Message>().Select(m => m.ToDalMessage()).ToList();
        }

        public DalMessage GetById(int key)
        {
            var message = unitOfWorkContext.Set<Message>().Find(key);
            if (message == null)
                return null;
            return message.ToDalMessage();
        }

        public void Update(DalMessage e)
        {
            var ormMessage = e.ToOrmMessage();
            var message = unitOfWorkContext.Set<Message>().FirstOrDefault(m => m.Id == e.Id);
            if(message != null)
            {
                message.TextMessage = ormMessage.TextMessage;
            }
        }

        public List<DalMessage> GetMessages(int UserFromId, int UserToId)
        {
            return GetAll().Where(m => (m.FromUserId == UserFromId && m.ToUserId == UserToId) ||
                (m.FromUserId == UserToId && m.ToUserId == UserFromId)).
                OrderBy(m => m.Date).ToList();
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
            var final = unitOfWorkContext.Set<Message>().Where(express).ToList();
            return final.Select(m => m.ToDalMessage());
        }

        public void DeleteAllUserMessagesById(int id)
        {
            var messages = unitOfWorkContext.Set<Message>().Where(m => m.FromUserId == id || m.ToUserId == id);
            foreach (var item in messages)
            {
                unitOfWorkContext.Set<Message>().Attach(item);
                unitOfWorkContext.Set<Message>().Remove(item);
                unitOfWorkContext.Entry(item).State = System.Data.Entity.EntityState.Deleted;
            }
        }
    }
}
