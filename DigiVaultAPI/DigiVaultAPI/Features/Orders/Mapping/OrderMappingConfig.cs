using DigiVaultAPI.Features.Orders.Messages.DTOs;
using DigiVaultAPI.Models;
using Mapster;

namespace DigiVaultAPI.Features.Orders.Mapping;

public class OrderMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderItem, OrderItemDto>()
            .Map(dest => dest.Title, src => src.Course != null ? src.Course.Title : string.Empty);

        config.NewConfig<Order, OrderHistoryDto>()
            .Map(dest => dest.ItemsCount, src => src.OrderItems.Count);
    }
}