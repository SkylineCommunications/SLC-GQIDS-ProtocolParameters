using System.Collections.Generic;

using Skyline.DataMiner.Analytics.GenericInterface;
using Skyline.DataMiner.Net.Messages;

[GQIMetaData(Name = "SLC-GQIDS-ProtocolParameters")]
public class GqiDataSource : IGQIOnInit, IGQIDataSource, IGQIInputArguments
{
	private readonly Arguments _arguments = new Arguments();
	private GQIDMS _dms;

	public OnInitOutputArgs OnInit(OnInitInputArgs args)
	{
		_dms = args.DMS;
		return new OnInitOutputArgs();
	}

	public GQIArgument[] GetInputArguments()
	{
		return _arguments.GetArguments();
	}

	public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
	{
		_arguments.ProcessArguments(args);
		return new OnArgumentsProcessedOutputArgs();
	}

	public GQIColumn[] GetColumns()
	{
		var columns = new GQIColumn[]
		{
			new GQIIntColumn("Parameter ID"),
			new GQIStringColumn("Parameter Name"),
		};

		return columns;
	}

	public GQIPage GetNextPage(GetNextPageInputArgs args)
	{
		var rows = new List<GQIRow>();

		var protocolInfo = GetProtocolInfo(_arguments.ProtocolName, _arguments.ProtocolVersion);

		foreach (var parameterInfo in protocolInfo.Parameters)
		{
			var cells = new GQICell[]
			{
				new GQICell{ Value = parameterInfo.ID },
				new GQICell{ Value = parameterInfo.Name },
			};

			rows.Add(new GQIRow(cells));
		}

		return new GQIPage(rows.ToArray())
		{
			HasNextPage = false,
		};
	}

	private GetProtocolInfoResponseMessage GetProtocolInfo(string name, string version)
	{
		var message = new GetProtocolMessage(name, version);
		var response = (GetProtocolInfoResponseMessage)_dms.SendMessage(message);

		return response;
	}

}