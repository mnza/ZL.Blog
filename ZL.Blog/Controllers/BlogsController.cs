using Microsoft.AspNetCore.Mvc;
using ZL.Blog.Models;
using System.Text;

namespace ZL.Blog.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public BlogsController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string blogPath = _configuration["BlogAddress"];

            if (!Directory.Exists(blogPath))
            {
                blogPath = _webHostEnvironment.WebRootPath + @"\" + blogPath;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(blogPath);
            List<Models.Blog> blogs = new List<Models.Blog>();
            Test(directoryInfo, ref blogs);

            ViewBag.BlogCategory = blogs.GroupBy(x => x.Category).Select(x => new
            {
                Category = x.Key,
                Count = x.Count()
            }).ToList();
            return View(blogs);
        }

        [Route("blogs/{category}/{file}")]
        public IActionResult Details(string category,string file)
        {
            ViewBag.CurrentBlog = file;
            ViewBag.CurrentCategory = category;

            string blogPath = _configuration["BlogAddress"];

            if (!Directory.Exists(blogPath))
            {
                blogPath = _webHostEnvironment.WebRootPath + @"\" + blogPath;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(blogPath);
            List<Models.Blog> blogs = new List<Models.Blog>();
            Test(directoryInfo, ref blogs);

            ViewBag.BlogCategory = blogs.GroupBy(x => x.Category).Select(x => new
            {
                Category = x.Key,
                Count = x.Count()
            }).ToList();
            return View(blogs.Where(x => x.Category == category).OrderByDescending(x => x.LastModifyDateTime));
        }

        [Route("blogs/{category}")]
        public IActionResult Details(string category)
        {
            string blogPath = _configuration["BlogAddress"];
            ViewBag.CurrentCategory = category;

            if (!Directory.Exists(blogPath))
            {
                blogPath = _webHostEnvironment.WebRootPath + @"\" + blogPath;
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(blogPath);
            List<Models.Blog> blogs = new List<Models.Blog>();
            Test(directoryInfo, ref blogs);

            ViewBag.BlogCategory = blogs.GroupBy(x => x.Category).Select(x => new
            {
                Category = x.Key,
                Count = x.Count()
            }).ToList();
            return View(blogs.Where(x => x.Category == category).OrderByDescending(x => x.LastModifyDateTime));
        }



        private void Test(DirectoryInfo dir, ref List<Models.Blog> blogs)
        {
            foreach (var file in dir.GetFiles())
            {
                var blog = new Models.Blog();
                blog.Name = Path.GetFileNameWithoutExtension(file.Name);
                string extension = Path.GetExtension(file.Name);
                switch (extension)
                {
                    case ".html":
                    case ".htm":
                        blog.FileType = FileType.Blog;
                        break;
                    case ".doc":
                        blog.FileType = FileType.Word;
                        break;
                    case ".pdf":
                        blog.FileType = FileType.Pdf;
                        break;
                    case ".txt":
                        blog.FileType = FileType.Text;
                        break;
                    case ".md":
                        blog.FileType = FileType.Markdown;
                        break;
                    case ".png":
                    case ".PNG":
                    case ".JPG":
                    case ".jpg":
                    case ".bmp":
                    case ".svg":
                    case ".webp":
                        blog.FileType = FileType.Image;
                        break;
                    default:
                        blog.FileType = FileType.Other;
                        break;
                }
                blog.Path = file.FullName.Substring(_webHostEnvironment.WebRootPath.Length);
                blog.Category = dir.Name;
                blog.LastModifyDateTime = file.LastAccessTime;
                blogs.Add(blog);
            }

            foreach (var dirInfo in dir.GetDirectories())
            {
                if (dirInfo.Name.EndsWith("_files")) continue;
                Test(dirInfo, ref blogs);
            }
        }
    }
}
