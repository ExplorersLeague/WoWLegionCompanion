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
				if (text == null)
				{
					goto IL_1E7;
				}
				if (RPCMeterConfigParser.<>f__switch$map1 == null)
				{
					RPCMeterConfigParser.<>f__switch$map1 = new Dictionary<string, int>(11)
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
				if (!RPCMeterConfigParser.<>f__switch$map1.TryGetValue(text, out num))
				{
					goto IL_1E7;
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
				case 11:
					goto IL_1E7;
				default:
					goto IL_1E7;
				}
				continue;
				IL_1E7:
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
				if (text == null)
				{
					goto IL_CF;
				}
				if (!(text == "method"))
				{
					if (!(text == "income_per_second:"))
					{
						if (!(text == "initial_balance:"))
						{
							if (!(text == "cap_balance:"))
							{
								if (!(text == "startup_period:"))
								{
									goto IL_CF;
								}
								rpcmeterConfig.StartupPeriod = tokenizer.NextFloat();
							}
							else
							{
								rpcmeterConfig.CapBalance = tokenizer.NextUInt32();
							}
						}
						else
						{
							rpcmeterConfig.InitialBalance = tokenizer.NextUInt32();
						}
					}
					else
					{
						rpcmeterConfig.IncomePerSecond = tokenizer.NextUInt32();
					}
				}
				else
				{
					rpcmeterConfig.AddMethod(RPCMeterConfigParser.ParseMethod(tokenizer));
				}
				continue;
				IL_CF:
				tokenizer.SkipUnknownToken();
			}
			return rpcmeterConfig;
		}
	}
}
