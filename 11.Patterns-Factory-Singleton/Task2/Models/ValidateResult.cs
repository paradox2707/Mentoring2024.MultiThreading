namespace Task2.Models;

public class ValidateResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new List<string>();
}