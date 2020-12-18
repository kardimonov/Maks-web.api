using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Data.Models.Admin;
using GolovinskyAPI.Data.Models.Authorization;
using GolovinskyAPI.Logic.Infrastructure;
using GolovinskyAPI.Logic.Interfaces;
using GolovinskyAPI.Logic.Models;
using GolovinskyAPI.Logic.Models.Gallery;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace GolovinskyAPI.Logic.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository repo;
        private readonly IAuthHandler _authHandler;
        private readonly IOptions<AuthServiceModel> _options;

        public AdminService(IAdminRepository repository, IAuthHandler authHandler, IOptions<AuthServiceModel> options)
        {
            repo = repository;
            _authHandler = authHandler;
            _options = options;
        }

        public LoginAdminOutputModel CheckWebPasswordAdmin(LoginModel loginModel, string userName, string audience)
        {
            var admin = repo.CheckWebPasswordAdmin(loginModel);

            var now = DateTime.UtcNow;
            var identity = _authHandler.GetIdentity(userName, admin.Cust_ID, admin.Role);
            
            var jwt = new JwtSecurityToken(
             issuer: _options.Value.Issuer,
             audience: audience,
             notBefore: now,
             claims: identity.Claims,
             expires: now.AddMonths(1),
             signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(_options), SecurityAlgorithms.HmacSha256));

            var endcodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            admin.accessToken = endcodedJwt;

            return admin;
        }

        public GalleryViewModel SearchAllAdminPictures(AdminPictureInfo dto, int itemsPerPage, int currentPage)
        {
            var images = repo.SearchAllAdminPictures(dto);
            Each(images, i => ConvertCategoriesToArr(i));
            var totalItems = images.Count();

            images = images
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            var response = new GalleryViewModel
            {
                Images = images,
                TotalItems = totalItems
            };
            return response;
        }

        private void Each<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        private void ConvertCategoriesToArr(SearchPictureOutputModel item)
        {
            item.IdCategories = item.idcrumbs.Split(';').ToList();
            item.NameCategories = item.txtcrumbs.Split(';').ToList();
        }
    }
}