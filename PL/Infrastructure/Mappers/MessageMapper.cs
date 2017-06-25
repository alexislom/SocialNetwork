using System;
using BLL.Interface.Entities;
using PL.Models.Message;

namespace PL.Infrastructure.Mappers
{
    public static class MessageMapper
    {
        public static BllMessage ToBllMessage(this MessageModel messageModel)
        {
            return new BllMessage
            {
                Id = messageModel.Id,
                Date = messageModel.SendTime,
                TextMessage = messageModel.Text,
                FromUserId = messageModel.SenderId,
                ToUserId = messageModel.ReceiverId
            };
        }

        public static MessageModel ToMvcMessage(this BllMessage bllMessage)
        {
            return new MessageModel
            {
                Id = bllMessage.Id,
                ReceiverId = bllMessage.ToUserId,
                SenderId = bllMessage.FromUserId,
                SendTime = bllMessage.Date,
                Text = bllMessage.TextMessage,
                Sender = bllMessage.UserFrom.ToDialogProfile(),
                Receiver = bllMessage.UserTo.ToDialogProfile()
            };
        }
    }
}