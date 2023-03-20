using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Job_Portal.App.Constants;
using Job_Portal.App.Interfaces;
using Job_Portal.Data.Context;
using Job_Portal.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presentation.Areas.Admin.Controllers;
using Presentation.Areas.Client.Models;
using Presentation.Areas.Client.Models.HomeViewModels;
using Presentation.FileHelper;
using Presentation.Areas.Client.Models.MailModel;
using Job_Portal.Data.Entities;

namespace Presentation.Areas.Client.Controllers
{
    [Area(AreasConstants.Client)]
    [Authorize(Policy = "Client")]

    public class HomeController : Controller
    {
        private readonly Job_PortalContext _context;
        private readonly IUserRepository userRepository;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRolesRepository rolesRepository;
        private readonly ILogger _logger;
        private readonly IFileHelper _fileHelper;
        private readonly IPostJobRepository postJobRepository;
        private readonly IJobTypeRepository jobTypeRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public HomeController(IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IRolesRepository rolesRepository, ILogger<UsersController> logger, IFileHelper fileHelper, IPostJobRepository postJobRepository, IJobTypeRepository jobTypeRepository, Job_PortalContext context, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _context = context;
            this.userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.rolesRepository = rolesRepository;
            _logger = logger;
            _fileHelper = fileHelper;
            this.postJobRepository = postJobRepository;
            this.jobTypeRepository = jobTypeRepository;
            _httpContextAccessor = httpContextAccessor;
            this.userService = userService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
           
            var model = new HomeViewModel();
            var list = postJobRepository.GetAllJobTypes();
            var ll = jobTypeRepository.GetAll();
            model.JobTypes = ll.Select(x => new JobTypeViewModel() { Id = x.Id, Type = x.Type }).ToList();
            model.PostJobs = list.Select(x => new PostJobViewModel() { Address = x.Address, Description = x.Description, Email = x.Email, Id = x.Id, JobTypeName = x.JobType.Type, Name = x.Name }).ToList();
            return View(model);           
        }
    

        [AllowAnonymous]
        public IActionResult Aboutus()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Apply(int id)
        {
            //+var userAccessor = _httpContextAccessor.HttpContext.User;
            var job = await _context.PostJobs.Include(x=> x.JobType).FirstOrDefaultAsync(x => x.Id == id);
            if (job == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var model = new PostJobViewModel()
            {
                Name = job.Name,
                Email = job.Email,
                Address = job.Address,
                Description = job.Description,
                Id = job.Id,
                JobTypeName = job.JobType.Type
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyJob(int id)
        {
            var applyEntry = new Apply()
            {
                PostJobId = id,
                UserId = userService.GetUserId()
            };

            _context.Applies.Add(applyEntry);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Search(string searchString)
        {
            try
            {
                return RedirectToAction("Index", new { Search = searchString });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AllowAnonymous]
        public ViewResult SendEmail(MailModel _objModelMail)
        {
            if (ModelState.IsValid)
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(_objModelMail.To);
                mail.From = new MailAddress(_objModelMail.From);
                mail.Subject = _objModelMail.Subject;
                string Body = _objModelMail.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.outlook.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("DiellzaMedina@outlook.com", "MedinaDiellza123.."); // Enter seders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return View("SendEmail", _objModelMail);
            }
            else
            {
                return View();
            }
        }
       

    }
}

