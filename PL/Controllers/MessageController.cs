using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using Microsoft.AspNet.SignalR;
using PagedList;
using PL.Hubs;
using PL.Infrastructure;
using PL.Infrastructure.Mappers;
using PL.Models.Message;
using PL.Models.Profile;

namespace PL.Controllers
{
    [CustomAuthorize]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;

        public MessageController(IMessageService messageService, IUserService userService,IUserProfileService userProfileService)
        {
            _messageService = messageService;
            _userService = userService;
            _userProfileService = userProfileService;
        }

        #region 

        public ActionResult Dialogs(int? page)
        {
            var userId = _userProfileService.GetOneByPredicate(p => p.NickName == User.Identity.Name).Id;
            var lastMessages = _messageService.GetAllChatsWith(userId).Select(m => m.ToMvcMessage()).ToList();
            var model = new List<DialogViewModel>();
            foreach (var item in lastMessages)
            {
                var interlocutorId = (int)(item.SenderId == userId ? item.ReceiverId : item.SenderId);
                var interlocutorProfile = _userProfileService.GetById(interlocutorId).ToDialogProfile();
                model.Add(new DialogViewModel { InterLocutor = interlocutorProfile, LastMessage = item });
            }

            int pageNumber = (page ?? 1);

            if (Request.IsAjaxRequest())
                return PartialView("_Dialogs", model.ToPagedList(pageNumber, Constants.PAGESIZE));
            return View("_Dialogs", model.ToPagedList(pageNumber, Constants.PAGESIZE));
        }

        #endregion

        [HttpGet]
        public ActionResult GetMessages(int id)
        {
            //var companion = _userProfileService.GetById(id);
            //var currentUser = _userProfileService.GetOneByPredicate(p => p.NickName == User.Identity.Name);

            var companion = _userService.GetById(id);

            var currentUser = _userService.GetOneByPredicate(p => p.UserName == User.Identity.Name);

            var messages = _messageService.GetAllByPredicate(m =>
                    (m.FromUserId == currentUser.Id && m.ToUserId == companion.Id) ||
                    (m.FromUserId == companion.Id && m.ToUserId == currentUser.Id)).
                OrderByDescending(d => d.Date).OrderBy(d => d.Date).
                Select(m => m.ToMvcMessage()).ToList();

            var model = new MessagesViewModel()
            {
                Messages = messages,
                FromUser = currentUser.ToMvcUser(),
                ToUser = companion.ToMvcUser()
            };

            if (Request.IsAjaxRequest())
                return PartialView("_GetMessages", model);
            return View("_GetMessages", model);
        }

        #region Sending message

        [HttpPost]
        public ActionResult SendMessage(int senderId, int receiverId, string messageText)
        {
            var fromUser = _userProfileService.GetById(senderId);
            var toUser = _userProfileService.GetById(receiverId);

            var message = new BllMessage()
            {
                FromUserId = senderId,
                ToUserId = receiverId,
                TextMessage = messageText,
                Date = DateTime.Now,
                UserFrom = _userProfileService.GetById(senderId),
                UserTo = _userProfileService.GetById(receiverId)
            };

            _messageService.Create(message);

            var conversationHub = GlobalHost.ConnectionManager.GetHubContext<ConversationHub>();
            conversationHub.Clients.Group(toUser.NickName).sendMessage(fromUser.FirstName, fromUser.PhotoId, message.Date, message.TextMessage);

            if (Request.IsAjaxRequest())
            {
                var model = message.ToMvcMessage();
                return PartialView("_Message", model);
            }
            return RedirectToAction("GetMessages", new { id = receiverId });
            //return RedirectToAction("GetMessages", new { id = receiverId });
        }

        //[HttpGet]
        //public ActionResult SendMessage(int id = -1)
        //{
        //    var toUser = _userProfileService.GetById(id);

        //    var fromUser = _userProfileService.GetOneByPredicate(p => p.NickName == User.Identity.Name);

        //    var messagesViewModel = new MessagesViewModel
        //    {
        //        //Messages = _messageService.GetAllChatsWith(toUser.Id)
        //        //FromUser = fromUser.ToUserViewModel(),
        //        //ToUser = toUser.ToUserViewModel()
        //    };

        //    return View(messagesViewModel);
        //}

        #endregion

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        public ActionResult GetUserMessages(int id)
        {
            var messages = _messageService.GetAllByPredicate(m => m.FromUserId == id).Select(m => m.ToMvcMessage()).ToList();
            return View(messages);
        }

        public ActionResult BlockMessage(int id)
        {
            var message = _messageService.GetById(id);
            message.TextMessage = "Message's text blocked by " + User.Identity.Name;
            _messageService.Update(message);
            return RedirectToAction("GetUserMessages", new { id = message.FromUserId });
        }
    }
}