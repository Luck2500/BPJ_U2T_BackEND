﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BPJ_U2T.DTOS.ProductDescription;
using BPJ_U2T.Models;
using BPJ_U2T.Settings;

namespace BPJ_U2T.DTOS.ProductDetailProcess
{
    public class ProductDetailProcessResponse
    {
        public string ID { get; set; }
        public string NameRawMaterial { get; set; }// วัตถุดิบ
        public string MakeProductsprocess { get; set; } //วิธีการทำ
        public string? VideoProduct { get; set; }
        public int? ProductID { get; set; }
        public Models.Product Product { get; set; }

        static public ProductDetailProcessResponse FromProductDetailProcess(Models.ProductDetailProcess productDetailProcess)
        {
            // return ตัวมันเอง
            return new ProductDetailProcessResponse
            {
                ID = productDetailProcess.ID,
                NameRawMaterial = productDetailProcess.NameRawMaterial,
                MakeProductsprocess= productDetailProcess.MakeProductsprocess,
                VideoProduct = !string.IsNullOrEmpty(productDetailProcess.VideoProduct) ? UrlServer.Url + "vedio/" + productDetailProcess.VideoProduct : "",
                ProductID = productDetailProcess.ProductID,
                Product = productDetailProcess.Product,
            };
        }
    }
}
