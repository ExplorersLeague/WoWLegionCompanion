using System;
using System.Collections.Generic;
using System.Text;
using bgs.RPCServices;
using bnet.protocol.config;

namespace bgs
{
	public class RPCConnectionMetering
	{
		public void SetConnectionMeteringData(byte[] data, ServiceCollectionHelper serviceHelper)
		{
			this.m_data = new RPCConnectionMetering.MeteringData();
			if (data == null || data.Length == 0 || serviceHelper == null)
			{
				this.m_log.LogError("Unable to retrieve Connection Metering data");
				return;
			}
			try
			{
				RPCMeterConfig rpcmeterConfig = RPCMeterConfigParser.ParseConfig(Encoding.ASCII.GetString(data));
				if (rpcmeterConfig == null || !rpcmeterConfig.IsInitialized)
				{
					this.m_data = null;
					throw new Exception("Unable to parse metering config protocol buffer.");
				}
				this.UpdateConfigStats(rpcmeterConfig);
				if (rpcmeterConfig.HasStartupPeriod)
				{
					this.m_data.StartupPeriodDuration = rpcmeterConfig.StartupPeriod;
					this.m_data.StartupPeriodEnd = (float)BattleNet.GetRealTimeSinceStartup() + rpcmeterConfig.StartupPeriod;
					this.m_log.LogDebug("StartupPeriod={0}", new object[]
					{
						rpcmeterConfig.StartupPeriod
					});
					this.m_log.LogDebug("StartupPeriodEnd={0}", new object[]
					{
						this.m_data.StartupPeriodEnd
					});
				}
				this.InitializeInternalState(rpcmeterConfig, serviceHelper);
			}
			catch (Exception ex)
			{
				this.m_data = null;
				this.m_log.LogError("EXCEPTION = {0} {1}", new object[]
				{
					ex.Message,
					ex.StackTrace
				});
			}
			if (this.m_data == null)
			{
				this.m_log.LogError("Unable to parse Connection Metering data");
			}
		}

		public void ResetStartupPeriod()
		{
			if (this.m_data != null)
			{
				this.m_data.StartupPeriodEnd = (float)BattleNet.GetRealTimeSinceStartup() + this.m_data.StartupPeriodDuration;
			}
		}

		public bool AllowRPCCall(uint serviceID, uint methodID)
		{
			if (this.m_data == null)
			{
				return true;
			}
			RPCConnectionMetering.RuntimeData runtimedData = this.GetRuntimedData(serviceID, methodID);
			if (runtimedData == null)
			{
				return true;
			}
			float num = (float)BattleNet.GetRealTimeSinceStartup();
			if ((double)this.m_data.StartupPeriodEnd > 0.0 && num < this.m_data.StartupPeriodEnd)
			{
				float num2 = this.m_data.StartupPeriodEnd - num;
				this.m_log.LogDebug("Allow (STARTUP PERIOD {0}) {1} ({2}:{3})", new object[]
				{
					num2,
					runtimedData.GetServiceAndMethodNames(),
					serviceID,
					methodID
				});
				return true;
			}
			if (runtimedData.AlwaysAllow)
			{
				this.m_log.LogDebug("Allow (ALWAYS ALLOW) {0} ({1}:{2})", new object[]
				{
					runtimedData.GetServiceAndMethodNames(),
					serviceID,
					methodID
				});
				return true;
			}
			if (runtimedData.AlwaysDeny)
			{
				this.m_log.LogDebug("Deny (ALWAYS DENY) {0} ({1}:{2})", new object[]
				{
					runtimedData.GetServiceAndMethodNames(),
					serviceID,
					methodID
				});
				return false;
			}
			if (runtimedData.FiniteCallsLeft == 4294967295u)
			{
				bool flag = runtimedData.CanCall(num);
				this.m_log.LogDebug("{0} (TRACKER) {1} ({2}:{3})", new object[]
				{
					(!flag) ? "Deny" : "Allow",
					runtimedData.GetServiceAndMethodNames(),
					serviceID,
					methodID
				});
				return flag;
			}
			if (runtimedData.FiniteCallsLeft > 0u)
			{
				this.m_log.LogDebug("Allow (FINITE CALLS LEFT {0}) {1} ({2}:{3})", new object[]
				{
					runtimedData.FiniteCallsLeft,
					runtimedData.GetServiceAndMethodNames(),
					serviceID,
					methodID
				});
				runtimedData.FiniteCallsLeft -= 1u;
				return true;
			}
			this.m_log.LogDebug("Deny (FINITE CALLS LEFT 0) {0} ({1}:{2})", new object[]
			{
				runtimedData.GetServiceAndMethodNames(),
				serviceID,
				methodID
			});
			return false;
		}

		private RPCConnectionMetering.RuntimeData GetRuntimedData(uint serviceID, uint methodID)
		{
			uint num = serviceID * 1000u + methodID;
			RPCConnectionMetering.RuntimeData runtimeData = this.m_data.GetRuntimeData(num);
			if (runtimeData == null)
			{
				runtimeData = new RPCConnectionMetering.RuntimeData();
				this.m_data.RuntimeData[num] = runtimeData;
				RPCConnectionMetering.StaticData staticData = null;
				foreach (KeyValuePair<string, RPCConnectionMetering.StaticData> keyValuePair in this.m_data.MethodDefaults)
				{
					if (keyValuePair.Value.ServiceId == serviceID && keyValuePair.Value.MethodId == methodID)
					{
						staticData = keyValuePair.Value;
						break;
					}
				}
				if (staticData == null)
				{
					foreach (KeyValuePair<string, RPCConnectionMetering.StaticData> keyValuePair2 in this.m_data.ServiceDefaults)
					{
						if (keyValuePair2.Value.ServiceId == serviceID)
						{
							staticData = keyValuePair2.Value;
							break;
						}
					}
				}
				if (staticData == null && this.m_data.GlobalDefault != null)
				{
					staticData = this.m_data.GlobalDefault;
				}
				if (staticData == null)
				{
					this.m_log.LogDebug("Always allowing ServiceId={0} MethodId={1}", new object[]
					{
						serviceID,
						methodID
					});
					runtimeData.AlwaysAllow = true;
					return runtimeData;
				}
				runtimeData.StaticData = staticData;
				if (staticData.RateLimitCount == 0u)
				{
					runtimeData.AlwaysDeny = true;
				}
				else if (staticData.RateLimitSeconds == 0u)
				{
					runtimeData.FiniteCallsLeft = staticData.RateLimitCount;
				}
				else
				{
					runtimeData.Tracker = new RPCConnectionMetering.CallTracker(staticData.RateLimitCount, staticData.RateLimitSeconds);
				}
			}
			return runtimeData;
		}

		private void InitializeInternalState(RPCMeterConfig config, ServiceCollectionHelper serviceHelper)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			int methodCount = config.MethodCount;
			for (int i = 0; i < methodCount; i++)
			{
				RPCMethodConfig rpcmethodConfig = config.Method[i];
				RPCConnectionMetering.StaticData staticData = new RPCConnectionMetering.StaticData();
				staticData.FromProtocol(rpcmethodConfig);
				if (!rpcmethodConfig.HasServiceName)
				{
					if (this.m_data.GlobalDefault == null)
					{
						this.m_data.GlobalDefault = staticData;
						this.m_log.LogDebug("Adding global default {0}", new object[]
						{
							staticData
						});
					}
					else
					{
						this.m_log.LogWarning("Static data has two defaults, ignoring additional ones.");
					}
				}
				else
				{
					string serviceName = rpcmethodConfig.ServiceName;
					ServiceDescriptor importedServiceByName = serviceHelper.GetImportedServiceByName(serviceName);
					if (importedServiceByName == null)
					{
						if (!list2.Contains(serviceName))
						{
							this.m_log.LogDebug("Ignoring not imported service {0}", new object[]
							{
								serviceName
							});
							list2.Add(serviceName);
						}
					}
					else
					{
						staticData.ServiceId = importedServiceByName.Id;
						if (rpcmethodConfig.HasMethodName)
						{
							string methodName = rpcmethodConfig.MethodName;
							string text = string.Format("{0}.{1}", serviceName, methodName);
							MethodDescriptor methodDescriptorByName = importedServiceByName.GetMethodDescriptorByName(text);
							if (methodDescriptorByName == null)
							{
								this.m_log.LogDebug("Configuration specifies an unused method {0}, ignoring.", new object[]
								{
									methodName
								});
								goto IL_231;
							}
							if (this.m_data.MethodDefaults.ContainsKey(text))
							{
								this.m_log.LogWarning("Default for method {0} already exists, ignoring extras.", new object[]
								{
									text
								});
								goto IL_231;
							}
							staticData.MethodId = methodDescriptorByName.Id;
							this.m_data.MethodDefaults[text] = staticData;
							this.m_log.LogDebug("Adding Method default {0}", new object[]
							{
								staticData
							});
						}
						else
						{
							if (this.m_data.ServiceDefaults.ContainsKey(serviceName))
							{
								this.m_log.LogWarning("Default for service {0} already exists, ignoring extras.", new object[]
								{
									serviceName
								});
								goto IL_231;
							}
							this.m_data.ServiceDefaults[serviceName] = staticData;
							this.m_log.LogDebug("Adding Service default {0}", new object[]
							{
								staticData
							});
						}
						list.Add(serviceName);
					}
				}
				IL_231:;
			}
			foreach (KeyValuePair<uint, ServiceDescriptor> keyValuePair in serviceHelper.ImportedServices)
			{
				if (!list.Contains(keyValuePair.Value.Name) && this.m_data.GlobalDefault == null)
				{
					this.m_log.LogDebug("Configuration for service {0} was not found and will not be metered.", new object[]
					{
						keyValuePair.Value.Name
					});
				}
			}
		}

		private void UpdateMethodStats(RPCMethodConfig method)
		{
			this.m_data.Stats.MethodCount += 1u;
			if (method.HasServiceName)
			{
				this.m_data.Stats.ServiceNameCount += 1u;
			}
			if (method.HasMethodName)
			{
				this.m_data.Stats.MethodNameCount += 1u;
			}
			if (method.HasFixedCallCost)
			{
				this.m_data.Stats.FixedCalledCostCount += 1u;
			}
			if (method.HasFixedPacketSize)
			{
				this.m_data.Stats.FixedPacketSizeCount += 1u;
			}
			if (method.HasVariableMultiplier)
			{
				this.m_data.Stats.VariableMultiplierCount += 1u;
			}
			if (method.HasMultiplier)
			{
				this.m_data.Stats.MultiplierCount += 1u;
			}
			if (method.HasRateLimitCount)
			{
				this.m_data.Stats.RateLimitCountCount += 1u;
				this.m_data.Stats.AggregatedRateLimitCountCount += method.RateLimitCount;
			}
			if (method.HasRateLimitSeconds)
			{
				this.m_data.Stats.RateLimitSecondsCount += 1u;
			}
			if (method.HasMaxPacketSize)
			{
				this.m_data.Stats.MaxPacketSizeCount += 1u;
			}
			if (method.HasMaxEncodedSize)
			{
				this.m_data.Stats.MaxEncodedSizeCount += 1u;
			}
			if (method.HasTimeout)
			{
				this.m_data.Stats.TimeoutCount += 1u;
			}
		}

		private bool UpdateConfigStats(RPCMeterConfig config)
		{
			int methodCount = config.MethodCount;
			for (int i = 0; i < methodCount; i++)
			{
				RPCMethodConfig method = config.Method[i];
				this.UpdateMethodStats(method);
			}
			RPCConnectionMetering.Stats stats = this.m_data.Stats;
			this.m_log.LogDebug("Config Stats:");
			this.m_log.LogDebug("  MethodCount={0}", new object[]
			{
				stats.MethodCount
			});
			this.m_log.LogDebug("  ServiceNameCount={0}", new object[]
			{
				stats.ServiceNameCount
			});
			this.m_log.LogDebug("  MethodNameCount={0}", new object[]
			{
				stats.MethodNameCount
			});
			this.m_log.LogDebug("  FixedCalledCostCount={0}", new object[]
			{
				stats.FixedCalledCostCount
			});
			this.m_log.LogDebug("  FixedPacketSizeCount={0}", new object[]
			{
				stats.FixedPacketSizeCount
			});
			this.m_log.LogDebug("  VariableMultiplierCount={0}", new object[]
			{
				stats.VariableMultiplierCount
			});
			this.m_log.LogDebug("  MultiplierCount={0}", new object[]
			{
				stats.MultiplierCount
			});
			this.m_log.LogDebug("  RateLimitCountCount={0}", new object[]
			{
				stats.RateLimitCountCount
			});
			this.m_log.LogDebug("  RateLimitSecondsCount={0}", new object[]
			{
				stats.RateLimitSecondsCount
			});
			this.m_log.LogDebug("  MaxPacketSizeCount={0}", new object[]
			{
				stats.MaxPacketSizeCount
			});
			this.m_log.LogDebug("  MaxEncodedSizeCount={0}", new object[]
			{
				stats.MaxEncodedSizeCount
			});
			this.m_log.LogDebug("  TimeoutCount={0}", new object[]
			{
				stats.TimeoutCount
			});
			this.m_log.LogDebug("  AggregatedRateLimitCountCount={0}", new object[]
			{
				stats.AggregatedRateLimitCountCount
			});
			return true;
		}

		private BattleNetLogSource m_log = new BattleNetLogSource("ConnectionMetering");

		private RPCConnectionMetering.MeteringData m_data;

		private class StaticData
		{
			public string ServiceName { get; set; }

			public string MethodName { get; set; }

			public uint FixedCallCost { get; set; }

			public uint RateLimitCount { get; set; }

			public uint RateLimitSeconds { get; set; }

			public uint ServiceId
			{
				get
				{
					return this.m_serviceId;
				}
				set
				{
					this.m_serviceId = value;
				}
			}

			public uint MethodId
			{
				get
				{
					return this.m_methodId;
				}
				set
				{
					this.m_methodId = value;
				}
			}

			public void FromProtocol(RPCMethodConfig method)
			{
				if (method.HasServiceName)
				{
					this.ServiceName = method.ServiceName;
				}
				if (method.HasMethodName)
				{
					this.MethodName = method.MethodName;
				}
				if (method.HasFixedCallCost)
				{
					this.FixedCallCost = method.FixedCallCost;
				}
				if (method.HasRateLimitCount)
				{
					this.RateLimitCount = method.RateLimitCount;
				}
				if (method.HasRateLimitSeconds)
				{
					this.RateLimitSeconds = method.RateLimitSeconds;
				}
			}

			public override string ToString()
			{
				string text = (!string.IsNullOrEmpty(this.ServiceName)) ? this.ServiceName : "<null>";
				string text2 = (!string.IsNullOrEmpty(this.MethodName)) ? this.MethodName : "<null>";
				return string.Format("ServiceName={0} MethodName={1} RateLimitCount={2} RateLimitSeconds={3} FixedCallCost={4}", new object[]
				{
					text,
					text2,
					this.RateLimitCount,
					this.RateLimitSeconds,
					this.FixedCallCost
				});
			}

			private uint m_serviceId = uint.MaxValue;

			private uint m_methodId = uint.MaxValue;
		}

		private class Stats
		{
			public uint MethodCount { get; set; }

			public uint ServiceNameCount { get; set; }

			public uint MethodNameCount { get; set; }

			public uint FixedCalledCostCount { get; set; }

			public uint FixedPacketSizeCount { get; set; }

			public uint VariableMultiplierCount { get; set; }

			public uint MultiplierCount { get; set; }

			public uint RateLimitCountCount { get; set; }

			public uint RateLimitSecondsCount { get; set; }

			public uint MaxPacketSizeCount { get; set; }

			public uint MaxEncodedSizeCount { get; set; }

			public uint TimeoutCount { get; set; }

			public uint AggregatedRateLimitCountCount { get; set; }
		}

		private class CallTracker
		{
			public CallTracker(uint maxCalls, uint timePeriodInSeconds)
			{
				if (maxCalls == 0u || timePeriodInSeconds == 0u)
				{
					return;
				}
				this.m_calls = new float[maxCalls];
				this.m_numberOfSeconds = timePeriodInSeconds;
			}

			public bool CanCall(float now)
			{
				if (this.m_calls == null || this.m_calls.Length == 0)
				{
					return false;
				}
				if (this.m_callIndex < this.m_calls.Length)
				{
					this.m_calls[this.m_callIndex++] = now;
					return true;
				}
				float num = now - this.m_calls[0];
				if (num <= this.m_numberOfSeconds)
				{
					return false;
				}
				if (this.m_calls.Length == 1)
				{
					this.m_calls[0] = now;
					this.m_callIndex = 1;
					return true;
				}
				int num2 = 0;
				while (num2 + 1 < this.m_calls.Length && now - this.m_calls[num2 + 1] > this.m_numberOfSeconds)
				{
					num2++;
				}
				int num3 = this.m_calls.Length - (num2 + 1);
				Array.Copy(this.m_calls, num2 + 1, this.m_calls, 0, num3);
				this.m_callIndex = num3;
				this.m_calls[this.m_callIndex++] = now;
				return true;
			}

			private float[] m_calls;

			private int m_callIndex;

			private float m_numberOfSeconds;
		}

		private class RuntimeData
		{
			public bool AlwaysAllow { get; set; }

			public bool AlwaysDeny { get; set; }

			public RPCConnectionMetering.StaticData StaticData { get; set; }

			public uint FiniteCallsLeft
			{
				get
				{
					return this.m_finiteCallsLeft;
				}
				set
				{
					this.m_finiteCallsLeft = value;
				}
			}

			public RPCConnectionMetering.CallTracker Tracker
			{
				get
				{
					return this.m_callTracker;
				}
				set
				{
					this.m_callTracker = value;
				}
			}

			public bool CanCall(float now)
			{
				return this.m_callTracker == null || this.m_callTracker.CanCall(now);
			}

			public string GetServiceAndMethodNames()
			{
				string arg = (this.StaticData == null || this.StaticData.ServiceName == null) ? "<null>" : this.StaticData.ServiceName;
				string arg2 = (this.StaticData == null || this.StaticData.MethodName == null) ? "<null>" : this.StaticData.MethodName;
				return string.Format("{0}.{1}", arg, arg2);
			}

			private uint m_finiteCallsLeft = uint.MaxValue;

			private RPCConnectionMetering.CallTracker m_callTracker;
		}

		private class MeteringData
		{
			public RPCConnectionMetering.Stats Stats
			{
				get
				{
					return this.m_staticDataStats;
				}
			}

			public RPCConnectionMetering.StaticData GlobalDefault
			{
				get
				{
					return this.m_globalDefault;
				}
				set
				{
					this.m_globalDefault = value;
				}
			}

			public Dictionary<string, RPCConnectionMetering.StaticData> ServiceDefaults
			{
				get
				{
					return this.m_serviceDefaults;
				}
			}

			public Dictionary<string, RPCConnectionMetering.StaticData> MethodDefaults
			{
				get
				{
					return this.m_methodDefaults;
				}
			}

			public Dictionary<uint, RPCConnectionMetering.RuntimeData> RuntimeData
			{
				get
				{
					return this.m_runtimeData;
				}
			}

			public RPCConnectionMetering.RuntimeData GetRuntimeData(uint id)
			{
				RPCConnectionMetering.RuntimeData result;
				if (this.m_runtimeData.TryGetValue(id, out result))
				{
					return result;
				}
				return null;
			}

			public float StartupPeriodEnd { get; set; }

			public float StartupPeriodDuration { get; set; }

			private RPCConnectionMetering.Stats m_staticDataStats = new RPCConnectionMetering.Stats();

			private RPCConnectionMetering.StaticData m_globalDefault;

			private Dictionary<string, RPCConnectionMetering.StaticData> m_serviceDefaults = new Dictionary<string, RPCConnectionMetering.StaticData>();

			private Dictionary<string, RPCConnectionMetering.StaticData> m_methodDefaults = new Dictionary<string, RPCConnectionMetering.StaticData>();

			private Dictionary<uint, RPCConnectionMetering.RuntimeData> m_runtimeData = new Dictionary<uint, RPCConnectionMetering.RuntimeData>();
		}
	}
}
