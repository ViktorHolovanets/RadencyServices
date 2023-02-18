using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RadencyWebApplication.Models.Db
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Cover { get; set; }
        [Required]
        public string Content { get; set; }
        public string? Author { get; set; }
        [Required]
        public string Genre { get; set; }

    }
}
