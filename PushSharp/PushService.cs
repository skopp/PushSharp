using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushSharp.Common;

namespace PushSharp
{
	public class PushService : IDisposable
	{
		public ChannelEvents Events;

		private List<PushServiceBase> pushServices;
 
		static PushService instance = null;
		public static PushService Instance
		{
			get
			{
				if (instance == null)
					instance = new PushService();

				return instance;
			}
		}

		public PushService()
		{
			Events = new ChannelEvents();

			pushServices = (from a in AppDomain.CurrentDomain.GetAssemblies()
			                where a.FullName.Contains("PushSharp")
			                from t in a.GetTypes()
			                where !t.IsAbstract
			                      && t.BaseType == typeof (PushServiceBase)
			                select (PushServiceBase) Activator.CreateInstance(t)).ToList();
		}

		public void StartService(PlatformType platformType, PushChannelSettings channelSettings, PushServiceSettings serviceSettings = null)
		{
			var service = pushServices.FirstOrDefault(s => s.Platform == platformType);

			if (service == null)
				throw new NullReferenceException("Could not find a service for the Platform: " + platformType + ". Are you sure you have the right Assembly referenced?");

			service.Start(channelSettings, serviceSettings);
		}

		public void StopService(PlatformType platformType, bool waitForQueueToFinish = true)
		{
			var service = pushServices.FirstOrDefault(s => s.Platform == platformType);

			if (service != null)
				service.Stop(waitForQueueToFinish);
		}

		public void QueueNotification(Notification notification)
		{
			var service = pushServices.FirstOrDefault(s => s.Platform == notification.Platform);

			if (service == null)
				throw new NullReferenceException("Could not find a service for the Platform: " + notification.Platform + ". Are you sure you have the right Assembly referenced?");

			service.QueueNotification(notification);
		}

		public void StopAllServices(bool waitForQueuesToFinish = true)
		{
			Task.WaitAll(pushServices.Select(svc => Task.Factory.StartNew(() => svc.Stop(true))).ToArray());
		}

		void IDisposable.Dispose()
		{
			StopAllServices(false);
		}
	}	
}
