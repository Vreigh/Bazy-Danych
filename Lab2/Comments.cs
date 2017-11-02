using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public abstract class Comment
    {
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime AddDate { get; set; }

        public virtual User User { get; set; }

        public void ShowSelf()
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("Comment from: {0}", User.DisplayName);
            Console.WriteLine("Comment added: {0}", AddDate);
            Console.WriteLine(Content);
        }

        public static void Add(User user)
        {
            using (var db = new BlogContext())
            {
                while (true)
                {
                    Console.Write("Enter the name or title of the entity you want to add a comment to: ");
                    string name = Console.ReadLine();

                    Console.Write("Is this a Post, or a Blog? p/b");
                    var c = Console.ReadKey();
                    Console.WriteLine();

                    if (c.KeyChar == 'p')
                    {
                        if(!db.Posts.Any(o => o.Title == name))
                        {
                            Console.WriteLine("There is no such post.");
                            continue;
                        }
                        Post post = db.Posts.Where(o => o.Title == name).First();

                        Console.Write("Enter the content:");
                        string content = Console.ReadLine();
                        db.PostComments.Add(new PostComment { Content = content, UserName = user.UserName, AddDate = DateTime.UtcNow, PostId = post.PostId });
                        db.SaveChanges();
                        Console.WriteLine("Adding succesful.");
                        break;
                        
                    }
                    else if (c.KeyChar == 'b')
                    {
                        if(!db.Blogs.Any(o => o.Name == name))
                        {
                            Console.WriteLine("There is no such blog.");
                            continue;
                        }
                        Blog blog = db.Blogs.Where(o => o.Name == name).First();

                        Console.Write("Enter the content:");
                        string content = Console.ReadLine();
                        db.BlogComments.Add(new BlogComment { Content = content, UserName = user.UserName, AddDate = DateTime.UtcNow, BlogId = blog.BlogId });
                        db.SaveChanges();
                        Console.WriteLine("Adding succesful.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Unknown command.");
                    }
                }
            }
        }
    }

    public class BlogComment : Comment
    {
        public int BlogCommentId { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class PostComment : Comment
    {
        public int PostCommentId { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}