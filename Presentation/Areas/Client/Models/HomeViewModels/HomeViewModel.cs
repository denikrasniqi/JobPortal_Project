namespace Presentation.Areas.Client.Models.HomeViewModels
{
    public class HomeViewModel
    {
        public List<PostJobViewModel> PostJobs { get; set; } = null!;
        public List<JobTypeViewModel> JobTypes { get; set; } = null!;
    }
}
