using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BPJ_U2T.Models
{
    public class Cart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public int AccountID { get; set; }
        public int ProductID { get; set; }
        public int AmountProduct { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
