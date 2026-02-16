namespace ApiProjeKampi.WebUI.Dtos.ProductDtos;

public class CreateProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public int Price { get; set; }
    public int? CategoryId { get; set; }
}