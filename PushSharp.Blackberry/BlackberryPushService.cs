using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PushSharp.Common;

namespace PushSharp.Blackberry
{
	public class BlackberryPushService : PushServiceBase
	{
		protected override PushChannelBase CreateChannel(PushChannelSettings channelSettings)
		{
			return new BlackberryPushChannel(channelSettings as BlackberryPushChannelSettings);
		}

		public override PlatformType Platform
		{
			get { return PlatformType.Blackberry; }
		}
	}
}
