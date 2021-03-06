﻿using FreeShareAPI.Controllers.Base;
using FreeShareAPI.Converter;
using FreeShareAPI.Interface;
using FreeShareAPI.Models;
using FreeShareAPI.Models.Dbmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreeShareAPI.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductDataManager : BaseDataManager<ProductConverter>,IDataManager<ProductModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void Add(ProductModel model)
        {
            try
            {
                if (model != null)
                {
                    using (AngularEntities obj = new AngularEntities())
                    {
                        Product product = new Product();
                        product=converter.ConvertToEntity(model);
                        obj.Products.Add(product);
                        obj.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ProductModel> GetAll()
        {
            try
            {
                using (AngularEntities obj = new AngularEntities())
                {
                    List<Product> product = new List<Product>();
                    List<ProductModel> productModel = new List<ProductModel>();

                    product = obj.Products.ToList();
                    foreach(Product entity in product)
                    {
                        productModel.Add(converter.ConvertToModel(entity));
                    }
                    return productModel;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(int id)
        {
            try
            {
                bool result = false;
                if (id != 0)
                {
                    using (AngularEntities obj = new AngularEntities())
                    {
                        Product product = obj.Products.FirstOrDefault(x => x.ProductId == id);
                        if (product != null)
                        {
                            obj.Products.Remove(product);
                            obj.SaveChanges();
                            result = true;
                        }

                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public bool Edit(ProductModel model)
        {
            try
            {
                bool result = false;
                if (model!=null)
                {
                    using (AngularEntities obj = new AngularEntities())
                    {
                        Product product = obj.Products.FirstOrDefault(x => x.ProductId == model.ProductId);
                        if (product.ProductId>0)
                        {
                            product.ProductName = model.ProductName;
                            product.Deleted = model.Deleted;
                            obj.SaveChanges();
                            result = true;
                        }
                    }
                }
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductModel GetByID(int id)
        {
            try
            {
                ProductModel model = new ProductModel();
                if (id != 0)
                {
                    using (AngularEntities obj = new AngularEntities())
                    {
                        Product product = new Product();
                        product = obj.Products.FirstOrDefault(x => x.ProductId == id);
                        model = converter.ConvertToModel(product);
                        return model;
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}