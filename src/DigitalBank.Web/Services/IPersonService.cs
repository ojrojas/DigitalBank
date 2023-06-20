namespace DigitalBank.Web.Services;

public interface IPersonService
{
    Task<Data.Person> CreatePersonAsync(Data.Person person);
    Task<Data.Person> DeletePersonAsync(Guid Id);
    Task<Data.Person> GetPersonByIdAsync(Guid Id);
    Task<IEnumerable<Data.Person>> GetAllPersonsAsync();
    Task<Data.Person> UpdatePersonAsync(Data.Person person);
}