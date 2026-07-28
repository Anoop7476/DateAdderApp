using DateAdderApp.Models;
namespace DateAdderApp.Interfaces; public interface IDateAddService { DateAddResponse AddDays(DateAddRequest request); }