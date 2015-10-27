using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace AU_Management.Models
{
    public class ManagementDB
    {
    }

    public class UserMain
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name="帳號")]
        public String UserID { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name = "密碼")]
        public String PassWord { get; set; }
    }

    public class PaperMain
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "論文名稱")]
        public String Title { get; set; }

        [Required]
        [Display(Name = "論文出處")]
        public String Source { get; set; }

        [Required]
        [Display(Name = "發表年分")]
        public int Year { get; set; }

        [Display(Name = "關鍵字")]
        public String Key { get; set; }

        [Display(Name = "描述")]
        public String Discription { get; set; }
        [Required]
        public String Creater { get; set; }

        public String PDF { get; set; }

        public String PPT { get; set; }
    }

    public class ManagementDBContext : DbContext
    {
        public DbSet<UserMain> Users { get; set; }

        public DbSet<PaperMain> Papers { get; set; }
    }

}