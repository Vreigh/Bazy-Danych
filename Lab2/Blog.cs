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
    public class Blog
    {
        public int BlogId { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Url { get; set; }

        public string UserName { get; set; }
        public virtual User User { get; set; }

        public virtual List<Post> Posts { get; set; }
        public virtual List<BlogComment> Comments { get; set; }
        public virtual List<BlogMark> Marks { get; set; }

        private double PostsAvarage()
        {
            if (!Posts.Any()) return 3.0;
            else return Posts.Average(o => o.GetMark());
        }

        public double GetMark()
        {
            double directMark;
            if (!Marks.Any()) directMark = 3.0;
            else directMark = Marks.Sum(o => o.Weight * o.Value) / Marks.Sum(o => o.Weight);
            return 0.4 * directMark + 0.6 * PostsAvarage();
        }

        public void ShowSelf()
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("Blog's Name: {0}", Name);
            Console.WriteLine("Blog's Url: {0}", Url);
            Console.WriteLine("Blog's number of Posts: {0}", Posts.Count());
            Console.WriteLine("Blog's number of Marks: {0}", Marks.Count());
            Console.WriteLine("Blog's number of Comments: {0}", Comments.Count());
            Console.WriteLine("Blog's current Mark: {0}", GetMark());
        }

        public void Show()
        {
            Console.WriteLine("Name: {0}, Current Mark: {1}", Name, GetMark());
            Console.WriteLine("POSTS");
            foreach (var post in Posts)
            {
                post.ShowSelf();
            }
            Console.WriteLine("COMMENTS");
            foreach (var comm in Comments)
            {
                comm.ShowSelf();
            }
            Console.WriteLine("MARKS");
            foreach(var mark in Marks)
            {
                mark.ShowSelf();
            }
            Console.WriteLine("---------------------------------------------------------------------");
        }

        public static void Add(User user)
        {
            string name, url;
            using (var db = new BlogContext())
            {
                while (true)
                {
                    Console.Write("Enter Name: ");
                    name = Console.ReadLine();

                    if (db.Blogs.Any(o => o.Name == name))
                    {
                        Console.WriteLine("Name already taken!");
                    }
                    else break;
                }

                Console.Write("Enter Url: ");
                url = Console.ReadLine();

                db.Blogs.Add(new Blog { Name = name, UserName = user.UserName, Url = url });
                db.SaveChanges();
                Console.WriteLine("Adding succesful.");
            }
            
        }
    }
}
