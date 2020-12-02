using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models;
using GolovinskyAPI.Logic.Interfaces;
using GolovinskyAPI.Logic.Models.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GolovinskyAPI.Logic.Services
{
    public class GalleryService : IGalleryService
    {
        private readonly IGalleryRepository repo;

        public GalleryService(IGalleryRepository repository)
        {
            repo = repository;
        }

        public List<SearchPictureOutputModel> SearchPicture(SearchPictureInputModel model)
        {
            var images = repo.SearchPicture(model);
            Each(images, i => ConvertCategoriesToArr(i));
            return images;
        }

        public GalleryViewModel SearchPicture(SearchPictureInputModel model, int itemsPerPage, int currentPage, int sort)
        {
            var images = repo.SearchPicture(model);
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

        public GalleryViewModel SearchAllPictures(SearchAllPictureInputModel model, int itemsPerPage, int currentPage)
        {
            var images = repo.SearchAllPictures(model);
            Each(images, i => ConvertCategoriesToArr(i));

            var totalItems = images.Count();
            images = images
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .OrderBy(i => i.CreatedAt)
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