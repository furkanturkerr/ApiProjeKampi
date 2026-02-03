namespace ApiProjeKampi.WebApi.Dto.ContactDto;

public class UpdateContactDto
{
    public int ContactId { get; set; }
    public string Map { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string OpenHours { get; set; }
}