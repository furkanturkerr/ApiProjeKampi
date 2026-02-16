namespace ApiProjeKampi.WebApi.Dtos.TestimonialDtos;

public class GetTestimonialByIdDto
{
    public int TestimonialId { get; set; }
    public string NameSurname { get; set; }
    public string Title { get; set; }
    public string Commend { get; set; }
    public string ImageUrl { get; set; }
}