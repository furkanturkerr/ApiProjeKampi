namespace ApiProjeKampi.WebApi.Dtos.MessageDtos;

public class ResultMessageDto
{
    public int MessageId { get; set; }
    public string NameSurname { get; set; }
    public string Email { get; set; }
    public string Subject { get; set; }
    public string MessageBody { get; set; }
    public DateTime MessageDate { get; set; }
    public bool IsRead { get; set; }
}