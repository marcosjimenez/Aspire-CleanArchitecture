using AutoMapper;
using CleanArchitectureSample.Application.Dto.Request;
using CleanArchitectureSample.Application.Dto.Response;
using CleanArchitectureSample.Aspire.Web.Model;

namespace CleanArchitectureSample.Aspire.Web;

public class ContactApiClient(HttpClient httpClient, IMapper mapper)
{
    public async Task<ContactResponse[]> GetContactsAsync(int maxItems = 100, CancellationToken cancellationToken = default)
    {
        List<ContactResponse>? contacts = null;

        await foreach (var contact in httpClient.GetFromJsonAsAsyncEnumerable<ContactResponse>("/api/contact", cancellationToken))
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

    public async Task<ContactModel> GetContactAsync(int contactId, CancellationToken cancellationToken = default)
    {
        ContactResponse? contact = await httpClient.GetFromJsonAsync<ContactResponse>($"/api/contact/{contactId}", cancellationToken);

        return mapper.Map<ContactModel>(contact);
    }

    public async Task<ContactModel> CreateContactAsync(ContactModel contact, CancellationToken cancellationToken = default)
    {
        var dto = mapper.Map<CreateOrUpdateContactRequest>(contact);
        var result = await httpClient.PostAsJsonAsync("/api/contact", dto, cancellationToken);

        if (!result.IsSuccessStatusCode)
            throw new Exception(await result.Content.ReadAsStringAsync(cancellationToken));
        
        var retVal = await result.Content.ReadFromJsonAsync<ContactResponse>(cancellationToken) ?? null;

        return mapper.Map<ContactModel>(retVal);
    }

    public async Task<ContactModel> UpdateContactAsync(ContactModel contact, CancellationToken cancellationToken = default)
    {
        var dto = mapper.Map<CreateOrUpdateContactRequest>(contact);
        var result = await httpClient.PutAsJsonAsync($"/api/contact/{contact.Id}", dto, cancellationToken);

        if (!result.IsSuccessStatusCode)
            throw new Exception(await result.Content.ReadAsStringAsync(cancellationToken));

        var retVal = await result.Content.ReadFromJsonAsync<ContactResponse>(cancellationToken) ?? null;

        return mapper.Map<ContactModel>(retVal);
    }

    public async Task<string> DeleteContactAsync(int contactId, CancellationToken cancellationToken = default)
    {
        var result = await httpClient.DeleteAsync($"/api/contact/{contactId}", cancellationToken);

        return result.IsSuccessStatusCode ?
            string.Empty :
            result.ReasonPhrase ?? string.Empty;
    }



    #region Countries

    public async Task<CountryResponse[]> GetCountriesAsync(CancellationToken cancellationToken = default)
    {
        List<CountryResponse>? countries = null;

        await foreach (var country in httpClient.GetFromJsonAsAsyncEnumerable<CountryResponse>("/api/country", cancellationToken))
        {
            if (country is not null)
            {
                countries ??= [];
                countries.Add(country);
            }
        }

        return countries?.ToArray() ?? [];
    }

    #endregion
}