using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class User
    {
        [Key]
        [Index(IsUnique = true)]
        [MaxLength(30)]
        public string UserName { get; set; }
        [MaxLength(30)]
        [Index(IsUnique = true)]
        public string DisplayName { get; set; }
        public string Password { get; set; }

        public virtual List<Blog> Blogs { get; set; }
        public virtual List<BlogComment> BlogComments { get; set; }
        public virtual List<PostComment> PostComments { get; set; }
        public virtual List<BlogMark> BlogMarks { get; set; }
        public virtual List<PostMark> PostMarks { get; set; }

        public double GetMark()
        {
            if (!Blogs.Any()) return 3.0;
            else return Blogs.Average(o => o.GetMark());
        }

        public void Show()
        {
            Console.WriteLine("Display Name: {0}, Current Mark: {1}", DisplayName, GetMark());
            Console.WriteLine("BLOGS:");
            foreach (var blog in Blogs)
            {
                blog.ShowSelf();
            }
            Console.WriteLine("---------------------------------------------------------------------");
        }
    }
}
