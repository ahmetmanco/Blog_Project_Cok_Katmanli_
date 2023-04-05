using _4_Blog_Project_UI.Models;
using Blog_Project_Application.Services.PostServices;
using Microsoft.AspNetCore.Mvc;
using Models.VM.PostVM;
using System.Diagnostics;

namespace _4_Blog_Project_UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostServices _postServices;
        public HomeController(IPostServices postServices)
        {
            _postServices = postServices;
        }
        public async Task<IActionResult> Index()
        {
            //postları görüntüleme sayfası
            //ModelBinderAttribute, postları iletecek view'a
           List<PostVM> posts = await _postServices.GetPosts();
             return View(posts);
        }
    }
}