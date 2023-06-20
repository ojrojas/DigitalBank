namespace DigitalBank.Grpc.Services;

/// <summary>
/// Person services
/// </summary>
public class PersonService : IntegrationPersonService.IntegrationPersonServiceBase
{
    /// <summary>
    /// Repository person
    /// </summary>
    private readonly PersonRepository _personRepository;
    /// <summary>
    /// Logger 
    /// </summary>
    private readonly ILogger<PersonService> _logger;

    /// <summary>
    /// Constructor Personservice
    /// </summary>
    /// <param name="personRepository">Person repository</param>
    /// <param name="logger">logger application</param>
    /// <exception cref="ArgumentNullException"></exception>
    public PersonService(PersonRepository personRepository, ILogger<PersonService> logger)
    {
        _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Create person
    /// </summary>
    /// <param name="request">Create person request</param>
    /// <param name="context">Context call context</param>
    /// <returns>Create person response grpc</returns>
    public override async Task<CreatePersonResponse> CreatePerson(CreatePersonRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Execute service grpc Created Person");
        var input = Mappers.PersonMapper.CreatePersonRequestToPerson(request);
        var output = await _personRepository.CreatePersonAsync(input, default);
        return Mappers.PersonMapper.PersonToCreatePersonResponse(output);
    }

    /// <summary>
    /// Update person
    /// </summary>
    /// <param name="request">Update person request</param>
    /// <param name="context">Context call context</param>
    /// <returns>Update person response grpc</returns>
    public override async Task<UpdatePersonResponse> UpdatePerson(UpdatePersonRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Execute service grpc Updated Person");
        var input = Mappers.PersonMapper.UpdatePersonRequestToPerson(request);
        var output = await _personRepository.UpdatePersonAsync(input, default);
        return Mappers.PersonMapper.PersonToUpdatePersonResponse(output);
    }

    /// <summary>
    /// Delete Person
    /// </summary>
    /// <param name="request">Delete person request grpc</param>
    /// <param name="context">Server call context</param>
    /// <returns>Delete response grpc</returns>
    public override async Task<DeletePersonResponse> DeletePerson(DeletePersonRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Execute service grpc Delete Person");
        var output = await _personRepository.DeletePersonAsync(Guid.Parse(request.Id), default);
        return Mappers.PersonMapper.PersonToDeletePersonResponse(output);
    }

    /// <summary>
    /// GetById Person
    /// </summary>
    /// <param name="request">Get by person grpc</param>
    /// <param name="context">Server call context</param>
    /// <returns>person found grpc</returns>
    public override async Task<GetByIdPersonResponse> GetByIdPerson(GetByIdPersonRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Execute service grpc Delete Person");
        var output = await _personRepository.GetByIdPersonAsync(Guid.Parse(request.Id), default);
        return Mappers.PersonMapper.PersonToGetByIdPersonResponse(output);
    }

    /// <summary>
    /// Get all persons
    /// </summary>
    /// <param name="request">Empty request</param>
    /// <param name="context">Server call context</param>
    /// <returns>List persons grpc</returns>
    public override async Task<GetAllPersonsResponse> GetAllPersons(Empty request, ServerCallContext context)
    {
        _logger.LogInformation("Execute service grpc Delete Person");
        var output = await _personRepository.GetAllPersonsAsync(default);
        return Mappers.PersonMapper.PersonToGetAllPersonsResponse(output);
    }
}
