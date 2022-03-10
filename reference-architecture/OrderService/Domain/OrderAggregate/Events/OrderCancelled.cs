﻿using EventDriven.DDD.Abstractions.Events;

namespace OrderService.Domain.OrderAggregate.Events;

public record OrderCancelled(Order Entity) : DomainEvent<Order>(Entity);