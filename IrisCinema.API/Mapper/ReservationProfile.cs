using AutoMapper;
using IrisCinema.API.DTO.Reservation;
using IrisCinema.API.Models;

namespace IrisCinema.API.Mapper
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<CreateReservationDto, Reservation>()
                .ForMember(dest => dest.CreatedAt,
                           opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<Reservation, ReservationResponseDto>();
        }
    }
}
