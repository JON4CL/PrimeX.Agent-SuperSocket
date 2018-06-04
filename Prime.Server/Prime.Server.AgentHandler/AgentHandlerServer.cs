using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Logging;
using SuperSocket.SocketBase.Protocol;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Prime.Server
{
    public class AgentHandlerServer : AppServer<AgentHandlerSession>
    {
        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            Logger.Info("Setup()");
            return true;
        }

        protected override void OnStarted()
        {
            Logger.Info("OnStarted()");
            base.OnStarted();
        }

        protected override void OnStartup()
        {
            Logger.Info("OnStartup()");
            base.OnStartup();
        }

        protected override void OnStopped()
        {
            Logger.Info("OnStoped()");
            base.OnStopped();
        }

        protected override void OnNewSessionConnected(AgentHandlerSession session)
        {
            Logger.InfoFormat("OnNewSessionConnected() id:'{0}'", session.SessionID);
            base.OnNewSessionConnected(session);
        }

        protected override void OnSessionClosed(AgentHandlerSession session, CloseReason reason)
        {
            Logger.InfoFormat("OnSessionClosed() id:'{0}', reason:'{1}'", session.SessionID, reason);
            base.OnSessionClosed(session, reason);
        }

        protected override void OnSystemMessageReceived(string messageType, object messageData)
        {
            Logger.InfoFormat("OnSystemMessageReceived() messageType:'{0}', messageData:'{1}", messageType, messageData.ToString());
            base.OnSystemMessageReceived(messageType, messageData);
        }

        protected override AgentHandlerSession CreateAppSession(ISocketSession socketSession)
        {
            Logger.InfoFormat("CreateAppSession() socketSession:'{0}'", socketSession.SessionID);
            return base.CreateAppSession(socketSession);
        }
        
        protected override ILog CreateLogger(string loggerName)
        {
            //Logger.InfoFormat("CreateLogger() loggerName:'{0}'", loggerName);
            return base.CreateLogger(loggerName);
        }

        protected override void ExecuteCommand(AgentHandlerSession session, StringRequestInfo requestInfo)
        {
            Logger.InfoFormat("ExecuteCommand() session:'{0}', requestInfo.Key:'{1}', requestInfo.Body:'{2}'", session.SessionID, requestInfo.Key, requestInfo.Body);
            base.ExecuteCommand(session, requestInfo);
        }

        protected override X509Certificate GetCertificate(ICertificateConfig certificate)
        {
            Logger.InfoFormat("GetCertificate() certificate:'{0}'", certificate.ToString());
            return base.GetCertificate(certificate);
        }

        protected override void OnServerStatusCollected(StatusInfoCollection bootstrapStatus, StatusInfoCollection serverStatus)
        {
            Logger.InfoFormat("OnServerStatusCollected() bootstrapStatus:'{0}', serverStatus:'{1}'", bootstrapStatus.ToString(), serverStatus.ToString());
            base.OnServerStatusCollected(bootstrapStatus, serverStatus);
        }

        protected override bool RegisterSession(string sessionID, AgentHandlerSession appSession)
        {
            Logger.InfoFormat("RegisterSession() sessionID:'{0}', appSession:'{1}'", sessionID, appSession.SessionID);
            return base.RegisterSession(sessionID, appSession);
        }

        protected override bool SetupCommandLoaders(List<ICommandLoader<ICommand<AgentHandlerSession, StringRequestInfo>>> commandLoaders)
        {
            Logger.InfoFormat("SetupCommandLoaders() commandLoaders:'{0}'", commandLoaders.ToString());
            return base.SetupCommandLoaders(commandLoaders);
        }

        protected override bool SetupCommands(Dictionary<string, ICommand<AgentHandlerSession, StringRequestInfo>> discoveredCommands)
        {
            Logger.InfoFormat("SetupCommands() discoveredCommands:'{0}'", discoveredCommands.ToString());
            return base.SetupCommands(discoveredCommands);
        }

        protected override void UpdateServerStatus(StatusInfoCollection serverStatus)
        {
            Logger.InfoFormat("UpdateServerStatus() serverStatus:'{0}'", serverStatus.ToString());
            base.UpdateServerStatus(serverStatus);
        }

        protected override bool ValidateClientCertificate(AgentHandlerSession session, object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            Logger.InfoFormat("ValidateClientCertificate() session:'{0}'", session.SessionID);
            return base.ValidateClientCertificate(session, sender, certificate, chain, sslPolicyErrors);
        }
    }
}
