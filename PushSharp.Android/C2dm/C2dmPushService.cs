using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PushSharp.Common;

namespace PushSharp.Android
{
	[Obsolete("Google has Deprecated C2DM, and you should now use GCM Instead.")]
	public class C2dmPushService : PushServiceBase
	{
		protected override PushChannelBase CreateChannel(PushChannelSettings channelSettings)
		{
			return new C2dmPushChannel(channelSettings as C2dmPushChannelSettings);
		}

		public override PlatformType Platform
		{
			get { return PlatformType.AndroidC2dm; }
		}
	}
}
