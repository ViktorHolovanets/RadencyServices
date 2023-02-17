using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadencyWebApplication.Models.Db
{
    public class Rating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Range(1, 5)]
        public int Score { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
    }
}
