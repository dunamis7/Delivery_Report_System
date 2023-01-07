using System.ComponentModel.DataAnnotations;
using Delivery_Report_System.Models.enums;

namespace Delivery_Report_System.Models.Data;

public class Delivery
{
    [EnumDataType(typeof(WarehouseBlock))]
    public string WarehouseBlock { get; set; }
    
    [EnumDataType(typeof(ModeofShipment))]
    public string ModeofShipment { get; set; }
    
    public int CustomerCareCalls { get; set; }
    
    public int CustomerRating { get; set; }
    
    public float CostofProduct { get; set; }
    
    public int PriorPurchases { get; set; }
    
    [EnumDataType(typeof(ProductImportance))]
    public string ProductImportance { get; set; }
    
    [EnumDataType(typeof(Gender))]
    public string Gender { get; set; }
    
    public float DiscountOffered { get; set; }
    
    public float WeightInGMS { get; set; }
    
    [EnumDataType(typeof(Arrived))]
    public string ArrivedOnTime { get; set; }
}
