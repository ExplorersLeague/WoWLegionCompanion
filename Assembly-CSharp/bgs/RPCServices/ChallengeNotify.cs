using System;
using System.Runtime.CompilerServices;
using bnet.protocol.challenge;

namespace bgs.RPCServices
{
	public class ChallengeNotify : ServiceDescriptor
	{
		public ChallengeNotify() : base("bnet.protocol.challenge.ChallengeNotify")
		{
			this.Methods = new MethodDescriptor[5];
			MethodDescriptor[] methods = this.Methods;
			int num = (int)((UIntPtr)1);
			string n = "bnet.protocol.challenge.ChallengeNotify.ChallengeUser";
			uint i = 1u;
			if (ChallengeNotify.<>f__mg$cache0 == null)
			{
				ChallengeNotify.<>f__mg$cache0 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeUserRequest>);
			}
			methods[num] = new MethodDescriptor(n, i, ChallengeNotify.<>f__mg$cache0);
			MethodDescriptor[] methods2 = this.Methods;
			int num2 = (int)((UIntPtr)2);
			string n2 = "bnet.protocol.challenge.ChallengeNotify.ChallengeResult";
			uint i2 = 2u;
			if (ChallengeNotify.<>f__mg$cache1 == null)
			{
				ChallengeNotify.<>f__mg$cache1 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeResultRequest>);
			}
			methods2[num2] = new MethodDescriptor(n2, i2, ChallengeNotify.<>f__mg$cache1);
			MethodDescriptor[] methods3 = this.Methods;
			int num3 = (int)((UIntPtr)3);
			string n3 = "bnet.protocol.challenge.ChallengeNotify.OnExternalChallenge";
			uint i3 = 3u;
			if (ChallengeNotify.<>f__mg$cache2 == null)
			{
				ChallengeNotify.<>f__mg$cache2 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeExternalRequest>);
			}
			methods3[num3] = new MethodDescriptor(n3, i3, ChallengeNotify.<>f__mg$cache2);
			MethodDescriptor[] methods4 = this.Methods;
			int num4 = (int)((UIntPtr)4);
			string n4 = "bnet.protocol.challenge.ChallengeNotify.OnExternalChallengeResult";
			uint i4 = 4u;
			if (ChallengeNotify.<>f__mg$cache3 == null)
			{
				ChallengeNotify.<>f__mg$cache3 = new MethodDescriptor.ParseMethod(ProtobufUtil.ParseFromGeneric<ChallengeExternalResult>);
			}
			methods4[num4] = new MethodDescriptor(n4, i4, ChallengeNotify.<>f__mg$cache3);
		}

		public const uint CHALLENGE_USER = 1u;

		public const uint CHALLENGE_RESULT = 2u;

		public const uint CHALLENGE_EXTERNAL_REQUEST = 3u;

		public const uint CHALLENGE_EXTERNAL_REQUEST_RESULT = 4u;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache0;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache1;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache2;

		[CompilerGenerated]
		private static MethodDescriptor.ParseMethod <>f__mg$cache3;
	}
}
