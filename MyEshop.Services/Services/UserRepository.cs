using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyEshop.Services
{

    using MyEshop.DomainClass;
    using MyEshop.DataLayer;
    using MyEshop.ViewModel;
    using MyEshop.ViewModel.User;
    using System.Globalization;

    public class UserRepository : IUserRepository
    {
        #region Ctor

        private MyEshopDbContext _context;
        public UserRepository(MyEshopDbContext context)
        {
            this._context = context;
        }

        #endregion

        #region Users

        public void InsertUser(User user)
        {
            _context.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var local = _context.Set<User>()
              .Local
              .FirstOrDefault(f => f.UserId == user.UserId);
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(int userId)
        {
            User user = GetUserByUserId(userId);
            user.IsDelete = true;
        }

        public User GetUserByUserId(int userId)
        {
            return _context.Users.Find(userId);
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.OrderByDescending(u => u.UserId);
        }

        public bool IsExistUserWithEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email.Trim().ToLower());
        }

        public bool IsExistUserWithUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }

        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }

        public User GetUserByEmailPass(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName);
        }

        public void ReturnUser(int userId)
        {
            User user = _context.Users.Find(userId);
            user.IsDelete = false;
        }

        public User GetUserForEditByUserId(int userId)
        {
            User currentUser = _context.Users.Find(userId);
            currentUser.ViewByAdmin = true;
            _context.SaveChanges();

            return currentUser;
        }

        public FilterUsersViewModel GetUsersByFilter(FilterUsersViewModel filter)
        {
            int take = filter.Take;
            int skip = (filter.PageId - 1) * take;

            FilterUsersViewModel data = new FilterUsersViewModel();
            IQueryable<User> users = _context.Users;

            #region State

            switch (filter.State)
            {
                case "All":
                    {
                        break;
                    }
                case "Active":
                    {
                        users = users.Where(u => u.IsActive && !u.IsDelete);
                        break;
                    }
                case "NotActive":
                    {
                        users = users.Where(u => !u.IsActive && !u.IsDelete);
                        break;
                    }
                case "Deleted":
                    {
                        users = users.Where(u => u.IsDelete);
                        break;
                    }
            }

            data.State = filter.State;

            #endregion

            #region Impelementing Filters

            if (!string.IsNullOrEmpty(filter.UserName))
            {
                users = users.Where(u => u.UserName.Trim().ToLower().Contains(filter.UserName.Trim().ToLower()));
                data.UserName = filter.UserName;
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                users = users.Where(u => u.Email.Trim().ToLower().Contains(filter.Email.Trim().ToLower()));
                data.Email = filter.Email;
            }

            if (filter.FromeDate != null)
            {
                DateTime fromDate = new DateTime(filter.FromeDate.Value.Year, filter.FromeDate.Value.Month, filter.FromeDate.Value.Day, new PersianCalendar());
                users = users.Where(u => u.RegisterDate >= fromDate);
                data.FromeDate = filter.FromeDate;
            }

            if (filter.ToDate != null)
            {
                DateTime toDate = new DateTime(filter.ToDate.Value.Year, filter.ToDate.Value.Month, filter.ToDate.Value.Day, new PersianCalendar());
                users = users.Where(u => u.RegisterDate <= toDate);
                data.ToDate = filter.ToDate;
            }

            #endregion

            #region Pagging

            int thisPageCount = users.Count();
            if (thisPageCount % take > 0)
            {
                data.PageCount = (thisPageCount / take) + 1;
            }
            else
            {
                data.PageCount = thisPageCount / take;
            }

            data.ActivePage = filter.PageId;
            data.StartPage = filter.PageId - 3;
            data.EndPage = data.ActivePage + 3;
            if (data.StartPage <= 0) data.StartPage = 1;
            if (data.EndPage > data.PageCount) data.EndPage = data.PageCount;

            #endregion

            data.Users = users.OrderByDescending(u => u.RegisterDate).Skip(skip).Take(take).AsNoTracking().ToList();

            return data;
        }

        #endregion

        #region UserRole

        public string[] GetUserRoleByUserId(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId).Select(u => u.UserRole.RoleName).ToArray();
        }

        #endregion

        #region User Roles

        public IEnumerable<UserRole> GetUserRoles()
        {
            return _context.UserRoles;

        }

        public IEnumerable<UserRole> GetActiveUserRoles()
        {
            return _context.UserRoles.Where(r => !r.IsDelete);
        }

        #endregion

        #region User Profiles



        #endregion

        #region User Panel

        public UserPanelIndexViewModel GetUserPanelIndexData(int userId)
        {
            UserPanelIndexViewModel data = new UserPanelIndexViewModel();
            data.User = GetUserByUserId(userId);

            return data;
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            _context.Dispose();
        }

        























        #endregion

    }
}
