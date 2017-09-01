using System;
using System.Collections.Generic;
using bnet.protocol.config;

namespace bgs
{
	public class RPCMeterConfigParser
	{
		public static RPCMethodConfig ParseMethod(Tokenizer tokenizer)
		{
			RPCMethodConfig rpcmethodConfig = new RPCMethodConfig();
			tokenizer.NextOpenBracket();
			for (;;)
			{
				string text = tokenizer.NextString();
				if (text == null)
				{
					break;
				}
				if (text == "}")
				{
					return rpcmethodConfig;
				}
				string text2 = text;
				if (text2 == null)
				{
					goto IL_1E6;
				}
				if (RPCMeterConfigParser.<>f__switch$mapF == null)
				{
					RPCMeterConfigParser.<>f__switch$mapF = new Dictionary<string, int>(11)
					{
						{
							"service_name:",
							0
						},
						{
							"method_name:",
							1
						},
						{
							"fixed_call_cost:",
							2
						},
						{
							"fixed_packet_size:",
							3
						},
						{
							"variable_multiplier:",
							4
						},
						{
							"multiplier:",
							5
						},
						{
							"rate_limit_count:",
							6
						},
						{
							"rate_limit_seconds:",
							7
						},
						{
							"max_packet_size:",
							8
						},
						{
							"max_encoded_size:",
							9
						},
						{
							"timeout:",
							10
						}
					};
				}
				int num;
				if (!RPCMeterConfigParser.<>f__switch$mapF.TryGetValue(text2, out num))
				{
					goto IL_1E6;
				}
				switch (num)
				{
				case 0:
					rpcmethodConfig.ServiceName = tokenizer.NextQuotedString();
					break;
				case 1:
					rpcmethodConfig.MethodName = tokenizer.NextQuotedString();
					break;
				case 2:
					rpcmethodConfig.FixedCallCost = tokenizer.NextUInt32();
					break;
				case 3:
					rpcmethodConfig.FixedPacketSize = tokenizer.NextUInt32();
					break;
				case 4:
					rpcmethodConfig.VariableMultiplier = tokenizer.NextUInt32();
					break;
				case 5:
					rpcmethodConfig.Multiplier = tokenizer.NextFloat();
					break;
				case 6:
					rpcmethodConfig.RateLimitCount = tokenizer.NextUInt32();
					break;
				case 7:
					rpcmethodConfig.RateLimitSeconds = tokenizer.NextUInt32();
					break;
				case 8:
					rpcmethodConfig.MaxPacketSize = tokenizer.NextUInt32();
					break;
				case 9:
					rpcmethodConfig.MaxEncodedSize = tokenizer.NextUInt32();
					break;
				case 10:
					rpcmethodConfig.Timeout = tokenizer.NextFloat();
					break;
				default:
					goto IL_1E6;
				}
				continue;
				IL_1E6:
				tokenizer.SkipUnknownToken();
			}
			throw new Exception("Parsing ended with unfinished RPCMethodConfig");
		}

		public static RPCMeterConfig ParseConfig(string str)
		{
			RPCMeterConfig rpcmeterConfig = new RPCMeterConfig();
			Tokenizer tokenizer = new Tokenizer(str);
			for (;;)
			{
				string text = tokenizer.NextString();
				if (text == null)
				{
					break;
				}
				string text2 = text;
				if (text2 == null)
				{
					goto IL_108;
				}
				if (RPCMeterConfigParser.<>f__switch$map10 == null)
				{
					RPCMeterConfigParser.<>f__switch$map10 = new Dictionary<string, int>(5)
					{
						{
							"method",
							0
						},
						{
							"income_per_second:",
							1
						},
						{
							"initial_balance:",
							2
						},
						{
							"cap_balance:",
							3
						},
						{
							"startup_period:",
							4
						}
					};
				}
				int num;
				if (!RPCMeterConfigParser.<>f__switch$map10.TryGetValue(text2, out num))
				{
					goto IL_108;
				}
				switch (num)
				{
				case 0:
					rpcmeterConfig.AddMethod(RPCMeterConfigParser.ParseMethod(tokenizer));
					break;
				case 1:
					rpcmeterConfig.IncomePerSecond = tokenizer.NextUInt32();
					break;
				case 2:
					rpcmeterConfig.InitialBalance = tokenizer.NextUInt32();
					break;
				case 3:
					rpcmeterConfig.CapBalance = tokenizer.NextUInt32();
					break;
				case 4:
					rpcmeterConfig.StartupPeriod = tokenizer.NextFloat();
					break;
				default:
					goto IL_108;
				}
				continue;
				IL_108:
				tokenizer.SkipUnknownToken();
			}
			return rpcmeterConfig;
		}
	}
}
