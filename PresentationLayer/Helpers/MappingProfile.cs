using AutoMapper;
using BusinessServiceLayer.DTOs;
using PresentationLayer.Models;
using PresentationLayer.ViewModels;
using RepositoryLayer.Models;
namespace PresentationLayer.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerToAddOrUpdateDTO, Customer>();
            CreateMap<CreateCustomerViewModel, CustomerToAddOrUpdateDTO>();
            CreateMap<UpdateCustomerViewModel, CustomerToAddOrUpdateDTO>();
            CreateMap<Customer, UserDTO>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.CustomerId))
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.CustomerFullName))
                .ForMember(d => d.Birthday, o => o.MapFrom(s => s.CustomerBirthday));
            CreateMap<UserDTO, UserAccountModel>();
            CreateMap<BookingReservation, BookingReservationDTO>();
        }
    }
}
