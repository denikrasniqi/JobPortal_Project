using Job_Portal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal.App.Interfaces
{
    public interface IUserRepository : IRepository<AspNetUser>
    {
        AspNetUser? GetByStringId(string id);
        UserPicture? GetUserPicture(string id);
       //UserPicture? GetUserCv(string id);
        List<AspNetUser> GetAllWithRoles();
        string GetProfilePicturePath(string userId, int thumbnail);
        //string GetCVPath(string userId,);
        void DeleteUserPicture(UserPicture userPicture);
        void AddUserPicture(UserPicture userPicture);
       // void DeleteUserCv(Cv userCv);
        //void AddUserCv(Cv userCv);
    }
}
