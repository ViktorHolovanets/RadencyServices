using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadencyWebApplication.Models.Db
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Reviewer { get; set; }
        public int BookId { get; set; }
        public Book?  Book { get; set; }
        
    }
}
