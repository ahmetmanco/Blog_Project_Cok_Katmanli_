using _3_Blog_Project_Application.Models.DTOs.GenreDTO;
using _3_Blog_Project_Application.Services.GenreServices;
using Blog_Project_Application.Models.VM.GenreVM;
using Blog_Project_Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace _4_Blog_Project_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IGenreServices _genreServices;

        public GenreController(IGenreServices genreServices)
        {
            _genreServices = genreServices;

        }
        //Genre list
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<GenreVM> genres = await _genreServices.GetGenres();

            return View(genres);
        }
        //genre Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreDTO model)
        {

            if (ModelState.IsValid)
            {
                await _genreServices.Create(model);
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {

            return View(await _genreServices.GetById(Id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateGenreDTO model)
        {

            if (ModelState.IsValid)
            {
                await _genreServices.Update(model);
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _genreServices.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
