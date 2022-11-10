using CoreJwtExample.IRepository;
using CoreJwtExample.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CoreJwtExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(Student oStudent)
        {
            try
            {
                oStudent = await _oStudentRepository.Save(oStudent);
                if (oStudent.Message == null) return Ok(oStudent);
                else return StatusCode((int)HttpStatusCode.InternalServerError, oStudent.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetByStudentId/{studentId}")]
        public async Task<IActionResult> GetByStudentId(int studentId)
        {
            if (studentId == 0) return Ok(new Student());
            var oStudent = await _oStudentRepository.Get(studentId);
            return Ok(oStudent);
        }

        [HttpDelete]
        [Route("Delete/{studentId}")]
        public async Task<IActionResult> Delete(int studentId)
        {
            try
            {
                Student oStudent = new Student() { StudentId = studentId };
                string message = await _oStudentRepository.Delete(oStudent);
                if (message == "Deleted") return Ok(message);
                else return StatusCode((int)HttpStatusCode.InternalServerError, message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
