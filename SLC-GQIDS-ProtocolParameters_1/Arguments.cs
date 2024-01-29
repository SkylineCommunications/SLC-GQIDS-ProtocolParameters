using Skyline.DataMiner.Analytics.GenericInterface;

public class Arguments
{
	private readonly GQIStringArgument _protocolNameArg = new GQIStringArgument("Protocol Name") { IsRequired = true };
	private readonly GQIStringArgument _protocolVersionArg = new GQIStringArgument("Protocol Version") { IsRequired = false, DefaultValue = "Production" };

	public string ProtocolName { get; private set; }

	public string ProtocolVersion { get; private set; }

	public GQIArgument[] GetArguments()
	{
		return new GQIArgument[]
		{
			_protocolNameArg,
			_protocolVersionArg,
		};
	}

	public void ProcessArguments(OnArgumentsProcessedInputArgs args)
	{
		ProtocolName = args.GetArgumentValue(_protocolNameArg);
		ProtocolVersion = args.GetArgumentValue(_protocolVersionArg);
	}
}
