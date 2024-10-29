using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dto.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Queries;

public class GetAllContactsQueryHandler(
    IContactRepository contactRepository,
    ICacheService cacheService,
    IMapper mapper)
    : IRequestHandler<GetAllContactsQuery, IEnumerable<ContactResponse>>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
          throw new ArgumentNullException(nameof(contactRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));
    private readonly ICacheService _cacheService = cacheService ??
        throw new ArgumentNullException(nameof(cacheService));

    public async Task<IEnumerable<ContactResponse>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        var retVal = await _cacheService.GetAsync<IEnumerable<ContactResponse>>(ApplicationConstants.Cache.ContactList);
        if (retVal is null)
        {
            var contacts = await _contactRepository.GetAllAsync();
            retVal = _mapper.Map<List<ContactResponse>>(contacts);
            await _cacheService.SetAsync(ApplicationConstants.Cache.ContactList, retVal);
        }
        return retVal;
    }
}