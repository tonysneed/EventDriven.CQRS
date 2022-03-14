using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using OrderService.Domain.OrderAggregate;
using OrderService.Domain.OrderAggregate.Queries;
using OrderService.Domain.OrderAggregate.QueryHandlers;
using OrderService.Repositories;
using OrderService.Tests.Fakes;
using OrderService.Tests.Helpers;
using Xunit;

namespace OrderService.Tests.Domain.OrderAggregate.QueryHandlers;

public class GetOrdersByCustomerHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<GetOrdersByCustomerHandler>> _loggerMock;
    private readonly Mock<IOrderRepository> _repositoryMock;

    public GetOrdersByCustomerHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetOrdersByCustomerHandler>>();
        _repositoryMock = new Mock<IOrderRepository>();
        _mapper = MappingHelper.GetMapper();
    }
    
    [Fact]
    public void WhenInstantiated_ThenShouldBeOfCorrectType()
    {
        var handler = new GetOrdersByCustomerHandler(_repositoryMock.Object, _loggerMock.Object);

        Assert.NotNull(handler);
        Assert.IsType<GetOrdersByCustomerHandler>(handler);
    }

    [Fact]
    public async Task WhenRetrievingEntities_ThenAllEntitiesShouldBeReturned()
    {
        _repositoryMock.Setup(x => x.GetByCustomerAsync(It.IsAny<Guid>()))
            .ReturnsAsync(new List<Order>
            {
                _mapper.Map<Order>(Orders.Order1),                    
                _mapper.Map<Order>(Orders.Order2)
            });

        var handler = new GetOrdersByCustomerHandler(_repositoryMock.Object, _loggerMock.Object);

        var result = await handler.Handle(new GetOrdersByCustomer(Guid.Empty), default);

        Assert.Collection(result,
            c => Assert.Equal(OrderViews.Order1.Id, c.Id),
            c => Assert.Equal(OrderViews.Order2.Id, c.Id));
    }
}