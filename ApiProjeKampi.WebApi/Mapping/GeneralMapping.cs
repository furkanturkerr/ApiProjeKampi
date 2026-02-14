using ApiProjeKampi.WebApi.Dtos.FeatureDtos;
using ApiProjeKampi.WebApi.Dtos.MessageDtos;
using ApiProjeKampi.WebApi.Dtos.NotificationDtos;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;
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
        
        CreateMap<Product, CreateProductDto>().ReverseMap();
        CreateMap<Product, ResultProductWithCategoryDto>().ForMember(x=>x.CategoryName,y=>y.MapFrom(y=>y.Category.Name)).ReverseMap();
    }
}