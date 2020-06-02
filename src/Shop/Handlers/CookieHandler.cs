using Microsoft.AspNetCore.Http;
using Shop.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Handlers
{
    public class CookieHandler : ICookieHandler
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CookieHandler(IHttpContextAccessor contextAccessor) =>
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(_contextAccessor));

        public void Create(string name, string value) =>
            _contextAccessor.HttpContext.Response.Cookies.Append(name, value);

        public void Create(string name, string value, CookieOptions options) =>
            _contextAccessor.HttpContext.Response.Cookies.Append(name, value, options);

        public void Delete(string name) =>
            _contextAccessor.HttpContext.Response.Cookies.Delete(name);

        public string Get(string name) =>
            _contextAccessor.HttpContext.Request.Cookies[name];
    }
}
