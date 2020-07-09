using FMCW.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Worter.DTO.Login;
using Worter.BL;
using Worter.DAO.Models;
using Microsoft.Extensions.Configuration;
using Worter.Common.Constants;
using Worter.API.Shared;

namespace Worter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly StudentBL studentBl;

        protected readonly IConfiguration configuration;

        public LoginController(WorterContext context, IConfiguration configuration)
        {
            this.studentBl = new StudentBL(context, configuration);
            this.configuration = configuration;
        }

        [Route("Student")]
        [HttpPost]
        public StringResult LoginStudent([FromBody] StudentLoginRequest student)
        {
            if (string.IsNullOrEmpty(student.Username) || string.IsNullOrEmpty(student.Password))
            {
                return StringResult.Error("Username and password cannot be blank");
            }

            var loginResult = studentBl.LoginStudent(student.Username, student.Password);
            if (loginResult.Success)
            {
                var secretKey = configuration[CONSTANTS.Keys.JWT_SECRETKEY];
                var issuer = configuration[CONSTANTS.Keys.JWT_ISSUER];
                var audience = configuration[CONSTANTS.Keys.JWT_AUDIENCE];
                var token =JwtHandler.GenerateAPIToken(loginResult.ResultOk.ToString(), secretKey, issuer, audience);
                return StringResult.Ok(token);
            }
            else
            {
                var result = StringResult.Error();
                result.ResultError = loginResult.ResultError;
                return result;
            }
        }
    }
}