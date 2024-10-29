using AutoMapper;
using CleanArchitectureSample.Application.Contracts;
using CleanArchitectureSample.Application.Dto.Response;
using MediatR;

namespace CleanArchitectureSample.Application.Cqrs.Contacts.Queries;

public class GetContactQueryHandler(IContactRepository contactRepository, IMapper mapper)
    : IRequestHandler<GetContactQuery, ContactResponse>
{
    private readonly IContactRepository _contactRepository = contactRepository ??
          throw new ArgumentNullException(nameof(contactRepository));
    private readonly IMapper _mapper = mapper ??
        throw new ArgumentNullException(nameof(mapper));

    public async Task<ContactResponse> Handle(GetContactQuery request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetByIdWithIncludesAsync(request.Id);

        return _mapper.Map<ContactResponse>(contact);
    }
}