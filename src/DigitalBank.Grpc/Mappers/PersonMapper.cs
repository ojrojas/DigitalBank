namespace DigitalBank.Grpc.Mappers;

/// <summary>
/// Person mapper
/// </summary>
public static class PersonMapper
{
    /// <summary>
    /// Mapper person to createpersonresponse
    /// </summary>
    /// <param name="person">Person model</param>
    /// <returns>Createperson grpc</returns>
    public static CreatePersonResponse PersonToCreatePersonResponse(DigitalBank.Core.Entities.Person person)
    {
        return new CreatePersonResponse
        {
            Id = person.Id.ToString(),
            Name = person.Name,
            BirthDate = Timestamp.FromDateTimeOffset(person.BirthDate),
            TypeSex = person.TypeSex,
            CreatedOn = Timestamp.FromDateTimeOffset(person.CreatedOn)
        };
    }

    /// <summary>
    /// Mapper createpersonrequest
    /// </summary>
    /// <param name="request">Create person request</param>
    /// <returns>Person entity</returns>
    public static DigitalBank.Core.Entities.Person CreatePersonRequestToPerson(CreatePersonRequest request)
    {
        return new Core.Entities.Person
        {
            Name = request.Name,
            BirthDate = request.BirthDate.ToDateTime(),
            TypeSex = request.TypeSex,
            CreatedOn = request.CreatedOn.ToDateTime()
        };
    }

    /// <summary>
    /// Mapper updateperson response
    /// </summary>
    /// <param name="person">Person model</param>
    /// <returns>Updateperson response grpc</returns>
    public static UpdatePersonResponse PersonToUpdatePersonResponse(DigitalBank.Core.Entities.Person person)
    {
        return new UpdatePersonResponse
        {
            Id = person.Id.ToString(),
            Name = person.Name,
            BirthDate = Timestamp.FromDateTimeOffset(person.BirthDate),
            TypeSex = person.TypeSex,
            UpdateOn = Timestamp.FromDateTimeOffset(person.UpdatedOn)
        };
    }

    /// <summary>
    /// Mapper Updateperson to person
    /// </summary>
    /// <param name="request">Updateperson request grpc</param>
    /// <returns>Person</returns>
    public static DigitalBank.Core.Entities.Person UpdatePersonRequestToPerson(UpdatePersonRequest request)
    {
        return new Core.Entities.Person
        {
            Id = Guid.Parse(request.Id),
            Name = request.Name,
            BirthDate = request.BirthDate.ToDateTime(),
            TypeSex = request.TypeSex,
            UpdatedOn = request.UpdateOn.ToDateTime()
        };
    }

    /// <summary>
    /// Mapper person to deleteperson 
    /// </summary>
    /// <param name="person">Person model</param>
    /// <returns>Deletepersonresponse grpc</returns>
    public static DeletePersonResponse PersonToDeletePersonResponse(DigitalBank.Core.Entities.Person person)
    {
        return new DeletePersonResponse
        {
            Id = person.Id.ToString(),
            Name = person.Name,
            BirthDate = Timestamp.FromDateTimeOffset(person.BirthDate),
            TypeSex = person.TypeSex
        };
    }

    /// <summary>
    /// Mapper person to getbyidperson
    /// </summary>
    /// <param name="person">Person model</param>
    /// <returns>Getbyidperson response grpc</returns>
    public static GetByIdPersonResponse PersonToGetByIdPersonResponse(DigitalBank.Core.Entities.Person person)
    {
        return new GetByIdPersonResponse
        {
            Id = person.Id.ToString(),
            Name = person.Name,
            BirthDate = Timestamp.FromDateTimeOffset(person.BirthDate),
            TypeSex = person.TypeSex,
            CreatedOn = Timestamp.FromDateTimeOffset(person.CreatedOn)
        };
    }

    /// <summary>
    /// Mapper Getallpersons 
    /// </summary>
    /// <param name="persons">List persons model</param>
    /// <returns>Get allperson response grpc</returns>
    public static GetAllPersonsResponse PersonToGetAllPersonsResponse(IEnumerable<DigitalBank.Core.Entities.Person> persons)
    {
        var response = new GetAllPersonsResponse();
        response.Persons.AddRange(persons.Select(x => new Person
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            BirthDate = Timestamp.FromDateTimeOffset(x.BirthDate),
            TypeSex = x.TypeSex,
            CreatedOn = Timestamp.FromDateTimeOffset(x.CreatedOn)
        }));

        return response;
    }
}

