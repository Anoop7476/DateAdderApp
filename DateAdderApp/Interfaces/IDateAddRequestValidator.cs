using DateAdderApp.Models;
namespace DateAdderApp.Interfaces; 
public interface IDateAddRequestValidator 
{ 
    ValidationResult Validate(DateAddRequest request); 
}