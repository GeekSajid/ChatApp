using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    [AllowAnonymous]
    public class MessageHandler
    {
        public MessageHandler(ChatAppContext chatContext)
        {
            _chatContext = chatContext;
        }

        public MessageHandler()
        {
        }

        private readonly ChatAppContext _chatContext;

        public void CreateNewMessage(Message item)
        {
            //if (GroupExists(item.NewsGroup))
            {
                _chatContext.Messages.Add(new Message
                {
                    clientuniqueid = item.clientuniqueid,
                    connectionid = item.connectionid,
                    sender = item.sender,
                    receiver = item.receiver,
                    date = item.date,
                    message = item.message
                });
                _chatContext.SaveChanges();
            }
            //else
            //{
            //    throw new System.Exception("group does not exist");
            //}
        }

        public IEnumerable<Message> GetAllMessages(/*string group*/)
        {
            return _chatContext.Messages.Select(z =>
                new Message
                {
                    sender = z.sender,
                    receiver = z.receiver,
                    date = z.date,
                    message = z.message
                });
        }

        public Task<List<Message>> getUserChat(Message model)
        {
            return Task.Run(() =>
            {
                List<Message> userChat = null;
                try
                {
                    //using (var _ctx = new ChatAppContext())
                    //{
                    //    userChat = (from x in _ctx.Messages
                    //                where (x.sender == model.sender && x.receiver == model.receiver) || (x.receiver == model.sender && x.sender == model.receiver)
                    //                select x).ToList();
                    //}
                    userChat = _chatContext.Messages.Where(x => (x.sender == model.sender && x.receiver == model.receiver) || (x.receiver == model.sender && x.sender == model.receiver)).ToList();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    userChat = new List<Message>();
                }

                return userChat;
            });
        }
    }
}
