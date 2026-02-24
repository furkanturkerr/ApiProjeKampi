namespace ApiProjeKampi.WebUI.Dtos.GroupRezervationDtos;

public class ResultGroupReservationDto
{
    public int GroupReservationId { get; set; }
    public string ResponsibleCustomerName { get; set; }
    public string GroupTitle { get; set; }
    public DateTime RezervationDate { get; set; }
    public DateTime LastProcessDate { get; set; }
    public string Priority { get; set; }
    public string Details { get; set; }
    public string ReservationStatus { get; set; }
    public int? PersonCount { get; set; }
    public string? Email { get; set; }
}