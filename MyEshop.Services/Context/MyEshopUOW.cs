using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEshop.DataLayer;

namespace MyEshop.Services
{
    public class MyEshopUOW : IDisposable
    {

        private MyEshopDbContext _context = new MyEshopDbContext();


        #region User Repository

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        #endregion

        #region ProductRepository

        private IProductRepository _productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository==null)
                {
                    _productRepository = new ProductRepository(_context);
                }

                return _productRepository;
            }
        }

        #endregion

        #region Save

        public void Save()
        {
            _context.SaveChanges();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            _context?.Dispose();
        }

        #endregion

    }
}
