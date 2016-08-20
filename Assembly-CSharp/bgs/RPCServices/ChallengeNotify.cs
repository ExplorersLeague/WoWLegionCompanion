using System;
using bnet.protocol.challenge;

namespace bgs.RPCServices
{
	public class ChallengeNotify : ServiceDescriptor
	{
		public ChallengeNotify() : base("bnet.protocol.challenge.ChallengeNotify")
		{
			this.Methods = new MethodDescriptor[5];
			this.Methods[(int)((UIntPtr)1)] = new MethodDescriptor("bnet.protocol.challenge.ChallengeNotify.ChallengeUser", 1u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeUserRequest>));
			this.Methods[(int)((UIntPtr)2)] = new MethodDescriptor("bnet.protocol.challenge.ChallengeNotify.ChallengeResult", 2u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeResultRequest>));
			this.Methods[(int)((UIntPtr)3)] = new MethodDescriptor("bnet.protocol.challenge.ChallengeNotify.OnExternalChallenge", 3u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeExternalRequest>));
			this.Methods[(int)((UIntPtr)4)] = new MethodDescriptor("bnet.protocol.challenge.ChallengeNotify.OnExternalChallengeResult", 4u, new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeExternalResult>));
		}

		public const uint CHALLENGE_USER = 1u;

		public const uint CHALLENGE_RESULT = 2u;

		public const uint CHALLENGE_EXTERNAL_REQUEST = 3u;

		public const uint CHALLENGE_EXTERNAL_REQUEST_RESULT = 4u;
	}
}
