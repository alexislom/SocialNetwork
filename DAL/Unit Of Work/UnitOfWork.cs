using DAL.Interfaces.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using CustomLogger;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger _logger = CustLogger.GetCurrentClassLogger;
        private bool _isDisposed = false;
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        #region Transaction

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
                    _logger.Error($"Entity of type \"{eve.Entry.Entity.GetType().Name}\"" + Environment.NewLine +
                        $"in state \"{eve.Entry.State}\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        _logger.Error($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                throw;
            }
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
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
