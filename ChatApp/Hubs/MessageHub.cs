using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Hubs
{
    [AllowAnonymous]
    public class MessageHub : Hub
    {
        private MessageHandler _messageHandler;
        private readonly static ConnectionMapping<string> _connections = new ConnectionMapping<string>();

        public MessageHub(MessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }
        public async Task SendMessage(Message _message)
        {
            //Receive Message
            List<string> ReceiverConnectionids = _connections.GetConnections(_message.receiver).ToList<string>();
            if (ReceiverConnectionids.Count() > 0)
            {
                //Save-Receive-Message
                try
                {
                    //_objData = new DataAccess();
                    //_message.IsPrivate = true;
                    _message.connectionid = String.Join(",", ReceiverConnectionids);
                    _messageHandler.CreateNewMessage(_message);
                    await Clients.Clients(ReceiverConnectionids).SendAsync("ReceiveMessage", _message);
                }
                catch (Exception) { }
            }
        }


        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                try
                {
                    //Add Logged User
                    var userName = httpContext.Request.Query["user"].ToString();
                    //var UserAgent = httpContext.Request.Headers["User-Agent"].FirstOrDefault().ToString();
                    var connId = Context.ConnectionId.ToString();
                    _connections.Add(userName, connId);

                    //Update Client
                    await Clients.All.SendAsync("UpdateUserList", _connections.ToJson());
                }
                catch (Exception) { }
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var httpContext = Context.GetHttpContext();
            if (httpContext != null)
            {
                //Remove Logged User
                var username = httpContext.Request.Query["user"];
                _connections.Remove(username, Context.ConnectionId);

                //Update Client
                await Clients.All.SendAsync("UpdateUserList", _connections.ToJson());
            }

            //return base.OnDisconnectedAsync(exception);
        }


        public async Task NewMessage(Message msg)
        {
            var history = _messageHandler.GetAllMessages();
            _messageHandler.CreateNewMessage(msg);
            //await Clients.Client(Context.ConnectionId).SendAsync("History", history);
            await Clients.All.SendAsync("MessageReceived", msg);
        }
    }
}
