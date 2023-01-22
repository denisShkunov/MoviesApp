using System.Collections.Generic;
using MoviesApp.Services.Dto;

namespace MoviesApp.Services;

public interface IActorService
{
    ActorDto AddActor(ActorDto actorDto);
    
    ActorDto GetActor(int id);

    IEnumerable<ActorDto> GetAllActors();

    ActorDto UpdateActor(ActorDto actorDto);
    
    bool DeleteActor(int id);
}