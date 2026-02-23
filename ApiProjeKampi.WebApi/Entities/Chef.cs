using System.Text.Json.Serialization;

namespace ApiProjeKampi.WebApi.Entities;

public class Chef
{
    public int ChefId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    [JsonIgnore]
    public List<EmployeeTaskChef> EmployeeTaskChefs { get; set; }
}