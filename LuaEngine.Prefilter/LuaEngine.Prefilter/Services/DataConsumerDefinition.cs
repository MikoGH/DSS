using MassTransit;
using static LuaEngine.Prefilter.Constants.AppConstants;

namespace LuaEngine.Prefilter.Services;

public class DataConsumerDefinition : ConsumerDefinition<DataConsumer>
{
    public DataConsumerDefinition(IConfiguration configuration)
    {
        var queueName = configuration.GetValue<string>(RawDataQueueNameSection);
        var prefix = configuration.GetValue<string>(PrefixSection);

        if (string.IsNullOrEmpty(queueName))
            throw new ArgumentNullException("Не задано имя очереди для исходных данных!");

        EndpointName = string.IsNullOrEmpty(prefix)
            ? EndpointName = queueName
            : EndpointName = string.Join("-", prefix, queueName);
    }
}
