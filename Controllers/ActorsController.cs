using System.Collections.Generic;
using System.Linq;
using AutoMapper;
//using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Filters;
using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.Services.Dto;
using MoviesApp.ViewModels.Actors;

namespace MoviesApp.Controllers;

public class ActorsController : Controller
{
     private readonly MoviesContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        private readonly IActorService _service;


        public ActorsController(MoviesContext context, ILogger<HomeController> logger,  IMapper mapper, IActorService service)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        // GET: Actors
        [HttpGet]
        public IActionResult Index()
        {
            var actors = _mapper.Map<IEnumerable<ActorDto>, IEnumerable<ActorViewModel>>(_service.GetAllActors());
            return View(actors);
        }

        // GET: Actors/Details/5
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var viewModel = _mapper.Map<ActorViewModel>(_service.GetActor((int) id));

            if (viewModel == null) return NotFound();

            return View(viewModel);
        }
        
        // GET: Actors/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActorAge(7, 99)]
        public IActionResult Create([Bind("Title,ReleaseDate,Genre,Price")] InputActorViewModel inputModel)
        {
            if (!ModelState.IsValid) return View(inputModel);
            _service.AddActor(_mapper.Map<ActorDto>(inputModel));
            return RedirectToAction(nameof(Index));

        }
        
        [HttpGet]
        // GET: Actors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var editModel = _mapper.Map<EditActorViewModel>(_service.GetActor((int) id));

            if (editModel == null) return NotFound();

            return View(editModel);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActorAge(7, 99)]
        public IActionResult Edit(int id, [Bind("Title,ReleaseDate,Genre,Price")] EditActorViewModel editModel)
        {
            if (!ModelState.IsValid) return View(editModel);
            var movie = _mapper.Map<ActorDto>(editModel);
            movie.Id = id;
            var result = _service.UpdateActor(movie);
            if (result == null) return NotFound();
            return RedirectToAction(nameof(Index));

        }
        
        [HttpGet]
        // GET: Actors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var deleteModel = _mapper.Map<DeleteActorViewModel>(_service.GetActor((int) id));

            if (deleteModel == null) return NotFound();

            return View(deleteModel);
        }
        
        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _service.DeleteActor(id);
            if (!movie) return NotFound();
            _logger.LogTrace($"Actor with id {id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
}