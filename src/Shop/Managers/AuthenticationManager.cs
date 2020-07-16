using AutoMapper;
using DataAccess.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using Shop.ViewModels;
using Shop.Wrappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Shop.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserManagerWrapper _userManager;
        private readonly ISignInManagerWrapper _signInManager;
        private readonly IEmployerHandler _employerHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly IConfiguration _configuration;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMapper _mapper;

        public AuthenticationManager(IUserManagerWrapper userManager, ISignInManagerWrapper signInManager, IMapper mapper, IEmployerHandler employerHandler, ITokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor, IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(_signInManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
            _employerHandler = employerHandler ?? throw new ArgumentNullException(nameof(_employerHandler));
            _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(_tokenHandler));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(_httpContextAccessor));
            _dataProtectionProvider = dataProtectionProvider ?? throw new ArgumentNullException(nameof(_dataProtectionProvider));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(_configuration));
        }

        public async Task<CustomeSignInResult> LogInUserAsync(Employer user, string password)
        {
            if (_employerHandler.IsAccessable(user))
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, true, false);

                if (result.Succeeded)
                {
                    var token = _tokenHandler.GenerateToken(user);
                    _httpContextAccessor.HttpContext.Response.Cookies.Append(".AspUser",
                                                                             _dataProtectionProvider.CreateProtector(_configuration["dataprotector"]).Protect(token),
                                                                              new CookieOptions()
                                                                              {
                                                                                  Expires = DateTime.Now.AddDays(.6)
                                                                              });
                    return new CustomeSignInResult()
                    {
                        IsLockedOut = result.IsLockedOut,
                        Succeeded = result.Succeeded,
                        IsNotAllowed = result.IsNotAllowed,
                        RequiresTwoFactor = result.RequiresTwoFactor,
                        Errors = (await _employerHandler.LastcheckInAsync(user)).Errors.ToList(),
                        Token = token
                    };
                }
            }
            return new CustomeSignInResult()
            {
                Errors = new List<IdentityError>()
                {
                    new IdentityError()
                    {
                        Code= "400",
                        Description = "Contact to the shopmanger"
                    },
                    new IdentityError()
                    {
                        Code= "304",
                        Description = "Contact to the inernet"
                    }
                },
                IsLockedOut = false,
                RequiresTwoFactor = false,
                IsNotAllowed = false,
                Succeeded = false,
                Token = null
            };
        }

        public async Task<Results<CustomeSignInResult>> LogInUserResultAsync(LogInViewModel logInViewModel) =>
            _employerHandler.GetEmployer(logInViewModel.UserName) == null ?
            new Results<CustomeSignInResult>()
            {
                Exception = "Provide a Valid Credentials",
                HttpStatusCode = HttpStatusCode.OK,
                Result = null,
                Success = false
            } :
            new Results<CustomeSignInResult>
            {
                Success = true,
                Result = await LogInUserAsync(_employerHandler.GetEmployer(logInViewModel.UserName), logInViewModel.Password),
                Exception = null,
                HttpStatusCode = HttpStatusCode.OK
            };

        public async Task SignOutUserAsync()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(".AspUser", new CookieOptions()
            {
                Expires = DateTime.Now
            });
            await _signInManager.SignOutAsync();
        }

        public async Task<CustomeIdentityResult> SignUpUserAsync(SignInViewModel model)
        {
            var employer = _mapper.Map<Employer>(model);
            var createResult = await _userManager.CreateAsync(employer, model.ConfirmPassword);
            var token = "";
            if (createResult.Succeeded)
            {
                await _signInManager.SignInAsync(employer, true);
                token = _tokenHandler.GenerateToken(employer);
            }
            return new CustomeIdentityResult()
            {
                Errors = createResult.Errors.ToList(),
                Success = createResult.Succeeded,
                Token = token
            };
        }
    }
}
