using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyEshop.Services
{

    using MyEshop.DomainClass;
    using MyEshop.ViewModel;
    using MyEshop.ViewModel.User;

    public interface IUserRepository:IDisposable
    {
        #region Users

        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        User GetUserByUserId(int userId);
        IEnumerable<User> GetUsers();
        bool IsExistUserWithEmail(string email);
        bool IsExistUserWithUserName(string userName);
        User GetUserByActiveCode(string activeCode);
        User GetUserByEmailPass(string email, string password);
        User GetUserByEmail(string email);
        User GetUserByUserName(string userName);
        void ReturnUser(int userId);
        User GetUserForEditByUserId(int userId);
        FilterUsersViewModel GetUsersByFilter(FilterUsersViewModel filter);

        #endregion

        #region User Role

        string[] GetUserRoleByUserId(int userId);

        #endregion

        #region User Roles

        IEnumerable<UserRole> GetUserRoles();
        IEnumerable<UserRole> GetActiveUserRoles();

        #endregion

        #region Users Profiles



        #endregion

        #region User Panel

        UserPanelIndexViewModel GetUserPanelIndexData(int userId);

        #endregion

    }

    internal class filterusersviewmodel
    {
    }
}
