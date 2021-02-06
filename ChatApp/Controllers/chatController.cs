using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChatApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class chatController : ControllerBase
    {
        private MessageHandler _objChat;

        public chatController(MessageHandler messageHandler)
        {
            this._objChat = messageHandler;
        }


        //GET: api/chat/userChat
        [HttpGet("[action]")]
        public async Task<object> userChat([FromQuery] string param)
        {
            object result = null; object resdata = null;

            try
            {
                if (param != string.Empty)
                {
                    dynamic data = JsonConvert.DeserializeObject(param);
                    Message model = JsonConvert.DeserializeObject<Message>(data.ToString());
                    if (model != null)
                    {
                        resdata = await _objChat.getUserChat(model);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            result = new
            {
                resdata
            };

            return result;
        }
    }
}
