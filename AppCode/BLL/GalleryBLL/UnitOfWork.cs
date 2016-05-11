using OWDARO.Helpers;
using OWDARO.Util;
using ProjectJKL.AppCode.DAL.GalleryModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OWDARO.BLL.GalleryBLL
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable
    {
        #region Private member variables...

        private GalleryEntities _context = null;
        private GenericRepository<GY_Albums> _albumsRepository;
        private GenericRepository<GY_AudioCategories> _audioCategoriesRepository;
        private GenericRepository<GY_Audios> _audiosRepository;
        #endregion

        public UnitOfWork()
        {
            _context = new GalleryEntities();
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get/Set Property for product repository.
        /// </summary>
        public GenericRepository<GY_AudioCategories> AudioCategoriesRepository
        {
            get
            {
                if (this._audioCategoriesRepository == null)
                    this._audioCategoriesRepository = new GenericRepository<GY_AudioCategories>(_context);
                return _audioCategoriesRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<GY_Albums> AlbumsRepository
        {
            get
            {
                if (this._albumsRepository == null)
                    this._albumsRepository = new GenericRepository<GY_Albums>(_context);
                return _albumsRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for token repository.
        /// </summary>
        public GenericRepository<GY_Audios> AudiosRepository
        {
            get
            {
                if (this._audiosRepository == null)
                    this._audiosRepository = new GenericRepository<GY_Audios>(_context);
                return _audiosRepository;
            }
        }
        #endregion

        #region Public member methods...
        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", Utilities.DateTimeNow(),
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        #endregion

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}