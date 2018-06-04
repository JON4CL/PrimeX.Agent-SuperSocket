using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;

namespace Prime.Server
{

    public class AgentHandlerSession : AppSession<AgentHandlerSession>
    {
        public new AgentHandlerServer AppServer
        {
            get { return (AgentHandlerServer)base.AppServer; }
        }

        public override void Close(SuperSocket.SocketBase.CloseReason reason)
        {
            Logger.InfoFormat("AppSession.Close() reason:'{0}'", reason.ToString());
            base.Close(reason);
        }

        public override void Close()
        {
            Logger.InfoFormat("AppSession.Close()");
            base.Close();
        }

        public override void Initialize(IAppServer<AgentHandlerSession, StringRequestInfo> appServer, ISocketSession socketSession)
        {
            //Logger.InfoFormat("AppSession.Initialize() appServer:'{0}', socketSession:'{1}'", appServer, socketSession);
            base.Initialize(appServer, socketSession);
        }

        public override void Send(byte[] data, int offset, int length)
        {
            Logger.InfoFormat("AppSession.Send() byte[]");
            base.Send(data, offset, length);
        }

        public override void Send(ArraySegment<byte> segment)
        {
            Logger.InfoFormat("AppSession.Send() ArraySegment<byte>");
            base.Send(segment);
        }

        public override void Send(IList<ArraySegment<byte>> segments)
        {
            Logger.InfoFormat("AppSession.Send() IList<ArraySegment>");
            base.Send(segments);
        }

        public override void Send(string message)
        {
            Logger.InfoFormat("AppSession.Send() message:'{0}'", message);
            base.Send(message);
        }

        public override void Send(string message, params object[] paramValues)
        {
            Logger.InfoFormat("AppSession.Send() message:'{0}', paramsValues", message);
            base.Send(message, paramValues);
        }

        public override bool TrySend(string message)
        {
            Logger.InfoFormat("AppSession.TrySend() message");
            return base.TrySend(message);
        }

        public override bool TrySend(byte[] data, int offset, int length)
        {
            Logger.InfoFormat("AppSession.TrySend() byte[]");
            return base.TrySend(data, offset, length);
        }

        public override bool TrySend(ArraySegment<byte> segment)
        {
            Logger.InfoFormat("AppSession.TrySend() ArraySegment");
            return base.TrySend(segment);
        }

        public override bool TrySend(IList<ArraySegment<byte>> segments)
        {
            Logger.InfoFormat("AppSession.TrySend() IList");
            return base.TrySend(segments);
        }

        protected override int GetMaxRequestLength()
        {
            Logger.InfoFormat("AppSession.GetMaxRequestLength()");
            return base.GetMaxRequestLength();
        }

        protected override void HandleUnknownRequest(StringRequestInfo requestInfo)
        {
            Logger.InfoFormat("AppSession.HandleUnknownRequest() requestInfo:'{0}'", requestInfo.Key);
            base.HandleUnknownRequest(requestInfo);
        }

        protected override void OnInit()
        {
            Logger.InfoFormat("AppSession.OnInit()");
            base.OnInit();
        }

        protected override void OnSessionClosed(SuperSocket.SocketBase.CloseReason reason)
        {
            Logger.InfoFormat("AppSession.OnSessionClosed() reason:'{0}'", reason);
            base.OnSessionClosed(reason);
        }

        protected override void OnSessionStarted()
        {
            Logger.InfoFormat("AppSession.OnSessionStarted()");
            base.OnSessionStarted();
        }

        protected override string ProcessSendingMessage(string rawMessage)
        {
            Logger.InfoFormat("AppSession.ProcessSendingMessage() rawMessage:'{0}'", rawMessage);
            return base.ProcessSendingMessage(rawMessage);
        }
    }
}
