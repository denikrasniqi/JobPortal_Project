using Job_Portal.App.Constants;
using Job_Portal.App.Enumerations;
using Job_Portal.App.Interfaces;
using Job_Portal.Data.Entities;
using Job_Portal.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Areas.Client.Models.UsersViewModels;
using Presentation.FileHelper;
using System.Net;

namespace Presentation.Areas.Client.Controllers
{
    [Area(AreasConstants.Client)]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRolesRepository rolesRepository;
        private readonly ILogger _logger;
        private readonly IFileHelper _fileHelper;
        private readonly ISelectListService selectListService;

        public UsersController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IRolesRepository rolesRepository, ILogger<UsersController> logger, ISelectListService selectListService, IFileHelper fileHelper)
        {
            this.userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.rolesRepository = rolesRepository;
            _logger = logger;
            this.selectListService = selectListService;
            _fileHelper = fileHelper;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Account(string? id)
        {
            var model = new User2ViewModels();
            if (id != null)
            {
                AspNetUser? user = userRepository.GetByStringId(id);
                if (user != null)
                {
                    model = new User2ViewModels()
                    {
                        Id = id,
                        Password = "",
                        ConfirmPassword = "",
                        Email = user.Email!,
                        Name = user.Name!,
                        PhoneNumber = user.PhoneNumber,
                        Surname = user.Surname!,
                        UserPicture = user.Picture,
                        Address = user.Address,
                        Experience = user.Experience,
                        Education = user.Education,
                        Gender = user.Gender,
                        Skills = user.Skills,
                        Projects = user.Projects,
                        Birthday = user.Birthday,
                        Facebook = user.Facebook,
                        Twitter = user.Twitter,
                        Linkedin = user.Linkedin,
                        GitHub = user.Linkedin,
                    };

                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Account(User2ViewModels model)
        {
            if (ModelState.IsValid)
            {
                string userId = "";
                if (string.IsNullOrEmpty(model.Id))
                {
                    var user = new ApplicationUser
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        UserName = model.Email,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Address = model.Address,
                        Experience = model.Experience,
                        Education = model.Education,
                        Gender = model.Gender,
                        Skills = model.Skills,
                        Projects = model.Projects,
                        Birthday = model.Birthday,
                        Facebook = model.Facebook,
                        Twitter = model.Twitter,
                        Linkedin = model.Linkedin,
                        GitHub = model.GitHub,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        userId = user.Id;
                        _logger.LogInformation("User created a new account with password.");
                        var roleResult = await _userManager.AddToRoleAsync(user, "Client");
                        if (roleResult.Succeeded)
                        {
                            _logger.LogInformation($"User created with role Client");
                        }
                    }
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(model.Id);
                    if (user != null)
                    {
                        userId = user.Id;
                        user.Name = model.Name;
                        user.Surname = model.Surname;
                        user.Email = model.Email;
                        user.PhoneNumber = model.PhoneNumber;
                        user.Address = model.Address;
                        user.Experience = model.Experience;
                        user.Education = model.Education;
                        user.Gender = model.Gender;
                        user.Skills = model.Skills;
                        user.Projects = model.Projects;
                        user.Birthday = model.Birthday;
                        user.Facebook = model.Facebook;
                        user.Twitter = model.Twitter;
                        user.Linkedin = model.Linkedin;
                        user.GitHub = model.Linkedin;


                        var editResult = await _userManager.UpdateAsync(user);

                        if (model.IsPictureDeleted)
                        {
                            var findExisting = userRepository.GetUserPicture(user.Id);
                            if (findExisting != null)
                            {
                                userRepository.DeleteUserPicture(findExisting);
                            }
                        }
                    }
                }

                if (model.Picture != null)
                {
                    var fileName = Path.GetFileName(model.Picture.FileName);
                    var uploadPath = "~/uploads/users/" + userId.ToString() + "/Image/" + fileName;

                    _fileHelper.SaveFile(FileTypesEnum.Image, model.Picture, "users", userId.ToString(), (int)ThumbnailsEnum.Grid, (int)ThumbnailsEnum.Catalog);

                    var findExisting = userRepository.GetUserPicture(userId);
                    if (findExisting != null)
                    {
                        userRepository.DeleteUserPicture(findExisting);
                    }
                    var userPicture = new UserPicture
                    {
                        FileName = fileName,
                        Path = uploadPath,
                        Extension = Path.GetExtension(fileName)
                    };
                    userRepository.AddUserPicture(userPicture);

                    var editUser = userRepository.GetByStringId(userId);
                    if (editUser != null)
                    {
                        editUser.PictureId = userPicture.Id;
                        userRepository.Update(editUser);
                        userRepository.SaveChanges();
                    }
                       }
            }
            return RedirectToAction("Index", "Home");
        }
        //    if (model.Cv != null)
        //    {
        //        var fileName = Path.GetFileName(model.Cv.FileName);
        //        var uploadPath = "~/uploads/users/" + userId.ToString() + "/File/" + fileName;

        //        _fileHelper.SaveFile(FileTypesEnum.Image, model.Cv, "users", userId.ToString(), (int)ThumbnailsEnum.Grid, (int)ThumbnailsEnum.Catalog);

        //        var findExisting = userRepository.GetUserCv(userId);
        //        if (findExisting != null)
        //        {
        //            userRepository.DeleteUserCv(findExisting);
        //        }
        //        var userCv = new Cv
        //        {
        //            FileName = fileName,
        //            Path = uploadPath,
        //            Extension = Path.GetExtension(fileName)
        //        };
        //        userRepository.AddUserCv(userCv);

        //        var editUser = userRepository.GetByStringId(userId);
        //        if (editUser != null)
        //        {
        //            editUser.CvId = userCv.Id;
        //            userRepository.Update(editUser);
        //            userRepository.SaveChanges();
        //        }
        //    }
        //}




        [HttpGet]
        public IActionResult GetUsersJson()
        {
            var users = userRepository.GetAllWithRoles();
            users.ForEach(x => x.PasswordHash = userRepository.GetProfilePicturePath(x.Id, (int)ThumbnailsEnum.Grid));
          //  users.ForEach(x => x.PasswordHash = userRepository.GetCVPath(x.Id, (int)ThumbnailsEnum.Grid));
            var result = users.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                surname = x.Surname,
                email = x.Email,
                picture = x.PasswordHash,
                cv = x.PasswordHash,
                Address = x.Address,
                Experience = x.Experience,
                Education = x.Education,
                Gender = x.Gender,
                Skills = x.Skills,
                Projects = x.Projects,
                Birthday = x.Birthday,
                Facebook = x.Facebook,
                Twitter = x.Twitter,
                Linkedin = x.Linkedin,
                GitHub = x.Linkedin,
            });

            return new JsonResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
