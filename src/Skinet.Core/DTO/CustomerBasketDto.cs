using System.ComponentModel.DataAnnotations;

namespace Skinet.Core.DTO;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; set; }

    public List<BasketItemDto> Items { get; set; }
}
