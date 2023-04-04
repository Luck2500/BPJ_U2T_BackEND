using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BPJ_U2T.Models
{
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Text { get; set; }
        public int AccountID { get; set; }
        public int? ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
    }
}
