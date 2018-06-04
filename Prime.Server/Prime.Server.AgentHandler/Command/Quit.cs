using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prime.Server
{
    public class QUIT : StringCommandBase<AgentHandlerSession>
    {
        public override void ExecuteCommand(AgentHandlerSession session, StringRequestInfo requestInfo)
        {
            session.Send("bye");
            session.Close();
        }
    }
}
