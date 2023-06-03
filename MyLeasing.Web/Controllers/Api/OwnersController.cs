using Microsoft.AspNetCore.Mvc;
using MyLeasing.Web.Data;

namespace MyLeasing.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : Controller
    {
        private readonly DataContext _context;
        private readonly IOwnerRepository _ownerRepository;

        public OwnersController(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        [HttpGet]
        public IActionResult GetOwner()
        {
            return Ok(_ownerRepository.GetAllWithUsers());
        }
    }
}
