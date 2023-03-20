using Job_Portal.App.Interfaces;
using Job_Portal.Data.Context;
using Job_Portal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal.App.Implementations
{
    public class UserRepository : Repository<AspNetUser>, IUserRepository
    {
        protected readonly Job_PortalContext _job_portalDbContext;
        public UserRepository(Job_PortalContext job_portalDbContext) : base(job_portalDbContext)
        {
            _job_portalDbContext = job_portalDbContext;
        }

        public AspNetUser? GetByStringId(string id)
        {
            return _job_portalDbContext.AspNetUsers.Include(x => x.Picture).FirstOrDefault(x => x.Id == id);
        }

        public List<AspNetUser> GetAllWithRoles()
        {
            return _job_portalDbContext.AspNetUsers.Include(x => x.Roles).ToList();
        }

        public UserPicture? GetUserPicture(string id)
        {
            return _job_portalDbContext.AspNetUsers.Include(x => x.Picture).FirstOrDefault(x => x.Id == id)?.Picture;
        }

        public Cv? GetUserCv(string id)
        {
            return _job_portalDbContext.AspNetUsers.Include(x => x.Cv).FirstOrDefault(x => x.Id == id)?.Cv;
        }

        public void DeleteUserPicture(UserPicture userPicture)
        {
            _job_portalDbContext.UserPictures.Remove(userPicture);
            _job_portalDbContext.SaveChanges();
        }

        public void AddUserPicture(UserPicture userPicture)
        {
            _job_portalDbContext.UserPictures.Add(userPicture);
            _job_portalDbContext.SaveChanges();
        }

        public void DeleteUserCv(Cv userCv)
        {
            _job_portalDbContext.Cvs.Remove(userCv);
            _job_portalDbContext.SaveChanges();
        }

        public void AddUserCv(Cv userCv)
        {
            _job_portalDbContext.Cvs.Add(userCv);
            _job_portalDbContext.SaveChanges();
        }

        public string GetProfilePicturePath(string userId, int thumbnail)
        {
            try
            {
                var upload = _job_portalDbContext.AspNetUsers.Include(x => x.Picture).FirstOrDefault(x => x.Id == userId)!.Picture;
                var path = "";
                if (upload != null)
                {
                    path = upload.Path;
                }

                var final = "";

                if (!string.IsNullOrEmpty(path))
                {
                    // remove ~
                    var pathwithoutsymbol = path.Substring(1, path.Length - 1);
                    //add_75
                    var splitted = pathwithoutsymbol.Split('.');
                    final = splitted[0] + "_" + thumbnail.ToString() + "." + splitted[1];
                }
                else
                {
                    final = "/uploads/notfound/notfound_75.png";
                }
                return final;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
