using System;
using System.Text;
using bgs.RPCServices;
using bnet.protocol;
using bnet.protocol.resources;

namespace bgs
{
	public class ResourcesAPI : BattleNetAPI
	{
		public ResourcesAPI(BattleNetCSharp battlenet) : base(battlenet, "ResourcesAPI")
		{
		}

		public ServiceDescriptor ResourcesService
		{
			get
			{
				return this.m_resourcesService;
			}
		}

		public override void Initialize()
		{
			base.Initialize();
			base.ApiLog.LogDebug("Initializing");
		}

		private void ResouceLookupTestCallback(ContentHandle contentHandle, object userContext)
		{
			if (contentHandle == null)
			{
				base.ApiLog.LogWarning("Lookup failed");
				return;
			}
			int num = (int)userContext;
			base.ApiLog.LogDebug("Lookup done i={0} Region={1} Usage={2} SHA256={3}", new object[]
			{
				num,
				contentHandle.Region,
				contentHandle.Usage,
				contentHandle.Sha256Digest
			});
		}

		public void LookupResource(FourCC programId, FourCC streamId, FourCC locale, ResourcesAPI.ResourceLookupCallback cb, object userContext)
		{
			ContentHandleRequest contentHandleRequest = new ContentHandleRequest();
			contentHandleRequest.SetProgramId(programId.GetValue());
			contentHandleRequest.SetStreamId(streamId.GetValue());
			contentHandleRequest.SetLocale(locale.GetValue());
			if (contentHandleRequest == null || !contentHandleRequest.IsInitialized)
			{
				base.ApiLog.LogWarning("Unable to create request for RPC call.");
				return;
			}
			RPCContext rpccontext = this.m_rpcConnection.QueueRequest(this.m_resourcesService.Id, 1u, contentHandleRequest, new RPCContextDelegate(this.GetContentHandleCallback), 0u);
			ResourcesAPIPendingState resourcesAPIPendingState = new ResourcesAPIPendingState();
			resourcesAPIPendingState.Callback = cb;
			resourcesAPIPendingState.UserContext = userContext;
			this.m_pendingLookups.Add(rpccontext.Header.Token, resourcesAPIPendingState);
			base.ApiLog.LogDebug("Lookup request sent. PID={0} StreamID={1} Locale={2}", new object[]
			{
				programId,
				streamId,
				locale
			});
		}

		private void GetContentHandleCallback(RPCContext context)
		{
			ResourcesAPIPendingState resourcesAPIPendingState = null;
			if (!this.m_pendingLookups.TryGetValue(context.Header.Token, out resourcesAPIPendingState))
			{
				base.ApiLog.LogWarning("Received unmatched lookup response");
				return;
			}
			this.m_pendingLookups.Remove(context.Header.Token);
			ContentHandle contentHandle = ContentHandle.ParseFrom(context.Payload);
			if (contentHandle == null || !contentHandle.IsInitialized)
			{
				base.ApiLog.LogWarning("Received invalid response");
				resourcesAPIPendingState.Callback(null, resourcesAPIPendingState.UserContext);
				return;
			}
			BattleNetErrors status = (BattleNetErrors)context.Header.Status;
			if (status != BattleNetErrors.ERROR_OK)
			{
				base.ApiLog.LogWarning("Battle.net Resources API C#: Failed lookup. Error={0}", new object[]
				{
					status
				});
				resourcesAPIPendingState.Callback(null, resourcesAPIPendingState.UserContext);
				return;
			}
			ContentHandle contentHandle2 = ContentHandle.FromProtocol(contentHandle);
			resourcesAPIPendingState.Callback(contentHandle2, resourcesAPIPendingState.UserContext);
		}

		public static string ByteArrayToString(byte[] ba)
		{
			StringBuilder stringBuilder = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		private ServiceDescriptor m_resourcesService = new ResourcesService();

		private Map<uint, ResourcesAPIPendingState> m_pendingLookups = new Map<uint, ResourcesAPIPendingState>();

		public delegate void ResourceLookupCallback(ContentHandle contentHandle, object userContext);
	}
}
