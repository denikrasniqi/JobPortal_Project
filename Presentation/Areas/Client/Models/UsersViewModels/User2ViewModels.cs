using Job_Portal.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Areas.Client.Models.UsersViewModels
{
    public class User2ViewModels
    {
        public string? Id { get; set; }
 
        public string? Name { get; set; }
      
        public string? Surname { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public bool IsPictureDeleted { get; set; }

        public IFormFile? Picture { get; set; }

        public UserPicture? UserPicture { get; set; }

        public IFormFile? Cv { get; set; }

        public Cv? UserCv { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Experience { get; set; }
        public string? Education { get; set; }
        public string? Skills { get; set; }
        public string? Projects { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Linkedin { get; set; }
        public string? GitHub { get; set; }
        public DateTime? Birthday { get; set; }

   
    }
}
