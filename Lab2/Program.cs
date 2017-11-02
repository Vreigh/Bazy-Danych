using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        private static bool run = true;
        private static User logged = null;

        static void Main(string[] args)
        {
            DisplayUsers();
            while (run)
            {
                var choice = Console.ReadLine();
                switch(choice)
                {
                    case "q":
                        run = false;
                        break;
                    case "register":
                        RegisterUser();
                        break;
                    case "login":
                        Login();
                        break;
                    case "logout":
                        Logout();
                        break;
                    case "users":
                        DisplayUsers();
                        break;
                    case "user":
                        ShowUser();
                        break;
                    case "blog":
                        ShowBlog();
                        break;
                    case "post":
                        ShowPost();
                        break;
                    case "add":
                        Add();
                        break;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
        }

        private static void DisplayUsers()
        {
            using (var db = new BlogContext())
            {
                var query = from user in db.Users
                            orderby user.DisplayName
                            select user;

                foreach(var user in query)
                {
                    Console.WriteLine("User: {0}      ||     Mark: {1}", user.DisplayName, user.GetMark());
                }

            }
        }

        private static void RegisterUser()
        {
            using (var db = new BlogContext())
            {
                string userName, displayName, password, checkPassword;
                while (true)
                {
                    Console.Write("Enter user name: ");
                    userName = Console.ReadLine();
                    if (db.Users.Any(o => o.UserName == userName))
                    {
                        Console.WriteLine("user name already taken!");
                    }
                    else break;
                }
                while(true)
                {
                    Console.Write("Enter display name: ");
                    displayName = Console.ReadLine();
                    if (db.Users.Any(o => o.DisplayName == displayName))
                    {
                        Console.WriteLine("display name already taken!");
                    }
                    else break;
                }
                while(true)
                {
                    Console.Write("Enter password: ");
                    password = Console.ReadLine();
                    Console.Write("Confirm password: ");
                    checkPassword = Console.ReadLine();

                    if (checkPassword != password)
                    {
                        Console.WriteLine("passwords do not match!");
                    }
                    else break;
                }

                var User = new User { UserName = userName, DisplayName = displayName, Password = password };
                db.Users.Add(User);
                db.SaveChanges();

                Console.WriteLine("User registered succesfuly!");
            }
        }
        private static void Login()
        {
            using (var db = new BlogContext())
            {
                if(logged != null)
                {
                    Console.WriteLine("Already logged in, {0}!", logged.DisplayName);
                    return;
                }

                string userName, password;
                Console.Write("Enter your user name: ");
                userName = Console.ReadLine();
                Console.Write("Enter your password: ");
                password = Console.ReadLine();

                if (db.Users.Any(o => (o.UserName == userName) && (o.Password == password)))
                {
                    User logging = db.Users.Where(o => (o.UserName == userName) && (o.Password == password)).First();
                    logged = logging;
                    Console.WriteLine("Logged in succesfuly, {0}!", logging.DisplayName);
                }
                else Console.WriteLine("Wrong user name or password!");
            }
        }
        private static void Logout()
        {
            if (logged != null)
            {
                Console.WriteLine("Logged out succesfuly, good bye {0}", logged.DisplayName);
                logged = null;
            }
            else Console.WriteLine("You are not logged in!");
        }
        private static void ShowUser()
        {
            Console.Write("Enter the name of the user: ");
            string name = Console.ReadLine();
            using (var db = new BlogContext())
            {
                if(!db.Users.Any(o => o.DisplayName == name))
                {
                    Console.WriteLine("User not found.");
                    return;
                }
                else
                {
                    User user = db.Users.Where(o => o.DisplayName == name).First();
                    user.Show();
                }
            }
        }
        private static void ShowBlog()
        {
            Console.Write("Enter the name of the blog: ");
            string name = Console.ReadLine();
            using (var db = new BlogContext())
            {
                if (!db.Blogs.Any(o => o.Name == name))
                {
                    Console.WriteLine("Blog not found.");
                    return;
                }
                else
                {
                    Blog blog = db.Blogs.Where(o => o.Name == name).First();
                    blog.Show();
                }
            }
        }
        private static void ShowPost()
        {
            Console.Write("Enter the title of the post: ");
            string title = Console.ReadLine();
            using (var db = new BlogContext())
            {
                if (!db.Posts.Any(o => o.Title == title))
                {
                    Console.WriteLine("Post not found.");
                    return;
                }
                else
                {
                    Post post = db.Posts.Where(o => o.Title == title).First();
                    post.Show();
                }
            }
        }
        private static void Add()
        {
            if(logged == null)
            {
                Console.WriteLine("You cant add anything unless you are logged in!");
                return;
            }

            Console.WriteLine("What would you like to add? A Blog, Post, Comment or a Mark? Hit b/p/c/m");
            var c = Console.ReadKey();
            Console.WriteLine();

            switch (c.KeyChar)
            {
                case 'b':
                    Blog.Add(logged);
                    break;
                case 'p':
                    Post.Add(logged);
                    break;
                case 'c':
                    Comment.Add(logged);
                    break;
                case 'm':
                    Mark.Add(logged);
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    break;

            }
        }
    }
}
