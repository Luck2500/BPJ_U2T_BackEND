using BPJ_U2T.DTOS.Review;
using BPJ_U2T.Models;

namespace BPJ_U2T.DTOS.District
{
    public class DistrictResponse
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TextDistrict { get; set; }
        public string? Image { get; set; }



        static public DistrictResponse FromDistrict(Models.District district)
        {
            return new DistrictResponse
            {
                ID = district.ID,
                Name = district.Name,
                TextDistrict= district.TextDistrict,
                Image = !string.IsNullOrEmpty(district.Image) ? "https://localhost:7141/" + "images/" + district.Image : "",

            };
        }
    }
}
