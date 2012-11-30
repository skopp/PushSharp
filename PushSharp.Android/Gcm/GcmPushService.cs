using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PushSharp.Common;

namespace PushSharp.Android
{
	public class GcmPushService : PushServiceBase
	{
		protected override PushChannelBase CreateChannel(PushChannelSettings channelSettings)
		{
			return new GcmPushChannel(channelSettings as GcmPushChannelSettings);
		}

		public override PlatformType Platform
		{
			get { return PlatformType.AndroidGcm; }
		}
	}
}
