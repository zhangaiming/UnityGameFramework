using System;
using System.Net;
using System.Net.Sockets;
using Framework.Utils;
using UnityEngine;

namespace Framework.Managers
{
    public enum NetState
    {
        Disconnected,
        Connecting,
        Connected
    }
    
    public class NetManager : UnitySingleton<NetManager>
    {
        /// <summary>
        /// 当前的连接状态
        /// </summary>
        public NetState NetState => netState;
        
        NetState netState;

        string ip = "127.0.0.1";

        int port = 6080;

        float retryConnectInterval      = 10;
        float checkConnectStateInterval = 5f;
        
        public void Connect()
        {
            if (netState != NetState.Disconnected) return;

            netState = NetState.Connecting;
            
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket socket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.BeginConnect(ipe, ConnectCallback, socket);
        }

        void ConnectCallback(IAsyncResult result)
        {
            Socket s = result.AsyncState as Socket;
            if (s == null)
            {
                Debug.LogError("连接服务端时出错：异步内容为空");
                return;
            }
            s.EndConnect(result);
            if (s.Connected)
            {
                netState = NetState.Connected;
                TimerManager.Instance.Schedule(checkConnectStateInterval, false, null, CheckConnectState);
            }
            else
            {
                netState = NetState.Disconnected;
                // 连接失败，尝试重连
                TimerManager.Instance.ScheduleOnce(retryConnectInterval, false, null, (id, o) => Connect());
            }
        }

        void CheckConnectState(int timerId, object param)
        {
            
        }

        public void SendMessage()
        {
            
        }

        public void AddMessageListener()
        {
            
        }
    }
}