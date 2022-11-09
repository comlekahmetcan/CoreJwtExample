using CoreJwtExample.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CoreJwtExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IConfiguration _config;
        IStudentRepository _oStudentRepository = null;

        public StudentController(IConfiguration config, IStudentRepository oStudentRepository)
        {
            _config = config;
            _oStudentRepository = oStudentRepository;
        }

        [HttpGet]
        [Route("Gets1")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Gets1()
        {
            var list = await _oStudentRepository.Gets1();
            return Ok(list);
        }
        [HttpGet]
        [Route("Gets2")]
        public async Task<IActionResult> Gets2()
        {
            var list = await _oStudentRepository.Gets2();
            return Ok(list);
        }
    }
}
