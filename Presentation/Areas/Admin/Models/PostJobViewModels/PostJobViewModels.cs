using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Areas.Admin.Models.PostJobViewModels
{
    public class PostJobViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int JobTypeId { get; set; }
        public SelectList? JobType { get; set; }
    }
}
