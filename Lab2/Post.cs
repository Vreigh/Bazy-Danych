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
    public class Post
    {
        public int PostId { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(30)]
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public virtual List<PostComment> Comments { get; set; }
        public virtual List<PostMark> Marks { get; set; }

        public double GetMark()
        {
            if (!Marks.Any()) return 3.0;
            else return Marks.Sum(o => o.Weight * o.Value) / Marks.Sum(o => o.Weight);
        }

        public void ShowSelf()
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("Post's Title: {0}", Title);
            Console.WriteLine("Posts's number of Comments: {0}", Comments.Count());
            Console.WriteLine("Posts's number of Marks: {0}", Marks.Count());
            Console.WriteLine("Posts's current Mark: {0}", GetMark());
        }

        public void Show()
        {
            Console.WriteLine("Title: {0}, Current Mark: {1}", Title, GetMark());
            Console.WriteLine("COMMENTS");
            foreach (var comm in Comments)
            {
                comm.ShowSelf();
            }
            Console.WriteLine("MARKS");
            foreach (var mark in Marks)
            {
                mark.ShowSelf();
            }
            Console.WriteLine("---------------------------------------------------------------------");
        }

        public static void Add(User user)
        {
            using (var db = new BlogContext())
            {
                string title, content;
                int blogId;
                while (true)
                {
                    Console.Write("Enter the name of the blog you want to add post to: ");
                    string name = Console.ReadLine();

                    if(!db.Blogs.Any(o => o.Name == name))
                    {
                        Console.WriteLine("Blog not found.");
                        continue;
                    }

                    Blog blog = db.Blogs.Where(o => o.Name == name).First();

                    if (blog.UserName != user.UserName)
                    {
                        Console.WriteLine("This blog does not belong to you.");
                    }
                    else
                    {
                        blogId = blog.BlogId;
                        break;
                    }
                }

                while (true)
                {
                    Console.Write("Enter the title of the new post: ");
                    title = Console.ReadLine();
                    if (db.Posts.Any(o => o.Title == title))
                    {
                        Console.WriteLine("Title already taken!");
                    }
                    else break;
                }

                

                Console.Write("Enter the content of the new post: ");
                content = Console.ReadLine();

                db.Posts.Add(new Post { Title = title, Content = content, BlogId = blogId });
                db.SaveChanges();

                Console.WriteLine("Adding succesful.");
            }
        }
    }
}
