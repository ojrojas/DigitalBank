namespace DigitalBank.Core.Repositories;

/// <summary>
/// PersonRepository
/// </summary>
public class PersonRepository : GenericRepository<Person>
{
    /// <summary>
    /// Logger application
    /// </summary>
    private readonly ILogger<GenericRepository<Person>> _logger;

    /// <summary>
    /// PersonRepository
    /// </summary>
    /// <param name="logger">Logger application</param>
    /// <param name="configuration">Configuration application</param>
    public PersonRepository(ILogger<GenericRepository<Person>> logger, IConfiguration configuration) : base(logger, configuration)
    {
        _logger = logger;
    }

    /// <summary>
    /// Create person 
    /// </summary>
    /// <param name="person">Person model</param>
    /// <param name="cancellationToken">Token cancellation</param>
    /// <returns>Person created</returns>
    public async Task<Person> CreatePersonAsync(Person person, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create person parameters: {person}", JsonSerializer.Serialize(person));
        DynamicParameters parameters = new DynamicParameters();
        person.Id = Guid.NewGuid();
        parameters.Add(nameof(person.Id), person.Id);
        parameters.Add(nameof(person.Name), person.Name);
        parameters.Add(nameof(person.BirthDate), person.BirthDate);
        parameters.Add(nameof(person.TypeSex), person.TypeSex);
        parameters.Add(nameof(person.CreatedOn), person.CreatedOn);

        _logger.LogInformation("Execute sp_createperson  procedure person");
        return await SqlRawExecutionAsync(ConstantsRepositoryPerson.CreatePerson, parameters, CommandType.StoredProcedure, cancellationToken);
    }

    /// <summary>
    /// UpdatePerson 
    /// </summary>
    /// <param name="person">Person model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Person updated</returns>
    public async Task<Person> UpdatePersonAsync(Person person, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Create person parameters: {person}", JsonSerializer.Serialize(person));
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add(nameof(person.Id), person.Id);
        parameters.Add(nameof(person.Name), person.Name);
        parameters.Add(nameof(person.BirthDate), person.BirthDate);
        parameters.Add(nameof(person.TypeSex), person.TypeSex);
        parameters.Add(nameof(person.UpdatedOn), person.UpdatedOn);

        _logger.LogInformation("Execute sp_updateperson procedure");
        return await SqlRawExecutionAsync(ConstantsRepositoryPerson.UpdatePerson, parameters, CommandType.StoredProcedure, cancellationToken);
    }

    /// <summary>
    /// Delete person
    /// </summary>
    /// <param name="Id">Id entity model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Person deleted</returns>
    public async Task<Person> DeletePersonAsync(Guid Id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete person parameters: {Id}", Id);
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add(nameof(Id), Id);

        _logger.LogInformation("Execute sp_deleteperson procedure");
        return await SqlRawExecutionAsync(ConstantsRepositoryPerson.DeletePerson, parameters, CommandType.StoredProcedure, cancellationToken);
    }

    /// <summary>
    /// Get by id person
    /// </summary>
    /// <param name="Id">Id person model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Person found</returns>
    public async Task<Person> GetByIdPersonAsync(Guid Id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get person by id parameters: {Id}", Id);
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add(nameof(Id), Id);

        _logger.LogInformation("Execute sp_getbyidperson procedure");
        return await SqlRawExecutionAsync(ConstantsRepositoryPerson.GetByIdPerson, parameters, CommandType.StoredProcedure, cancellationToken);
    }

    /// <summary>
    /// Get all persons
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>List entities models</returns>
    public async Task<IEnumerable<Person>> GetAllPersonsAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get all persons");

        _logger.LogInformation("Execute sp_getallpersons procedure");
        return await SqlRawExecutionMultimeAsync(ConstantsRepositoryPerson.GetAllPersons, null!, CommandType.StoredProcedure, cancellationToken);
    }
}