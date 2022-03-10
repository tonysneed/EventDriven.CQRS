using AutoMapper;
using OrderService.Mapping;

namespace OrderService.Tests.Utils;

public static class BaseUtils
{
    public static IMapper GetMapper()
    {
        var config = new MapperConfiguration(
            cfg => { cfg.AddProfile(typeof(AutoMapperProfile)); });

        return config.CreateMapper();
    }
}