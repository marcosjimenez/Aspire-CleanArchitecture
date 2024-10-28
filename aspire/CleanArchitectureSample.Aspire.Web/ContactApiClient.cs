using CleanArchitectureSample.Application.Dto.Response;

namespace CleanArchitectureSample.Aspire.Web;

public class ContactApiClient(HttpClient httpClient)
{
    public async Task<ContactResponse[]> GetContactsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<ContactResponse>? contacts = null;

        await foreach (var contact in httpClient.GetFromJsonAsAsyncEnumerable<ContactResponse>("/api/contacts", cancellationToken))
        {
            if (contacts?.Count >= maxItems)
            {
                break;
            }
            if (contact is not null)
            {
                contacts ??= [];
                contacts.Add(contact);
            }
        }

        return contacts?.ToArray() ?? [];
    }
}