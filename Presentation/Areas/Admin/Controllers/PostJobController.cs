using Job_Portal.App.Constants;
using Job_Portal.App.Interfaces;
using Job_Portal.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Presentation.Areas.Admin.Models.PostJobViewModels;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    public class PostJobController : Controller
    {

        private readonly IPostJobRepository postjobrepository;
        private readonly IJobTypeRepository jobTypeRepository;
        private readonly ISelectListService selectListService;
        private readonly ILogger _logger;

        public PostJobController(IPostJobRepository postjobrepository, IJobTypeRepository jobTypeRepository, ILogger<PostJobController> logger, ISelectListService selectListService)
        {
            this.jobTypeRepository = jobTypeRepository;
            this.postjobrepository = postjobrepository;
            _logger = logger;
            this.selectListService = selectListService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new PostJobViewModels();
           model.JobType = new SelectList(selectListService.GetTypesKeysValues(), "Key", "Value", model.JobTypeId);
            return View(model);
        }

        // GET: Product/Edit/5  
        [HttpGet]
        public IActionResult Edit(int id)
        {
            PostJob? job = postjobrepository.GetById(id);
            if (job != null)
            {
                var model = new PostJobViewModels()
                {
                    Id = id,
                    Description = job.Description,
                    Email = job.Email!,
                    Name = job.Name!,
                    Address = job.Address,
                };
                model.JobType = new SelectList(selectListService.GetTypesKeysValues(), "Key", "Value", model.JobTypeId);
                return View("Add", model);
            }

            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult Add(PostJobViewModels model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {                   
                    var job = new PostJob { Name = model.Name, Email = model.Email, Description = model.Description, Address = model.Address, JobTypeId = model.JobTypeId };
                    postjobrepository.Add(job);
                    postjobrepository.SaveChanges();
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    var postjob = postjobrepository.GetById(model.Id);
                 
                    if (postjob != null)
                    {
                        postjob.Name = model.Name;
                        postjob.Description = model.Description;
                        postjob.Email = model.Email;
                        postjob.Address = model.Address;
                        postjob.JobTypeId = model.JobTypeId;

                        postjobrepository.Update(postjob);
                        postjobrepository.SaveChanges();
                       
                        return RedirectToAction("Index");

                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetJobsJson()
        {           
            var jobs = postjobrepository.GetAllJobTypes();

            var result = jobs.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                email = x.Email,
                description = x.Description,
                address = x.Address,
                jobtype = x.JobType.Type
            }) ;

            return new JsonResult(result);
        }
        // POST: Product/Delete/5  

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var job = postjobrepository.GetById(id);
                if (job != null)
                {
                     postjobrepository.Remove(job);
                    postjobrepository.SaveChanges();
                    
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

