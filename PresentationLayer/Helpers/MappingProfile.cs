using AutoMapper;
using BusinessServiceLayer.DTOs;
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
        }
    }
}
