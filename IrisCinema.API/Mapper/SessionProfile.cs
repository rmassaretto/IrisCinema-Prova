using AutoMapper;

using IrisCinema.API.DTO.Seat;
using IrisCinema.API.DTO.Session;
using IrisCinema.API.Models;

namespace IrisCinema.API.Mapper
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            CreateMap<Seat, SeatDto>();
            CreateMap<Session, SessionDto>()
                .ForMember(dest => dest.RoomId,
                           opt => opt.MapFrom(src => src.Room.Id));
        }
    }
}
