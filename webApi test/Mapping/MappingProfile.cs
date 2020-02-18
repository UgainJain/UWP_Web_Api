using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webApi_test.Models;
using webApi_test.Models.DTO;
using webApi_test.ViewModels.Booking;
using webApi_test.ViewModels.Resource;
using webApi_test.ViewModels.ResourceType;

namespace webApi_test.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ResourceTypeModel, ResorceTypesDTO>();
            CreateMap<ResourceTypeViewModel, ResourceTypeModel>();
            CreateMap<Resource_DTO, ResourceModel>();
            CreateMap<Resource_ViewModel, Resource_DTO>();
            CreateMap<Resource_ViewModel, ResourceModel>().ReverseMap();
            CreateMap<BookingViewModel, BookingModel>().ReverseMap();

        }
    }
}
