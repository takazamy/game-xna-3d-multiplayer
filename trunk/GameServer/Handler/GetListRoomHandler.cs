using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace GameServer.Handler
{
    public class GetListRoomHandler:IHandler
    {

        public GetListRoomHandler(Client clt)
            :base(clt)
        {

        }

        public override void Handler(JObject data)
        {
            
            JObject room;
            JArray roomList = new JArray();
            foreach (var item in ServerManager.roomList)
            {
                room = new JObject();
                room.Add(GameKeys.ROOMID, item.Key);
                room.Add(GameKeys.NUMBER_USERS, item.Value.clientList.Count);
                room.Add(GameKeys.MAX, item.Value.maxPlayer);
                roomList.Add(room);
            }
            JObject mesg = new JObject();
            mesg[GameCommand.COMMAND] = GameCommand.GET_LIST_ROOM;
            mesg[GameKeys.ROOM_LIST] = roomList;
            this.client.send(mesg.ToString());
        }

        public override bool Valid(JObject data)
        {
            //Kiểm tra dữ liệu hợp lệ trước khi vào Handler
            return true;
        }
    }
}
