using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Common.Data;
using MyLeasing.Common.Data.Entities;

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
            return Ok(_ownerRepository.GetAll());
        }
    }
}
