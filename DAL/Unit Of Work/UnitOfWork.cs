using DAL.Interfaces.Interfaces;
using NLog;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private bool _isDisposed = false;
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Error("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (Context != null)
                    {
                        Context.Dispose();
                    }
                }
            }
            _isDisposed = true;
        }
        ~UnitOfWork()
        {
            Dispose(true);
        }
    }
}
