namespace BPJ_U2T.DTOS.District
{
    public class DistrictRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TextDistrict { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
    }
}
