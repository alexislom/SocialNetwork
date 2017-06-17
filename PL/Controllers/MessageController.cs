using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
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
        private readonly IUserProfileService _userProfileService;

        public MessageController(IMessageService messageService, IUserProfileService userProfileService)
        {
            _messageService = messageService;
            _userProfileService = userProfileService;
        }

        public ActionResult Dialogs()
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

            if (Request.IsAjaxRequest())
                return PartialView("_Dialogs", model);
            return View("_Dialogs", model);
        }

        [HttpGet]
        public ActionResult GetMessages(int id)
        {
            var companion = _userProfileService.GetById(id);

            var currentUser = _userProfileService.GetOneByPredicate(p => p.NickName == User.Identity.Name);

            var messages = _messageService.GetAllByPredicate(m =>
                    (m.FromUserId == currentUser.Id && m.ToUserId == companion.Id) ||
                    (m.FromUserId == companion.Id && m.ToUserId == currentUser.Id)).
                OrderByDescending(d => d.Date).Take(3).OrderBy(d => d.Date).
                Select(m => m.ToMvcMessage()).ToList();

            var model = new MessagesViewModel()
            {
                Messages = messages,
                UserId = currentUser.Id,
                InterlocutorId = companion.Id
            };

            if (Request.IsAjaxRequest())
                return PartialView("_GetMessages", model);
            return View("_GetMessages", model);
        }

        [HttpPost]
        public ActionResult SendMessage(int senderId, int receiverId, string messageText)
        {
            var message = new BllMessage()
            {
                ToUserId = receiverId,
                FromUserId = senderId,
                TextMessage = messageText,
                Date = DateTime.Now,
                UserFrom = _userProfileService.GetById(senderId),
                UserTo = _userProfileService.GetById(receiverId)
            };

            _messageService.Create(message);

            if (Request.IsAjaxRequest())
            {
                var model = message.ToMvcMessage();
                return PartialView("_Message", model);
            }

            return RedirectToAction("GetMessages", new { id = receiverId });
        }

        

        [Authorize(Roles = "Admin")]
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

        public ActionResult TestChat()
        {
            return View();
        }
    }
}