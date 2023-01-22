using AutoMapper;
using MoviesApp.Models;
using MoviesApp.ViewModels.Actors;

namespace MoviesApp.Services.Dto.AutoMapperProfiles;

public class ActorDtoProfile : Profile
{
    public ActorDtoProfile()
    {
        CreateMap<Actor,ActorDto>().ReverseMap();
        CreateMap<ActorDto, ActorViewModel>();
        CreateMap<ActorDto, DeleteActorViewModel>();
        CreateMap<ActorDto, EditActorViewModel>();
        CreateMap<ActorDto, InputActorViewModel>();
    }
}