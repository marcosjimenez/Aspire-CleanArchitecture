using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dto.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Queries;

public class GetAllContactsQueryHandler(IContactRepository contactRepository, IMapper mapper, ICacheManager cacheManager)
    : IRequestHandler<GetAllContactsQuery, IEnumerable<ContactResponse>>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
          throw new ArgumentNullException(nameof(contactRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));
    private readonly ICacheManager _cacheManager = cacheManager ??
        throw new ArgumentNullException(nameof(cacheManager));

    public async Task<IEnumerable<ContactResponse>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        var retVal = await _cacheManager.GetAsync<IEnumerable<ContactResponse>>("Contacts");
        if (retVal is null)
        {
            var contacts = await _contactRepository.GetAllAsync();
            retVal = _mapper.Map<List<ContactResponse>>(contacts);
            await _cacheManager.Set("Contacts", retVal);
        }
        return retVal;
    }
}