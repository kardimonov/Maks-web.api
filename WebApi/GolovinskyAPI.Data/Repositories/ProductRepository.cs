using Dapper;
using GolovinskyAPI.Data.Interfaces;
using GolovinskyAPI.Data.Models.Products;
using GolovinskyAPI.Data.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GolovinskyAPI.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        // добавление нового товара или частн. объявления
        public NewProductOutputModel InsertProduct(NewProductInputModel model)
        {
            //char res;
            NewProductOutputModel res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                res = db.Query<NewProductOutputModel>("sp_SearchCreateAvito",
                    new {
                        Catalog = model.Catalog,
                        Id = model.Id,
                        Ctlg_Name = model.Ctlg_Name,
                        TArticle  = model.TArticle,
                        TName = model.TName,
                        TDescription = model.TDescription,
                        TCost = model.TCost,
                        TImageprev = model.TImageprev,
                        Appcode = model.Appcode,
                        TypeProd = model.TypeProd,
                        PrcNt = model.PrcNt,
                        TransformMech = model.TransformMech,
                        video = model.video,
                        audio = model.audio,
                        CID = model.CID},
                       commandType: CommandType.StoredProcedure).First();
            }

            //var path = Path.Combine(res.MediaLink);
            //if (!Directory.Exists(path))
            //{
            //    DirectoryInfo dir = Directory.CreateDirectory(path);
            //}
            
            //string link = Path.Combine(path, model.audio.FileName);
            //var stream = new FileStream(link, FileMode.Create);
            //stream.Position = 0;
            //await model.audio.CopyToAsync(stream);
            //await stream.FlushAsync();
            return res;
        }

        // редактирование товара или частн. объявления.
        public bool UpdateProduct(NewProductInputModel model)
        {
            char res;
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                var resObj = db.Query<NewProductOutputModel>("sp_SearchUpdateAvito", 
                    new {
                        Catalog = model.Catalog,
                        Id = model.Id,
                        Ctlg_Name = model.Ctlg_Name,
                        TArticle = model.TArticle, 
                        TName = model.TName, 
                        TDescription = model.TDescription,
                        TCost = model.TCost, 
                        TImageprev = model.TImageprev, 
                        Appcode = model.Appcode,
                        TypeProd = model.TypeProd,
                        PrcNt = model.PrcNt,
                        TransformMech = model.TransformMech, 
                        audio = model.audio,
                        video = model.video, 
                        CID = model.CID
                    },
                    commandType: CommandType.StoredProcedure).First();
                res = resObj.Result;
            }
           return res == '1';
        }

        // удаление товара
        public bool DeleteProduct(DeleteProductInputModel model)
        {
            char res;
            try
            {
                using (IDbConnection db = new SqlConnection(Global.Connection))
                {
                    var resObj = db.Query<NewProductOutputModel>("sp_SearchDeleteAvito", model,
                                 commandType: CommandType.StoredProcedure).First();
                    res = resObj.Result;
                }
                return res == '1';
            }
            catch (Exception)
            {
                return false;
            }
        }

        //поиск товара 
        public List<SearchPictureOutputModel> SearchProduct(SearchPictureInputModel input)
        {
            List<SearchPictureOutputModel> response = new();
            using (IDbConnection db = new SqlConnection(Global.Connection))
            {
                response = db.Query<SearchPictureOutputModel>("sp_SearchPicture", 
                    new { 
                        SearchDescr = input.SearchDescr, 
                        Cust_ID = input.Cust_ID 
                    },
                    commandType: CommandType.StoredProcedure).ToList();
            }
            return response;
        }
    }
}