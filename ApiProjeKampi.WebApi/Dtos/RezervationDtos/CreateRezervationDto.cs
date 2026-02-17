namespace ApiProjeKampi.WebApi.Dtos.RezervationDtos;

public class CreateRezervationDto
{
    public int RezervationId { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public int CountOfPeople { get; set; }
    public string Message { get; set; }
    public bool Status { get; set; }
}