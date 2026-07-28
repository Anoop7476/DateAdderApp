namespace DateAdderApp.Models; 
public record ValidationResult(bool IsValid, string? ErrorMessage = null) 
{ 
    public static ValidationResult Ok() => new(true); 
    public static ValidationResult Fail(string message) => new(false, message); 
}