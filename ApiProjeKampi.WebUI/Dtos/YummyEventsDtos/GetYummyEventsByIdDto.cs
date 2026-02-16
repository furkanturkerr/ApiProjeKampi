namespace ApiProjeKampi.WebUI.Dtos.YummyEventsDtos;

public class GetYummyEventsByIdDto
{
    public int YummyEventId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool Status { get; set; }
    public string Price { get; set; }
}