using Job_Portal.App.Constants;
using Job_Portal.App.Interfaces;
using Job_Portal.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Areas.Admin.Models.JobTypeViewModels;

namespace Presentation.Areas.Admin.Controllers
{
    [Area(AreasConstants.Admin)]
    public class JobTypeController : Controller
    {
        private readonly IJobTypeRepository jobTypeRepository;
        private readonly ILogger _logger;

        public JobTypeController(IJobTypeRepository jobTypeRepository, ILogger<JobTypeController> logger)
        {
            this.jobTypeRepository = jobTypeRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new JobTypeViewModels();
            return View(model);
        }

        // GET: Product/Edit/5  
        [HttpGet]
        public IActionResult Edit(int id)
        {
            JobType? job = jobTypeRepository.GetById(id);
            if (job != null)
            {
                var model = new JobTypeViewModels()
                {
                    Id = id,
                    Type = job.Type,  
                };
                return View("Add", model);
            }

            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult Add(JobTypeViewModels model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    var job = new JobType { Type = model.Type };
                    jobTypeRepository.Add(job);
                    jobTypeRepository.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    var postjob = jobTypeRepository.GetById(model.Id);
                    if (postjob != null)
                    {
                        postjob.Type = model.Type;



                        jobTypeRepository.Update(postjob);
                        jobTypeRepository.SaveChanges();
                        return RedirectToAction("Index");

                    }
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetJobstypeJson()
        {
            var jobs = jobTypeRepository.GetAll();

            var result = jobs.Select(x => new
            {
                id = x.Id,
                type = x.Type,
               
            });

            return new JsonResult(result);
        }
        // POST: Product/Delete/5  

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var job = jobTypeRepository.GetById(id);
                if (job != null)
                {
                    jobTypeRepository.Remove(job);
                    jobTypeRepository.SaveChanges();
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
