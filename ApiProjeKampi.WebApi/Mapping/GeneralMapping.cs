using ApiProjeKampi.WebApi.Dtos.AboutDtos;
using ApiProjeKampi.WebApi.Dtos.CategoryDtos;
using ApiProjeKampi.WebApi.Dtos.EmployeeTaskDtos;
using ApiProjeKampi.WebApi.Dtos.FeatureDtos;
using ApiProjeKampi.WebApi.Dtos.GroupReservationDtos;
using ApiProjeKampi.WebApi.Dtos.ImagesDtos;
using ApiProjeKampi.WebApi.Dtos.MessageDtos;
using ApiProjeKampi.WebApi.Dtos.NotificationDtos;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using ApiProjeKampi.WebApi.Dtos.RezervationDtos;
using ApiProjeKampi.WebApi.Dtos.TestimonialDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;

namespace ApiProjeKampi.WebApi.Mapping;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Feature, CreateFeatureDto>().ReverseMap();
        CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
        CreateMap<Feature, ResultFeatureDto>().ReverseMap();
        CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();
        
        CreateMap<Message, CreateMessageDto>().ReverseMap();
        CreateMap<Message, GetByIdMessageDto>().ReverseMap();
        CreateMap<Message, ResultMessageDto>().ReverseMap();
        CreateMap<Message, UpdateMessageDto>().ReverseMap();

        CreateMap<Notification, ResultNotificationDto>().ReverseMap();
        CreateMap<Notification, CreateNotificationDto>().ReverseMap();
        CreateMap<Notification, UpdateNotificationDto>().ReverseMap();
        CreateMap<Notification, GetNotificationByIdDto>().ReverseMap();
        
        CreateMap<Category, CreateCategoryDto>().ReverseMap();
        CreateMap<Category, UpdateCategoryDto>().ReverseMap();
        
        CreateMap<About, GetAboutByIdDto>().ReverseMap();
        CreateMap<About, CreateAboutDto>().ReverseMap();
        CreateMap<About, UpdateAboutDto>().ReverseMap();
        CreateMap<About, ResultAboutDto>().ReverseMap();
        
        CreateMap<Testimonial, GetTestimonialByIdDto>().ReverseMap();
        CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
        CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
        CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
        
        CreateMap<Rezervation, GetRezervationByIdDto>().ReverseMap();
        CreateMap<Rezervation, CreateRezervationDto>().ReverseMap();
        CreateMap<Rezervation, UpdateRezervationDto>().ReverseMap();
        CreateMap<Rezervation, ResultRezervationDto>().ReverseMap();
        
        CreateMap<Image, CreateImageDto>().ReverseMap();
        CreateMap<Image, GetImageByIdDto>().ReverseMap();
        CreateMap<Image, ResultImageDto>().ReverseMap();
        CreateMap<Image, UpdateImageDto>().ReverseMap();
        
        CreateMap<EmployeeTask, GetEmployeeTaskByIdDto>().ReverseMap();
        CreateMap<EmployeeTask, CreateEmployeeTaskDto>().ReverseMap();
        CreateMap<EmployeeTask, UpdateEmployeeTaskDto>().ReverseMap();
        CreateMap<EmployeeTask, ResultEmployeeTaskDto>().ReverseMap();
        
        CreateMap<GroupReservation, ResultGroupReservationDto>().ReverseMap();
        CreateMap<GroupReservation, CreateGroupReservationDto>().ReverseMap();
        CreateMap<GroupReservation, UpdateGroupReservationDto>().ReverseMap();
        CreateMap<GroupReservation, GetByGroupReservationIdDto>().ReverseMap();
        
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, ResultProductWithCategoryDto>().ForMember(x=>x.CategoryName,y=>y.MapFrom(y=>y.Category.Name)).ReverseMap();
    }
}