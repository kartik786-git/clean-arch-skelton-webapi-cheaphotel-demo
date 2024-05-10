using Application.Common;
using Application.Features;
using Application.Features.Room.Queries.GetRoomsAvailability;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // required mapping here

            CreateMap<Hotel, HotelDto>().
                 //.ForMember(des => des.Title, opt => opt.MapFrom(src => src.Name))
                 ReverseMap();

            CreateMap<Room, RoomDto>().
                 //.ForMember(des => des.Title, opt => opt.MapFrom(src => src.Name))
                 ReverseMap();

            CreateMap<Booking, BookingDto>().
             //.ForMember(des => des.Title, opt => opt.MapFrom(src => src.Name))
             ReverseMap();

            CreateMap<Room, RoomsAvailabilityDto>()
                .ForMember(des => des.IsRoomAvailable, opt => opt.MapFrom(src => src.Bookings.Count > 0))
                .ForMember(des => des.LeftDays, opt => opt.MapFrom(src => CalculateActiveDays.CalculateDays(src)))
                .ReverseMap();


        }
    }
}
