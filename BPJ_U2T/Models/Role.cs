using System.ComponentModel.DataAnnotations;

namespace BPJ_U2T.Models
{
    public class Role
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
