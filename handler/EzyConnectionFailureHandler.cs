﻿using com.tvd12.ezyfoxserver.client.config;
using com.tvd12.ezyfoxserver.client.constant;
using com.tvd12.ezyfoxserver.client.evt;

namespace com.tvd12.ezyfoxserver.client.handler
{
	public class EzyConnectionFailureHandler : EzyAbstractEventHandler<EzyConnectionFailureEvent>
	{

		protected override sealed void process(EzyConnectionFailureEvent evt)
		{
            logger.info("connection failure, reason = " + evt.getReason());
			EzyClientConfig config = client.getConfig();
			EzyReconnectConfig reconnectConfig = config.getReconnect();
			bool should = shouldReconnect(evt);
			bool mustReconnect = reconnectConfig.isEnable() && should;
			bool reconnecting = false;
            client.setStatus(EzyConnectionStatus.FAILURE);
			if (mustReconnect)
				reconnecting = client.reconnect();
			if (!reconnecting)
			{
				control(evt);
			}
		}

		protected virtual bool shouldReconnect(EzyConnectionFailureEvent evt)
		{
			return true;
		}

		protected virtual void control(EzyConnectionFailureEvent evt)
		{
		}
	}
}
