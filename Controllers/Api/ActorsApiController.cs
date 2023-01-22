using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Services;
using MoviesApp.Services.Dto;
using MoviesApp.ViewModels.Actors;

namespace MoviesApp.Controllers.Api;

[Route("api/actors")]
[ApiController]
public class ActorsApiController : ControllerBase
{
    private readonly IActorService _service;

    public ActorsApiController(IActorService service)
    {
        _service = service;
    }
    
    [HttpGet] // GET: /api/actors
    [ProducesResponseType(200, Type = typeof(IEnumerable<ActorDto>))]
    [ProducesResponseType(404)]
    public ActionResult<IEnumerable<ActorDto>> GetActors()
    {
        return Ok(_service.GetAllActors());
    }

    [HttpGet("{id}")] // GET: /api/actors/5
    [ProducesResponseType(200, Type = typeof(ActorDto))]
    [ProducesResponseType(404)]
    public IActionResult GetById(int id)
    {
        var actor = _service.GetActor(id);
        if (actor == null) return NotFound();
        return Ok(actor);
    }

    [HttpPost]  // POST: api/actors
    public ActionResult<ActorDto> PostActor(ActorDto inputDto)
    {
        var actor = _service.AddActor(inputDto);
        return CreatedAtAction("GetById", new {id = actor.Id}, actor);
    }

    [HttpPut("{id}")] // PUT: api/actors/5
    public IActionResult UpdateActor(ActorDto editDto)
    {
        var actor = _service.UpdateActor(editDto);
        if (actor == null) return BadRequest();

        return Ok(actor);
    }

    [HttpDelete("{id}")] // DELETE: api/actors/5
    public ActionResult<DeleteActorViewModel> DeleteActor(int id)
    {
        var actor = _service.DeleteActor(id);
        
        if (!actor) 
            return NotFound();
        
        return Ok(actor);
    }
}