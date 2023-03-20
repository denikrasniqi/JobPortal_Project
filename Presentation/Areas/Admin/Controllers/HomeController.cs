using Job_Portal.App.Constants;
using Job_Portal.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Presentation.Areas.Admin.Models.DashboardViewModels;
using Presentation.Areas.Client.Models.HomeViewModels;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private IOptions<RequestLocalizationOptions> _options;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IPostJobRepository postJobRepository;
        private readonly IJobTypeRepository jobTypeRepository;
        private readonly IUserRepository userRepository;

        public HomeController(IHttpContextAccessor httpContextAccessor, IOptions<RequestLocalizationOptions> options, IPostJobRepository _postJobRepository, IJobTypeRepository _jobTypeRepository, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _options = options;
            postJobRepository = _postJobRepository;
            jobTypeRepository = _jobTypeRepository;
            this.userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel();
            model.NrJobs = postJobRepository.Count();
            model.NrUsers = userRepository.Count();
            model.NrTypes = jobTypeRepository.Count();
            return View(model);
        }

        public IActionResult SetLanguage(string culture)
        {
            try
            {
                var cultureItems = _options.Value.SupportedUICultures!.Select(x => x.Name).ToArray();
                if (cultureItems.Any(x => x.Equals(culture)))
                {
                    Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                       new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1), HttpOnly = true, Secure = _httpContextAccessor.HttpContext!.Request.IsHttps });
                }
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
