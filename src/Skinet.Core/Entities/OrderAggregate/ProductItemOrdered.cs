namespace Skinet.Core.Entities.OrderAggregate;

public class ProductItemOrdered
{
    public ProductItemOrdered()
    {
    }
    
    public ProductItemOrdered(int prodItemId, string prodName, string picUrl)
    {
        ProductItemId = prodItemId;
        ProductName = prodName;
        PictureUrl = picUrl;
    }
    
    public int ProductItemId { get; set; }

    public string ProductName { get; set; }

    public string PictureUrl { get; set; }
}
