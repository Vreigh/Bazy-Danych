using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public abstract class Mark
    {
        public double Weight { get; set; }
        public int Value { get; set; }
        public string UserName { get; set; }
        public DateTime AddDate { get; set; }

        public virtual User User { get; set; }

        public void ShowSelf()
        {
            Console.WriteLine("*************************************");
            Console.WriteLine("Mark from: {0}", User.DisplayName);
            Console.WriteLine("Mark added: {0}", AddDate);
            Console.WriteLine("Mark value: {0}", Value);
        }

        public static void Add(User user)
        {
            using (var db = new BlogContext())
            {
                while (true)
                {
                    Console.Write("Enter the name or title of the entity you want to add a mark to: ");
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

                        if(post.Blog.UserName == user.UserName)
                        {
                            Console.WriteLine("You cant mark your own entities.");
                        }
                        else if(post.Marks.Any(o => o.UserName == user.UserName))
                        {
                            Console.WriteLine("You have already marked this entity.");
                        }
                        else
                        {
                            int value;
                            while (true)
                            {
                                Console.Write("Enter your mark:");
                                value = Convert.ToInt32(Console.ReadLine());
                                if (value > 5 || value < 1)
                                {
                                    Console.WriteLine("Mark must be between 1 and 5!");
                                }
                                else break;
                            }
                            double weight = 4 + db.Users.Where(o => o.UserName == user.UserName).First().GetMark() - post.Blog.User.GetMark();

                            db.PostMarks.Add(new PostMark { PostId = post.PostId, Weight = weight, Value = value, UserName = user.UserName, AddDate = DateTime.UtcNow });
                            db.SaveChanges();
                            Console.WriteLine("Adding succesful.");
                            break;
                        }
                    }
                    else if (c.KeyChar == 'b')
                    {
                        if(!db.Blogs.Any(o => o.Name == name))
                        {
                            Console.WriteLine("There is no such blog.");
                            continue;
                        }
                        Blog blog = db.Blogs.Where(o => o.Name == name).First();

                        if (blog.UserName == user.UserName)
                        {
                            Console.WriteLine("You cant mark your own entities.");
                        }
                        else if (blog.Marks.Any(o => o.UserName == user.UserName))
                        {
                            Console.WriteLine("You have already marked this entity.");
                        }
                        else
                        {
                            int value;
                            while (true)
                            {
                                Console.Write("Enter your mark:");
                                value = Convert.ToInt32(Console.ReadLine());
                                if (value > 5 || value < 1)
                                {
                                    Console.WriteLine("Mark must be between 1 and 5!");
                                }
                                else break;
                            }
                            double weight = 4 + db.Users.Where(o => o.UserName == user.UserName).First().GetMark() - blog.User.GetMark();

                            db.BlogMarks.Add(new BlogMark { BlogId = blog.BlogId, Weight = weight, Value = value, UserName = user.UserName, AddDate = DateTime.UtcNow });
                            db.SaveChanges();
                            Console.WriteLine("Adding succesful.");
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknown command.");
                    }
                }
            }
        }
    }

    public class BlogMark : Mark
    {
        public int BlogMarkId { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }

    public class PostMark : Mark
    {
        public int PostMarkId { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}