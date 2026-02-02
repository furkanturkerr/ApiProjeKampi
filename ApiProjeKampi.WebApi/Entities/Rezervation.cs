namespace ApiProjeKampi.WebApi.Entities;

public class Rezervation
{
    public int RezervationId { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Date { get; set; }
    public string Time { get; set; }
    public int CountOfPeople { get; set; }
    public int Message { get; set; }
    public bool Status { get; set; }
}