using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dtos.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Queries;

public class GetAllContactsQueryHandler(IContactRepository contactRepository, IMapper mapper)
    : IRequestHandler<GetAllContactsQuery, IEnumerable<ContactResponse>>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
          throw new ArgumentNullException(nameof(contactRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));

    public async Task<IEnumerable<ContactResponse>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        var response = new List<ContactResponse>();
        var contacts = await _contactRepository.GetAllAsync();
        if (contacts is not null)
        {
            response = _mapper.Map<List<ContactResponse>>(contacts);
        }
        return response;
    }
}