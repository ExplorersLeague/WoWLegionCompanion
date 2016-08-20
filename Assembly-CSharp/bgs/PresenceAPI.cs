using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using bgs.RPCServices;
using bgs.types;
using bnet.protocol;
using bnet.protocol.attribute;
using bnet.protocol.presence;

namespace bgs
{
	public class PresenceAPI : BattleNetAPI
	{
		public PresenceAPI(BattleNetCSharp battlenet) : base(battlenet, "Presence")
		{
			this.m_stopWatch = new Stopwatch();
		}

		public ServiceDescriptor PresenceService
		{
			get
			{
				return this.m_presenceService;
			}
		}

		public override void InitRPCListeners(RPCConnection rpcConnection)
		{
			base.InitRPCListeners(rpcConnection);
		}

		public override void Initialize()
		{
			base.Initialize();
			this.m_stopWatch.Start();
			this.m_lastPresenceSubscriptionSent = 0L;
			this.m_presenceSubscriptionBalance = 0f;
		}

		public override void OnDisconnected()
		{
			base.OnDisconnected();
			this.m_presenceSubscriptions.Clear();
			this.m_presenceUpdates.Clear();
			this.m_queuedSubscriptions.Clear();
			this.m_stopWatch.Stop();
			this.m_lastPresenceSubscriptionSent = 0L;
			this.m_presenceSubscriptionBalance = 0f;
		}

		public override void Process()
		{
			base.Process();
			this.HandleSubscriptionRequests();
		}

		public int PresenceSize()
		{
			return this.m_presenceUpdates.Count;
		}

		public void ClearPresence()
		{
			this.m_presenceUpdates.Clear();
		}

		public void GetPresence([Out] PresenceUpdate[] updates)
		{
			this.m_presenceUpdates.CopyTo(updates);
		}

		public void SetPresenceBool(uint field, bool val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.WOW.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			variant.SetBoolValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			this.PublishField(updateRequest);
		}

		public void SetPresenceInt(uint field, long val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = this.m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.WOW.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			variant.SetIntValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			this.PublishField(updateRequest);
		}

		public void SetPresenceString(uint field, string val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = this.m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.WOW.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			variant.SetStringValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			this.PublishField(updateRequest);
		}

		public void SetPresenceBlob(uint field, byte[] val)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = this.m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field2 = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.WOW.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(field);
			Variant variant = new Variant();
			if (val == null)
			{
				val = new byte[0];
			}
			variant.SetBlobValue(val);
			field2.SetKey(fieldKey);
			field2.SetValue(variant);
			fieldOperation.SetField(field2);
			updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
			updateRequest.AddFieldOperation(fieldOperation);
			this.PublishField(updateRequest);
		}

		public void PublishRichPresence([In] RichPresenceUpdate[] updates)
		{
			UpdateRequest updateRequest = new UpdateRequest();
			updateRequest.EntityId = this.m_battleNet.GameAccountId;
			FieldOperation fieldOperation = new FieldOperation();
			Field field = new Field();
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(8u);
			foreach (RichPresenceUpdate richPresenceUpdate in updates)
			{
				fieldKey.SetIndex(richPresenceUpdate.presenceFieldIndex);
				RichPresence richPresence = new RichPresence();
				richPresence.SetIndex(richPresenceUpdate.index);
				richPresence.SetProgramId(richPresenceUpdate.programId);
				richPresence.SetStreamId(richPresenceUpdate.streamId);
				Variant variant = new Variant();
				variant.SetMessageValue(ProtobufUtil.ToByteArray(richPresence));
				field.SetKey(fieldKey);
				field.SetValue(variant);
				fieldOperation.SetField(field);
				updateRequest.SetEntityId(this.m_battleNet.GameAccountId);
				updateRequest.AddFieldOperation(fieldOperation);
			}
			this.PublishField(updateRequest);
		}

		private void HandleSubscriptionRequests()
		{
			if (this.m_queuedSubscriptions.Count > 0)
			{
				long elapsedMilliseconds = this.m_stopWatch.ElapsedMilliseconds;
				this.m_presenceSubscriptionBalance = Math.Min(0f, this.m_presenceSubscriptionBalance + (float)(elapsedMilliseconds - this.m_lastPresenceSubscriptionSent) * 0.00333333341f);
				this.m_lastPresenceSubscriptionSent = elapsedMilliseconds;
				List<bnet.protocol.EntityId> list = new List<bnet.protocol.EntityId>();
				foreach (bnet.protocol.EntityId entityId in this.m_queuedSubscriptions)
				{
					if (this.m_presenceSubscriptionBalance - 1f < -100f)
					{
						break;
					}
					PresenceAPI.PresenceRefCountObject presenceRefCountObject = this.m_presenceSubscriptions[entityId];
					SubscribeRequest subscribeRequest = new SubscribeRequest();
					subscribeRequest.SetObjectId(ChannelAPI.GetNextObjectId());
					subscribeRequest.SetEntityId(entityId);
					presenceRefCountObject.objectId = subscribeRequest.ObjectId;
					this.m_battleNet.Channel.AddActiveChannel(subscribeRequest.ObjectId, new ChannelAPI.ChannelReferenceObject(entityId, ChannelAPI.ChannelType.PRESENCE_CHANNEL));
					this.m_rpcConnection.QueueRequest(this.m_presenceService.Id, 1u, subscribeRequest, new RPCContextDelegate(this.PresenceSubscribeCallback), 0u);
					this.m_presenceSubscriptionBalance -= 1f;
					list.Add(entityId);
				}
				foreach (bnet.protocol.EntityId item in list)
				{
					this.m_queuedSubscriptions.Remove(item);
				}
			}
		}

		public void PresenceSubscribe(bnet.protocol.EntityId entityId)
		{
		}

		public void PresenceUnsubscribe(bnet.protocol.EntityId entityId)
		{
			if (this.m_presenceSubscriptions.ContainsKey(entityId))
			{
				this.m_presenceSubscriptions[entityId].refCount--;
				if (this.m_presenceSubscriptions[entityId].refCount <= 0)
				{
					if (this.m_queuedSubscriptions.Contains(entityId))
					{
						this.m_queuedSubscriptions.Remove(entityId);
						return;
					}
					PresenceAPI.PresenceUnsubscribeContext @object = new PresenceAPI.PresenceUnsubscribeContext(this.m_battleNet, this.m_presenceSubscriptions[entityId].objectId);
					UnsubscribeRequest unsubscribeRequest = new UnsubscribeRequest();
					unsubscribeRequest.SetEntityId(entityId);
					this.m_rpcConnection.QueueRequest(this.m_presenceService.Id, 2u, unsubscribeRequest, new RPCContextDelegate(@object.PresenceUnsubscribeCallback), 0u);
					this.m_presenceSubscriptions.Remove(entityId);
				}
			}
		}

		public void PublishField(UpdateRequest updateRequest)
		{
			this.m_rpcConnection.QueueRequest(this.m_presenceService.Id, 3u, updateRequest, new RPCContextDelegate(this.PresenceUpdateCallback), 0u);
		}

		private void PresenceSubscribeCallback(RPCContext context)
		{
			base.CheckRPCCallback("PresenceSubscribeCallback", context);
		}

		private void PresenceUpdateCallback(RPCContext context)
		{
			base.CheckRPCCallback("PresenceUpdateCallback", context);
		}

		public void HandlePresenceUpdates(ChannelState channelState, ChannelAPI.ChannelReferenceObject channelRef)
		{
			bgs.types.EntityId entityId;
			entityId.hi = channelRef.m_channelData.m_channelId.High;
			entityId.lo = channelRef.m_channelData.m_channelId.Low;
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey.SetGroup(1u);
			fieldKey.SetField(3u);
			FieldKey fieldKey2 = fieldKey;
			List<PresenceUpdate> list = new List<PresenceUpdate>();
			foreach (FieldOperation fieldOperation in channelState.FieldOperationList)
			{
				if (fieldOperation.Operation == FieldOperation.Types.OperationType.CLEAR)
				{
					this.m_presenceCache.SetCache(entityId, fieldOperation.Field.Key, null);
				}
				else
				{
					this.m_presenceCache.SetCache(entityId, fieldOperation.Field.Key, fieldOperation.Field.Value);
				}
				PresenceUpdate presenceUpdate = default(PresenceUpdate);
				presenceUpdate.entityId = entityId;
				presenceUpdate.programId = fieldOperation.Field.Key.Program;
				presenceUpdate.groupId = fieldOperation.Field.Key.Group;
				presenceUpdate.fieldId = fieldOperation.Field.Key.Field;
				presenceUpdate.index = fieldOperation.Field.Key.Index;
				presenceUpdate.boolVal = false;
				presenceUpdate.intVal = 0L;
				presenceUpdate.stringVal = string.Empty;
				presenceUpdate.valCleared = false;
				presenceUpdate.blobVal = new byte[0];
				if (fieldOperation.Operation == FieldOperation.Types.OperationType.CLEAR)
				{
					presenceUpdate.valCleared = true;
					bool flag = fieldKey2.Program == fieldOperation.Field.Key.Program;
					bool flag2 = fieldKey2.Group == fieldOperation.Field.Key.Group;
					bool flag3 = fieldKey2.Field == fieldOperation.Field.Key.Field;
					if (flag && flag2 && flag3)
					{
						BnetEntityId entityId2 = BnetEntityId.CreateFromEntityId(presenceUpdate.entityId);
						this.m_battleNet.Friends.RemoveFriendsActiveGameAccount(entityId2, fieldOperation.Field.Key.Index);
					}
				}
				else if (fieldOperation.Field.Value.HasBoolValue)
				{
					presenceUpdate.boolVal = fieldOperation.Field.Value.BoolValue;
				}
				else if (fieldOperation.Field.Value.HasIntValue)
				{
					presenceUpdate.intVal = fieldOperation.Field.Value.IntValue;
				}
				else if (fieldOperation.Field.Value.HasStringValue)
				{
					presenceUpdate.stringVal = fieldOperation.Field.Value.StringValue;
				}
				else if (fieldOperation.Field.Value.HasFourccValue)
				{
					presenceUpdate.stringVal = new BnetProgramId(fieldOperation.Field.Value.FourccValue).ToString();
				}
				else if (fieldOperation.Field.Value.HasEntityidValue)
				{
					presenceUpdate.entityIdVal.hi = fieldOperation.Field.Value.EntityidValue.High;
					presenceUpdate.entityIdVal.lo = fieldOperation.Field.Value.EntityidValue.Low;
					bool flag4 = fieldKey2.Program == fieldOperation.Field.Key.Program;
					bool flag5 = fieldKey2.Group == fieldOperation.Field.Key.Group;
					bool flag6 = fieldKey2.Field == fieldOperation.Field.Key.Field;
					if (flag4 && flag5 && flag6)
					{
						BnetEntityId entityId3 = BnetEntityId.CreateFromEntityId(presenceUpdate.entityId);
						this.m_battleNet.Friends.AddFriendsActiveGameAccount(entityId3, fieldOperation.Field.Value.EntityidValue, fieldOperation.Field.Key.Index);
					}
				}
				else if (fieldOperation.Field.Value.HasBlobValue)
				{
					presenceUpdate.blobVal = fieldOperation.Field.Value.BlobValue;
				}
				else
				{
					if (!fieldOperation.Field.Value.HasMessageValue)
					{
						continue;
					}
					if (fieldOperation.Field.Key.Field == 8u)
					{
						this.FetchRichPresenceResource(fieldOperation.Field.Value);
						this.HandleRichPresenceUpdate(presenceUpdate, fieldOperation.Field.Key);
						continue;
					}
					continue;
				}
				list.Add(presenceUpdate);
			}
			list.Reverse();
			this.m_presenceUpdates.AddRange(list);
		}

		private void HandleRichPresenceUpdate(PresenceUpdate rpUpdate, FieldKey fieldKey)
		{
			FieldKey fieldKey2 = new FieldKey();
			fieldKey2.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey2.SetGroup(2u);
			fieldKey2.SetField(8u);
			fieldKey2.SetIndex(0UL);
			if (!fieldKey2.Equals(fieldKey))
			{
				return;
			}
			this.m_pendingRichPresenceUpdates.Add(rpUpdate);
			this.TryToResolveRichPresence();
		}

		private bool FetchRichPresenceResource(Variant presenceValue)
		{
			if (presenceValue == null)
			{
				return false;
			}
			RichPresence richPresence = RichPresence.ParseFrom(presenceValue.MessageValue);
			if (richPresence == null || !richPresence.IsInitialized)
			{
				base.ApiLog.LogError("Rich presence field from battle.net does not contain valid RichPresence message");
				return false;
			}
			if (this.m_richPresenceStringTables.ContainsKey(richPresence))
			{
				return false;
			}
			FourCC programId = new FourCC(richPresence.ProgramId);
			FourCC streamId = new FourCC(richPresence.StreamId);
			FourCC locale = new FourCC(BattleNet.Client().GetLocaleName());
			this.IncrementOutstandingRichPresenceStringFetches();
			ResourcesAPI resources = this.m_battleNet.Resources;
			resources.LookupResource(programId, streamId, locale, new ResourcesAPI.ResourceLookupCallback(this.ResouceLookupCallback), richPresence);
			return true;
		}

		private void ResouceLookupCallback(ContentHandle contentHandle, object userContext)
		{
			if (contentHandle == null)
			{
				base.ApiLog.LogWarning("BN resource look up failed unable to proceed");
				this.DecrementOutstandingRichPresenceStringFetches();
				return;
			}
			base.ApiLog.LogDebug("Lookup done Region={0} Usage={1} SHA256={2}", new object[]
			{
				contentHandle.Region,
				contentHandle.Usage,
				contentHandle.Sha256Digest
			});
			this.m_battleNet.LocalStorage.GetFile(contentHandle, new LocalStorageAPI.DownloadCompletedCallback(this.DownloadCompletedCallback), userContext);
		}

		private void DownloadCompletedCallback(byte[] data, object userContext)
		{
			if (data == null)
			{
				base.ApiLog.LogWarning("Downloading of rich presence data from depot failed!");
				this.DecrementOutstandingRichPresenceStringFetches();
				return;
			}
			base.ApiLog.LogDebug("Downloading of rich presence data completed");
			try
			{
				PresenceAPI.IndexToStringMap indexToStringMap = new PresenceAPI.IndexToStringMap();
				string @string = Encoding.ASCII.GetString(data);
				using (StringReader stringReader = new StringReader(@string))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader))
					{
						while (xmlReader.Read())
						{
							if (xmlReader.NodeType == XmlNodeType.Element)
							{
								if (xmlReader.Name == "e")
								{
									string attribute = xmlReader.GetAttribute("id");
									string value = xmlReader.ReadElementContentAsString();
									ulong key = Convert.ToUInt64(attribute, 10);
									indexToStringMap[key] = value;
								}
							}
						}
					}
				}
				PresenceAPI.RichPresenceToStringsMap richPresenceStringTables = this.m_richPresenceStringTables;
				lock (richPresenceStringTables)
				{
					RichPresence key2 = (RichPresence)userContext;
					this.m_richPresenceStringTables[key2] = indexToStringMap;
				}
			}
			catch (Exception ex)
			{
				base.ApiLog.LogWarning("Failed to parse received data into rich presence strings. Ex  = {0}", new object[]
				{
					ex.ToString()
				});
			}
			this.DecrementOutstandingRichPresenceStringFetches();
		}

		private void IncrementOutstandingRichPresenceStringFetches()
		{
			this.m_numOutstandingRichPresenceStringFetches++;
		}

		private void DecrementOutstandingRichPresenceStringFetches()
		{
			if (this.m_numOutstandingRichPresenceStringFetches <= 0)
			{
				base.ApiLog.LogWarning("Number of outstanding rich presence string fetches tracked incorrectly - decemented to negative");
				return;
			}
			this.m_numOutstandingRichPresenceStringFetches--;
			this.TryToResolveRichPresence();
		}

		private void TryToResolveRichPresence()
		{
			if (this.m_numOutstandingRichPresenceStringFetches == 0)
			{
				this.ResolveRichPresence();
				if (this.m_pendingRichPresenceUpdates.Count != 0)
				{
					base.ApiLog.LogWarning("Failed to resolve rich presence strings");
					this.m_pendingRichPresenceUpdates.Clear();
				}
			}
		}

		private void ResolveRichPresence()
		{
			if (this.m_pendingRichPresenceUpdates.Count == 0)
			{
				return;
			}
			List<PresenceUpdate> list = new List<PresenceUpdate>();
			foreach (PresenceUpdate presenceUpdate in this.m_pendingRichPresenceUpdates)
			{
				string stringVal;
				if (this.ResolveRichPresenceStrings(out stringVal, presenceUpdate.entityId, 0UL, 0))
				{
					list.Add(presenceUpdate);
					PresenceUpdate item = presenceUpdate;
					item.fieldId = 1000u;
					item.stringVal = stringVal;
					this.m_presenceUpdates.Add(item);
				}
			}
			foreach (PresenceUpdate item2 in list)
			{
				this.m_pendingRichPresenceUpdates.Remove(item2);
			}
		}

		private bool ResolveRichPresenceStrings(out string richPresenceString, bgs.types.EntityId entityId, ulong index, int recurseDepth)
		{
			richPresenceString = string.Empty;
			FieldKey fieldKey = new FieldKey();
			fieldKey.SetProgram(BnetProgramId.BNET.GetValue());
			fieldKey.SetGroup(2u);
			fieldKey.SetField(8u);
			fieldKey.SetIndex(index);
			Variant cache = this.m_presenceCache.GetCache(entityId, fieldKey);
			if (cache == null)
			{
				base.ApiLog.LogError("Expected field missing from presence cache when resolving rich presence string");
				return false;
			}
			RichPresence richPresence = RichPresence.ParseFrom(cache.MessageValue);
			if (richPresence == null || !richPresence.IsInitialized)
			{
				base.ApiLog.LogError("Rich presence field did not contain valid RichPresence message when resolving");
				return false;
			}
			if (!this.m_richPresenceStringTables.ContainsKey(richPresence))
			{
				return false;
			}
			PresenceAPI.IndexToStringMap indexToStringMap = this.m_richPresenceStringTables[richPresence];
			if (!indexToStringMap.ContainsKey((ulong)richPresence.Index))
			{
				base.ApiLog.LogWarning("Rich presence string table data is missing");
				return false;
			}
			richPresenceString = indexToStringMap[(ulong)richPresence.Index];
			if (recurseDepth < 1 && !this.SubstituteVariables(out richPresenceString, richPresenceString, entityId, recurseDepth + 1))
			{
				base.ApiLog.LogWarning("Failed to substitute rich presence variables in: {0}", new object[]
				{
					richPresenceString
				});
				return false;
			}
			return true;
		}

		private bool SubstituteVariables(out string substitutedString, string originalStr, bgs.types.EntityId entityId, int recurseDepth)
		{
			substitutedString = originalStr;
			int i = 0;
			while (i < substitutedString.Length)
			{
				i = substitutedString.IndexOf("$0x", i);
				if (i == -1)
				{
					break;
				}
				int num = i + "$0x".Length;
				int num2 = this.LastIndexOfOccurenceFromIndex(substitutedString, PresenceAPI.hexChars, num);
				int length = num2 + 1 - num;
				int length2 = num2 + 1 - i;
				string text = substitutedString.Substring(num, length);
				ulong num3 = 0UL;
				try
				{
					num3 = Convert.ToUInt64(text, 16);
				}
				catch (Exception)
				{
					base.ApiLog.LogWarning("Failed to convert {0} to ulong when substiting rich presence variables", new object[]
					{
						text
					});
					return false;
				}
				string newValue;
				if (!this.ResolveRichPresenceStrings(out newValue, entityId, num3, recurseDepth))
				{
					base.ApiLog.LogWarning("Failed resolve rich presence string for \"{0}\" when substiting variables in \"{1}\"", new object[]
					{
						num3,
						originalStr
					});
					return false;
				}
				string oldValue = substitutedString.Substring(i, length2);
				substitutedString = substitutedString.Replace(oldValue, newValue);
			}
			return true;
		}

		private int LastIndexOfOccurenceFromIndex(string str, char[] testChars, int startIndex)
		{
			int result = -1;
			char[] array = str.ToCharArray();
			for (int i = startIndex; i < array.Length; i++)
			{
				char c = array[i];
				bool flag = false;
				foreach (char c2 in PresenceAPI.hexChars)
				{
					if (c == c2)
					{
						result = i;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					break;
				}
			}
			return result;
		}

		public void RequestPresenceFields(bool isGameAccountEntityId, [In] bgs.types.EntityId entityId, [In] PresenceFieldKey[] fieldList)
		{
			QueryRequest queryRequest = new QueryRequest();
			bnet.protocol.EntityId entityId2 = new bnet.protocol.EntityId();
			entityId2.SetHigh(entityId.hi);
			entityId2.SetLow(entityId.lo);
			queryRequest.SetEntityId(entityId2);
			foreach (PresenceFieldKey presenceFieldKey in fieldList)
			{
				FieldKey fieldKey = new FieldKey();
				fieldKey.SetProgram(presenceFieldKey.programId);
				fieldKey.SetGroup(presenceFieldKey.groupId);
				fieldKey.SetField(presenceFieldKey.fieldId);
				fieldKey.SetIndex(presenceFieldKey.index);
				queryRequest.AddKey(fieldKey);
			}
			this.m_rpcConnection.QueueRequest(this.m_presenceService.Id, 4u, queryRequest, delegate(RPCContext context)
			{
				this.RequestPresenceFieldsCallback(new bgs.types.EntityId(entityId), context);
			}, 0u);
		}

		private void RequestPresenceFieldsCallback(bgs.types.EntityId entityId, RPCContext context)
		{
			if (base.CheckRPCCallback("RequestPresenceFieldsCallback", context))
			{
				QueryResponse queryResponse = QueryResponse.ParseFrom(context.Payload);
				foreach (Field field in queryResponse.FieldList)
				{
					this.m_presenceCache.SetCache(entityId, field.Key, field.Value);
					PresenceUpdate presenceUpdate = default(PresenceUpdate);
					presenceUpdate.entityId = entityId;
					presenceUpdate.programId = field.Key.Program;
					presenceUpdate.groupId = field.Key.Group;
					presenceUpdate.fieldId = field.Key.Field;
					presenceUpdate.index = field.Key.Index;
					presenceUpdate.boolVal = false;
					presenceUpdate.intVal = 0L;
					presenceUpdate.stringVal = string.Empty;
					presenceUpdate.valCleared = false;
					presenceUpdate.blobVal = new byte[0];
					if (field.Value.HasBoolValue)
					{
						presenceUpdate.boolVal = field.Value.BoolValue;
					}
					else if (field.Value.HasIntValue)
					{
						presenceUpdate.intVal = field.Value.IntValue;
					}
					else if (!field.Value.HasFloatValue)
					{
						if (field.Value.HasStringValue)
						{
							presenceUpdate.stringVal = field.Value.StringValue;
						}
						else if (field.Value.HasBlobValue)
						{
							presenceUpdate.blobVal = field.Value.BlobValue;
						}
						else if (field.Value.HasMessageValue)
						{
							if (field.Key.Field == 8u)
							{
								this.FetchRichPresenceResource(field.Value);
								this.HandleRichPresenceUpdate(presenceUpdate, field.Key);
							}
							else
							{
								presenceUpdate.blobVal = field.Value.MessageValue;
							}
						}
						else if (field.Value.HasFourccValue)
						{
							presenceUpdate.stringVal = new BnetProgramId(field.Value.FourccValue).ToString();
						}
						else if (!field.Value.HasUintValue)
						{
							if (field.Value.HasEntityidValue)
							{
								presenceUpdate.entityIdVal.hi = field.Value.EntityidValue.High;
								presenceUpdate.entityIdVal.lo = field.Value.EntityidValue.Low;
							}
							else
							{
								presenceUpdate.valCleared = true;
							}
						}
					}
					this.m_presenceUpdates.Add(presenceUpdate);
				}
			}
		}

		private const float CREDIT_LIMIT = -100f;

		private const float COST_PER_REQUEST = 1f;

		private const float PAYDOWN_RATE_PER_MS = 0.00333333341f;

		private const string variablePrefix = "$0x";

		private float m_presenceSubscriptionBalance;

		private long m_lastPresenceSubscriptionSent;

		private Stopwatch m_stopWatch;

		private HashSet<bnet.protocol.EntityId> m_queuedSubscriptions = new HashSet<bnet.protocol.EntityId>();

		private Map<bnet.protocol.EntityId, PresenceAPI.PresenceRefCountObject> m_presenceSubscriptions = new Map<bnet.protocol.EntityId, PresenceAPI.PresenceRefCountObject>();

		private List<PresenceUpdate> m_presenceUpdates = new List<PresenceUpdate>();

		private PresenceAPI.EntityIdToFieldsMap m_presenceCache = new PresenceAPI.EntityIdToFieldsMap();

		private PresenceAPI.RichPresenceToStringsMap m_richPresenceStringTables = new PresenceAPI.RichPresenceToStringsMap();

		private HashSet<PresenceUpdate> m_pendingRichPresenceUpdates = new HashSet<PresenceUpdate>();

		private int m_numOutstandingRichPresenceStringFetches;

		private ServiceDescriptor m_presenceService = new PresenceService();

		private static char[] hexChars = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};

		public class PresenceRefCountObject
		{
			public ulong objectId;

			public int refCount;
		}

		private class FieldKeyToPresenceMap : Map<FieldKey, Variant>
		{
		}

		private class EntityIdToFieldsMap : Map<bgs.types.EntityId, PresenceAPI.FieldKeyToPresenceMap>
		{
			public void SetCache(bgs.types.EntityId entity, FieldKey key, Variant value)
			{
				if (key == null)
				{
					return;
				}
				if (!base.ContainsKey(entity))
				{
					base[entity] = new PresenceAPI.FieldKeyToPresenceMap();
				}
				base[entity][key] = value;
			}

			public Variant GetCache(bgs.types.EntityId entity, FieldKey key)
			{
				if (key == null)
				{
					return null;
				}
				if (!base.ContainsKey(entity))
				{
					return null;
				}
				PresenceAPI.FieldKeyToPresenceMap fieldKeyToPresenceMap = base[entity];
				if (!fieldKeyToPresenceMap.ContainsKey(key))
				{
					return null;
				}
				return fieldKeyToPresenceMap[key];
			}
		}

		private class IndexToStringMap : Map<ulong, string>
		{
		}

		private class RichPresenceToStringsMap : Map<RichPresence, PresenceAPI.IndexToStringMap>
		{
		}

		public class PresenceUnsubscribeContext
		{
			public PresenceUnsubscribeContext(BattleNetCSharp battleNet, ulong objectId)
			{
				this.m_battleNet = battleNet;
				this.m_objectId = objectId;
			}

			public void PresenceUnsubscribeCallback(RPCContext context)
			{
				if (this.m_battleNet.Presence.CheckRPCCallback("PresenceUnsubscribeCallback", context))
				{
					this.m_battleNet.Channel.RemoveActiveChannel(this.m_objectId);
				}
			}

			private ulong m_objectId;

			private BattleNetCSharp m_battleNet;
		}
	}
}
