namespace DigitalBank.Web.Mappers;

public static class MapperPersonGrpc
{
    public static CreatePersonRequest PersonToCreatePersonRequest(Data.Person person)
    {
        return new CreatePersonRequest
        {
            Name = person.Name,
            BirthDate = Timestamp.FromDateTimeOffset(person.BirthDate),
            TypeSex = person.TypeSex,
            CreatedOn = Timestamp.FromDateTimeOffset(person.CreatedOn)
        };
    }

    public static Data.Person CreatePersonResponseToPerson(CreatePersonResponse response)
    {
        return new Data.Person
        {
            Id = Guid.Parse(response.Id),
            Name = response.Name,
            BirthDate = response.BirthDate.ToDateTime(),
            TypeSex = response.TypeSex,
            CreatedOn = response.CreatedOn.ToDateTime()
        };
    }

    public static UpdatePersonRequest PersonToUpdatePersonRequest(Data.Person person)
    {
        return new UpdatePersonRequest
        {
            Id = person.Id.ToString(),
            Name = person.Name,
            BirthDate = Timestamp.FromDateTimeOffset(person.BirthDate),
            TypeSex = person.TypeSex,
            UpdateOn = Timestamp.FromDateTimeOffset(person.UpdatedOn)
        };
    }

    public static Data.Person UpdatePersonResponseToPerson(UpdatePersonResponse response)
    {
        return new Data.Person
        {
            Id = Guid.Parse(response.Id),
            Name = response.Name,
            BirthDate = response.BirthDate.ToDateTime(),
            TypeSex = response.TypeSex,
            UpdatedOn = response.UpdateOn.ToDateTime()
        };
    }

    public static Data.Person DeletePersonResponseToPerson(DeletePersonResponse response)
    {
        return new Data.Person
        {
            Id = Guid.Parse(response.Id),
            Name = response.Name,
            BirthDate = response.BirthDate.ToDateTime(),
            TypeSex = response.TypeSex
        };
    }

    public static Data.Person GetByIdPersonResponseToPerson(GetByIdPersonResponse response)
    {
        return new Data.Person
        {
            Id = Guid.Parse(response.Id),
            Name = response.Name,
            BirthDate = response.BirthDate.ToDateTime(),
            TypeSex = response.TypeSex,
            CreatedOn = response.CreatedOn.ToDateTime()
        };
    }

    public static IEnumerable<Data.Person> GetAllPersonsResponseToPersons(GetAllPersonsResponse response)
    {
        try
        {
            IEnumerable<Data.Person> persons = response.Persons.Select(x => new Data.Person
            {
                Id = Guid.Parse(x.Id),
                Name = x.Name,
                BirthDate = x.BirthDate.ToDateTime(),
                TypeSex = x.TypeSex,
                CreatedOn = x.CreatedOn.ToDateTime()
            });

            return persons;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(ex.Message, ex);
        }
       
    }
}

