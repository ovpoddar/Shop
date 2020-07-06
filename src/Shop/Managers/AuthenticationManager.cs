using AutoMapper;
using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shop.Handlers.Interfaces;
using Shop.Managers.Interfaces;
using Shop.Models;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Managers
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<Employer> _userManager;
        private readonly SignInManager<Employer> _signInManager;
        private readonly IEmployerHandler _employerHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMapper _mapper;

        public AuthenticationManager(UserManager<Employer> userManager, SignInManager<Employer> signInManager, IMapper mapper, IEmployerHandler employerHandler, ITokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(_userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(_signInManager));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
            _employerHandler = employerHandler ?? throw new ArgumentNullException(nameof(_employerHandler));
            _tokenHandler = tokenHandler ?? throw new ArgumentNullException(nameof(_tokenHandler));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(_httpContextAccessor));
        }

        public async Task<CustomeSignInResult> LogInUserAsync(Employer user, string password)
        {
            if (_employerHandler.IsAccessable(user))
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, true, false);

                if (result.Succeeded)
                {
                    var checkIn = await _employerHandler.LastcheckInAsync(user);
                    return new CustomeSignInResult()
                    {
                        IsLockedOut = result.IsLockedOut,
                        Succeeded = result.Succeeded,
                        IsNotAllowed = result.IsNotAllowed,
                        RequiresTwoFactor = result.RequiresTwoFactor,
                        Errors = checkIn.Errors.ToList(),
                        Token = _tokenHandler.GenerateToken(user)
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
                await _signInManager.SignInAsync(employer, isPersistent: true);
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
