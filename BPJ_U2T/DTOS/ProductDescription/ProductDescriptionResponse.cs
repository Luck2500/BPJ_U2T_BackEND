﻿using BPJ_U2T.Settings;

namespace BPJ_U2T.DTOS.ProductDescription
{
    public class ProductDescriptionResponse
    {
        public string ID { get; set; }
        public string? Image { get; set; }
        public int? ProductID { get; set; }


        static public ProductDescriptionResponse FromDescription(Models.ProductDescription productDescription)
        {
            // return ตัวมันเอง
            return new ProductDescriptionResponse
            {
                ID = productDescription.ID,
                ProductID = productDescription.ProductID,
                //Image = productDescription.Image,
                Image = !string.IsNullOrEmpty(productDescription.Image) ? UrlServer.Url + "images/" + productDescription.Image : "",

            };
        }
    }
}
