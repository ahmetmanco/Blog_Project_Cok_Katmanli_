using Blog_Project_Application.Models.DTOs.PostDTO;
using Blog_Project_Application.Services.PostServices;
using Blog_Project_Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.VM.PostVM;

namespace _4_Blog_Project_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostServices _postServices;
        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }

        public async Task<IActionResult> Index()
        {
            List<PostVM> posts = await _postServices.GetPosts();

            return View(posts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreatePostDTO CreateDto = await _postServices.CreatePost();

            ViewBag.Genres = new SelectList(CreateDto.Genres, "Id", "Name");
            ViewBag.Authors = new SelectList(CreateDto.Authors, "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDTO model)
        {
            if(ModelState.IsValid)
            {
                await _postServices.Create(model);
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            CreatePostDTO updateDTO = await _postServices.CreatePost();

            ViewBag.Genre = new SelectList(updateDTO.Genres, "Id", "Name");
            ViewBag.Author = new SelectList(updateDTO.Authors, "Id", "FullName");

            return View(await _postServices.GetById(Id));
        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDTO model)
        {
            if(ModelState.IsValid)
            {
                await _postServices.Update(model);
                return RedirectToAction("Index");
            }
            else
                 return View(model);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _postServices.Delete(Id);
            return RedirectToAction("Index");
        }

    }
}
