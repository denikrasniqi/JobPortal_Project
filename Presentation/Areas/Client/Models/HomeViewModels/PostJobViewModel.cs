using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Areas.Client.Models.HomeViewModels
{
    public class PostJobViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string JobTypeName { get; set; } = null!;
    }
}
