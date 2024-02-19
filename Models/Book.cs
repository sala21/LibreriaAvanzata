using WebAppLibreria.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAppLibreria.Models
{
    public class BookItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public int IdGen { get; set; }
        public int IdShelf { get; set; }
        public bool IsOut { get; set; }
    }
}


