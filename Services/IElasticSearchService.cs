using Delivery_Report_System.Models;
using Delivery_Report_System.Models.Data;

namespace Delivery_Report_System.Services;

public interface IElasticSearchService
{
 

    Task<Delivery> GetDelivery(string id);
    Task DeleteDelivery(string id);
    Task UpdateDelivery(string id,Delivery delivery);
    Task<IEnumerable<Delivery>> SearchDelivery(string keyword);
    Task CreateDelivery(Delivery delivery);
    
}