using TARS.DataModel.GenericRepository.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using TARS.DataModel.DataModel;

namespace TARS.DataModel.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        #region Private member variables

        private TARSEntities _context = null;
        private Repository<Victim> _victimRepository;
        private Repository<VictimPhoto> _victimPhotoRepository;
        private Repository<Donor> _donorRepository;
        private Repository<VictimProfile> _victimViewRepository;
        private Repository<VictimPercentageRate> _victimViewPercentageProfile;
        private Repository<VictimPhotoPercentage> _victimViewPercentagePhoto;
        private Repository<State> _stateRepository;
        private Repository<Lga> _lgaRepository;
        private Repository<City> _cityRepository;
        #endregion

        #region public constructor
        public UnitOfWork()
        {
            _context = new TARSEntities();
        }
        #endregion


        #region public methods
        public Repository<State> StateRepository
        {
            get
            {
                if (this._stateRepository == null)
                    this._stateRepository = new Repository<State>(_context);
                return _stateRepository;
            }
        }

        public Repository<Lga> LgaRepository
        {
            get
            {
                if (this._lgaRepository == null)
                    this._lgaRepository = new Repository<Lga>(_context);
                return _lgaRepository;
            }
        }

        public Repository<City> CityRepository
        {
            get
            {
                if (this._cityRepository == null)
                    this._cityRepository = new Repository<City>(_context);
                return _cityRepository;
            }
        }

        public Repository<Victim> victimRepository
        {
            get
            {
                if (this._victimRepository == null)
                    this._victimRepository = new Repository<Victim>(_context);
                return _victimRepository;
            }
        }

        public Repository<VictimProfile> victimProfileRepository
        {
            get
            {
                if (this._victimViewRepository == null)
                    this._victimViewRepository = new Repository<VictimProfile>(_context);
                return _victimViewRepository;
            }
        }

        public Repository<VictimPhoto> victimPhotoRepository
        {
            get
            {
                if (this._victimPhotoRepository == null)
                    this._victimPhotoRepository = new Repository<VictimPhoto>(_context);
                return _victimPhotoRepository;
            }
        }
        public Repository<Donor> DonorRepository
        {
            get
            {
                if (this._donorRepository == null)
                    this._donorRepository = new Repository<Donor>(_context);
                return _donorRepository;
            }
        }

        public Repository<VictimPercentageRate> victimPercProfileRepository
        {
            get
            {
                if (this._victimViewPercentageProfile == null)
                    this._victimViewPercentageProfile = new Repository<VictimPercentageRate>(_context);
                return _victimViewPercentageProfile;
            }
        }
        public Repository<VictimPhotoPercentage> victimPercPhotoRepository
        {
            get
            {
                if (this._victimViewPercentagePhoto == null)
                    this._victimViewPercentagePhoto = new Repository<VictimPhotoPercentage>(_context);
                return _victimViewPercentagePhoto;
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
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
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


        #region dispose members
        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


    }
}
