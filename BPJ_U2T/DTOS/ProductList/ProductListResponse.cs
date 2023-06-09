﻿using BPJ_U2T.Settings;

namespace BPJ_U2T.DTOS.ProductList
{
    public class ProductListResponse
    {
        public string ID { get; set; }
        public string OrderID { get; set; }
        public int ProductID { get; set; }
        public int ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public string  ImageProduct { get; set; }
        public Models.Product Product { get; set; }
        public Models.OrderAggregate.OrderAccount OrderAccount { get; set; }
        static public ProductListResponse FromProductList (Models.OrderAggregate.OrderItem productList)
        {
            // return ตัวมันเอง
            return new ProductListResponse
            {
                ID = productList.ID,
               OrderID = productList.OrderAccountID,
               ProductID = productList.ProductID,
               ProductPrice = productList.ProductPrice,
               ProductAmount = productList.ProductAmount,
               Product = productList.Product,
                OrderAccount = productList.OrderAccount,
                ImageProduct = !string.IsNullOrEmpty(productList.Product.Image) ? UrlServer.Url + "images/" + productList.Product.Image : "",
                
            };
        }
    }
}
