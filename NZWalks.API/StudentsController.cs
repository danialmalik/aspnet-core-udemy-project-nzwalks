using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp_core_web_api_udemy
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents() {
            string[] studentNames = new string[] {"John", "Steve", "Bill"};

            return Ok(studentNames);
        }
    }
}
