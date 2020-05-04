﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class SentRequestService : ISentRequestService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SentRequestService(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(_httpClientFactory));

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request) =>
            await _httpClientFactory.CreateClient().SendAsync(request);
    }
}
