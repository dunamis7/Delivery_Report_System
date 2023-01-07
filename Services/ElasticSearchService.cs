using Delivery_Report_System.Models;
using Delivery_Report_System.Models.Data;
using Nest;

namespace Delivery_Report_System.Services;

public class ElasticSearchService : IElasticSearchService
{
    private readonly ElasticClient _client;

    public ElasticSearchService()
    {
        _client = createInstance();
    }

    private ElasticClient createInstance() { 
        var settings = new ConnectionSettings()
    .DefaultMappingFor<Delivery>(x => x.IndexName("deliveries"))
    .DefaultFieldNameInferrer(p=>p);

        return new ElasticClient(settings);
    }

    public async Task<Delivery> GetDelivery(string id)
    {
        var response = await _client.GetAsync<Delivery>(id);
        var delivery = response.Source;
        return delivery;
    }

    public async Task DeleteDelivery(string id)
    {
        var getResponse = await _client.GetAsync<Delivery>(id);
        await _client.DeleteAsync<Delivery>(getResponse.Id);
    }

    public async Task UpdateDelivery(string id, Delivery delivery)
    { 
        var getResponse = await _client.GetAsync<Delivery>(id);
          await _client.UpdateAsync<Delivery>(getResponse.Id,u=>u.Doc(delivery));

    }

    public async Task<IEnumerable<Delivery>> SearchDelivery(string keyword)
    {
        var results = await _client.SearchAsync<Delivery>(
            s=>s.Query(
                q=>q.QueryString(
                    d=>d.Query('*'+keyword+'*')
                )
            ).Size(100)
        );
        
      
        return results.Documents.ToList();
    }

    public async Task CreateDelivery(Delivery delivery)
    {
        await _client.IndexDocumentAsync(delivery);
    }
}