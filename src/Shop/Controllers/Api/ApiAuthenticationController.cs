using Microsoft.AspNetCore.Mvc;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Controllers.Api
{
    [Route("api/Authentication")]
    [ApiController]
    public class ApiAuthenticationController : ControllerBase
    {
        private readonly IEmployerHandler _employerHandler;
        private readonly IAuthenticationManager _authenticationManager;
        public ApiAuthenticationController(IEmployerHandler employerHandler, IAuthenticationManager authenticationManager)
        {
            _employerHandler = employerHandler ?? throw new ArgumentNullException(nameof(_employerHandler));
            _authenticationManager = authenticationManager ?? throw new ArgumentNullException(nameof(_authenticationManager));
        }

        [HttpPost("Login")]
        public async Task<Results<CustomeSignInResult>> LoginAsync([FromBody] LogInViewModel logInView)
        {
            if (ModelState.IsValid)
            {
                var user = _employerHandler.GetEmployer(logInView.UserName);
                if (user != null)
                    return new Results<CustomeSignInResult>
                    {
                        HttpStatusCode = HttpStatusCode.BadRequest,
                        Result = await _authenticationManager.LogInUserAsync(user, logInView.Password),
                        Success = true,
                        Exception = "no data found"
                    };
                return new Results<CustomeSignInResult>
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    Result = null,
                    Success = false,
                    Exception = "no data found"
                };
            }
            return new Results<CustomeSignInResult>
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Result = null,
                Success = false,
                Exception = "provide some valid data"
            };
        }


    }
}
