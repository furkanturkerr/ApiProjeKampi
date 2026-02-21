namespace ApiProjeKampi.WebApi.Dtos.EmployeeTaskDtos;

public class CreateEmployeeTaskDto
{
    public string TaskName { get; set; }
    public byte TaskStatusValue { get; set; }
    public DateTime AssignDate { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
    public string TaskStatus { get; set; }
    public int ChefId { get; set; }
}