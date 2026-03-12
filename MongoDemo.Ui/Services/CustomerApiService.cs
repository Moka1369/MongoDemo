using MongoDemo.Ui.Models;

namespace MongoDemo.Ui.Services;

public  class CustomerApiService
{
    private readonly IHttpClientFactory _httpClientFaktory;
    public CustomerApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFaktory=httpClientFactory;
    }

    private HttpClient Client=>_httpClientFaktory.CreateClient("MongoApi");

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        var result=await Client.GetFromJsonAsync<List<CustomerDto>>("api/customers");
        return result??new List<CustomerDto>();
    }

    public async Task<CustomerDto?> GetByIdAsync(string id)
    {
        return await Client.GetFromJsonAsync<CustomerDto>($"api/customers/{id}");
        
    }

    public async Task<bool> CreateAsync(CreateCustomerRequest customer)
    {
          var response=await Client.PostAsJsonAsync("api/customers" , customer);
          return response.IsSuccessStatusCode;  
    }

    public async Task<bool> UpdateAsync(string id , UpdateCustomerRequest customer)
    {
        var response=await Client.PutAsJsonAsync($"api/customers/{id}" , customer);
        return response.IsSuccessStatusCode;

 
    }

    public async Task<bool> DeleteAsync(string id)
    {
        Console.WriteLine("Api1");
        var response=await Client.DeleteAsync($"api/customers/{id}");
        Console.WriteLine($"Api2 : {response.IsSuccessStatusCode}");
        return response.IsSuccessStatusCode;
    }
}