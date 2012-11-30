using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushSharp.Common;

namespace PushSharp.Windows
{
	public class WindowsPushService : PushServiceBase
	{
		protected override PushChannelBase CreateChannel(Common.PushChannelSettings channelSettings)
		{
			return new WindowsPushChannel(channelSettings as WindowsPushChannelSettings);
		}

		public override PlatformType Platform
		{
			get { return PlatformType.Windows; }
		}
	}
}
