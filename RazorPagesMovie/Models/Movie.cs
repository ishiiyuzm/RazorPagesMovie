using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        [DisplayName("項目ID")]
        public int ID { get; set; }
        [DisplayName("タイトル")]
        public string Title { get; set; }

        [DisplayName("リリース日")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("ジャンル")]
        public string Genre { get; set; }

        [DisplayName("金額")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}