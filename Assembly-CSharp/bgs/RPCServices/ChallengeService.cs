using System;
using bnet.protocol;
using bnet.protocol.challenge;

namespace bgs.RPCServices
{
	public class ChallengeService : ServiceDescriptor
	{
		public ChallengeService() : base("bnet.protocol.challenge.ChallengeService")
		{
			this.Methods = new MethodDescriptor[4];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.challenge.ChallengeService.ChallengePicked", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengePickedResponse>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.challenge.ChallengeService.ChallengeAnswered", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeAnsweredResponse>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.challenge.ChallengeService.ChallengeCancelled", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>));
		}

		public const uint CHALLENGED_PICKED = 1u;

		public const uint CHALLENGED_ANSWERED = 2u;

		public const uint CHALLENGED_CANCELLED = 3u;
	}
}
