using System;
using System.Runtime.CompilerServices;
using bnet.protocol;
using bnet.protocol.challenge;

namespace bgs.RPCServices
{
	public class ChallengeService : ServiceDescriptor
	{
		public ChallengeService() : base("bnet.protocol.challenge.ChallengeService")
		{
			this.Methods = new MethodDescriptor[4];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.challenge.ChallengeService.ChallengePicked";
			uint i = 1u;
			if (ChallengeService.<>f__mg$cache0 == null)
			{
				ChallengeService.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengePickedResponse>);
			}
			methods[num] = new MethodDescriptor(n, i, ChallengeService.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.challenge.ChallengeService.ChallengeAnswered";
			uint i2 = 2u;
			if (ChallengeService.<>f__mg$cache1 == null)
			{
				ChallengeService.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeAnsweredResponse>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, ChallengeService.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.challenge.ChallengeService.ChallengeCancelled";
			uint i3 = 3u;
			if (ChallengeService.<>f__mg$cache2 == null)
			{
				ChallengeService.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<NoData>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, ChallengeService.<>f__mg$cache2);
		}

		public const uint CHALLENGED_PICKED = 1u;

		public const uint CHALLENGED_ANSWERED = 2u;

		public const uint CHALLENGED_CANCELLED = 3u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache1;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache2;
	}
}
