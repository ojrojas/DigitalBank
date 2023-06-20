using DigitalBank.Web.Mappers;

namespace DigitalBank.Web.Services;

public class PersonService : IPersonService
{
    private readonly IntegrationPersonService.IntegrationPersonServiceClient _client;
    private readonly ILogger<PersonService> _logger;
    private readonly ILogService _logService;

    public PersonService(IntegrationPersonService.IntegrationPersonServiceClient client, ILogger<PersonService> logger, ILogService logService)
    {
        _client = client;
        _logger = logger;
        _logService = logService;
    }

    public async Task<Data.Person> CreatePersonAsync(Data.Person person)
    {
        var request = MapperPersonGrpc.PersonToCreatePersonRequest(person);

        _logger.LogInformation("Create person request web");
        _logger.LogInformation("Grpc PersonService Create request {request}", request);
       
        var response = await _client.CreatePersonAsync(request);
        _logger.LogInformation("Grpc PersonService Create response {response}", response);
        return MapperPersonGrpc.CreatePersonResponseToPerson(response);
    }

    public async Task<Data.Person> UpdatePersonAsync(Data.Person person)
    {
        var request = MapperPersonGrpc.PersonToUpdatePersonRequest(person);

        _logger.LogInformation("Update person request web");
        _logger.LogInformation("Grpc PersonService Update request {request}", request);
        var response = await _client.UpdatePersonAsync(request);
        _logger.LogInformation("Grpc PersonService Update response {response}", response);
        return MapperPersonGrpc.UpdatePersonResponseToPerson(response);
    }

    public async Task<Data.Person> DeletePersonAsync(Guid Id)
    {
        _logger.LogInformation("Delete person request web");
        _logger.LogInformation("Grpc PersonService Delete request {Id}", Id);

        var response = await _client.DeletePersonAsync(new DeletePersonRequest { Id = Id.ToString() });
        _logger.LogInformation("Grpc PersonService Delete response {response}", response);
        return MapperPersonGrpc.DeletePersonResponseToPerson(response);
    }

    public async Task<Data.Person> GetPersonByIdAsync(Guid Id)
    {
        _logger.LogInformation("Get by id person request web");
        _logger.LogInformation("Grpc PersonService Get by id request {Id}", Id);

        var response = await _client.GetByIdPersonAsync(new GetByIdPersonRequest { Id = Id.ToString() });
        _logger.LogInformation("Grpc PersonService Get by id response {response}", response);
        return MapperPersonGrpc.GetByIdPersonResponseToPerson(response);
    }

    public async Task<IEnumerable<Data.Person>> GetAllPersonsAsync()
    {
        try
        {
            _logger.LogInformation("Get all persons request web");
            _logger.LogInformation("Grpc PersonService Get all request");

            var response = await _client.GetAllPersonsAsync(new Empty());
            _logger.LogInformation("Grpc PersonService Get all response {response}", response);
            return MapperPersonGrpc.GetAllPersonsResponseToPersons(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}