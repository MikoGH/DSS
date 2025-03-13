using MassTransit;
using static LuaEngine.LuaOrchestrator.Constants.AppConstants;

namespace LuaEngine.LuaOrchestrator.Services;

public class FilteredDataConsumerDefinition : ConsumerDefinition<FilteredDataConsumer>
{
    public FilteredDataConsumerDefinition(IConfiguration configuration)
    {
        var queueName = configuration.GetValue<string>(FilteredDataQueueNameSection);
        var prefix = configuration.GetValue<string>(PrefixSection);

        if (string.IsNullOrEmpty(queueName))
            throw new ArgumentNullException("Не задано имя очереди для исходных данных!");

        EndpointName = string.IsNullOrEmpty(prefix)
            ? EndpointName = queueName
            : EndpointName = string.Join("-", prefix, queueName);
    }
}
