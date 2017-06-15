using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Interfaces;
using BLL.Interface.Entities;
using PL.Infrastructure;
using PL.Infrastructure.Mappers;
using PL.Models.Profile;

namespace PL.Controllers
{
    [CustomAuthorize]
    public class FriendController : Controller
    {
        private readonly IFriendRequestService _friendRequestService;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;

        public FriendController(IFriendRequestService friendRequestService, IUserService userService, IUserProfileService userProfileService)
        {
            _friendRequestService = friendRequestService;
            _userService = userService;
            _userProfileService = userProfileService;
        }

        public ActionResult AddToFriend(int id)
        {
            var userId = _userService.GetOneByPredicate(u => u.UserName == User.Identity.Name).Id;

            if (!(_friendRequestService.IsFriend(userId, id) || _friendRequestService.IsRequested(userId, id)))
            {
                _friendRequestService.AddFriend(userId, id);
            }

            ViewBag.IsFriend = true;

            return RedirectToAction("ShowUser", "Profile", new { id = id });
        }
        public ActionResult FriendRequests()
        {
            var curUserId = _userProfileService.GetOneByPredicate(u => u.NickName == User.Identity.Name).Id;

            var requests = _friendRequestService.GetAllByPredicate(r => (r.IsConfirmed == false && r.UserToId == curUserId));

            var requestsModelList = new List<ProfileViewModel>();

            foreach (var item in requests)
                requestsModelList.Add(_userService.GetById((int)item.UserFromId).UserProfile.ToMvcProfile());
            if (Request.IsAjaxRequest())
                return PartialView("_RequestsList", requestsModelList);
            return View("_RequestsList", requestsModelList);
        }

        public ActionResult Friends()
        {
            var curUserId = _userProfileService.GetOneByPredicate(u => u.NickName == User.Identity.Name).Id;

            var friends = _friendRequestService.GetAllByPredicate(u =>
                (u.UserFromId == curUserId && u.IsConfirmed == true) |
                (u.UserToId == curUserId && u.IsConfirmed == true)).ToList();

            var profileList = new List<ProfileViewModel>();

            foreach (var item in friends)
            {
                var friendId = (int)(item.UserFromId == curUserId ? item.UserToId : item.UserFromId);
                profileList.Add(_userProfileService.GetById(friendId).ToMvcProfile());
            }
            ViewBag.Title = "Friends";
            ViewBag.EmptyMessage = "You don't have friends...Use search to find them!!!";

            if (Request.IsAjaxRequest())
                return PartialView("_ProfilesViewList", profileList);

            return View("_ProfilesViewList", profileList);
        }

        public ActionResult ConfirmFriend(string username)
        {
            var userToConfirmId = _userService.GetOneByPredicate(u => u.UserName == username).Id;

            var currentUser = _userService.GetOneByPredicate(u => u.UserName == User.Identity.Name);

            var request = _friendRequestService.GetOneByPredicate(r => r.UserFromId == userToConfirmId && r.UserToId == currentUser.Id);

            request.IsConfirmed = true;

            _friendRequestService.Update(request);

            return RedirectToAction("Index", "Profile");
        }

        public ActionResult RemoveFriend(int id)
        {
            var currentUser = _userProfileService.GetOneByPredicate(f => f.NickName == User.Identity.Name);

            var friendship = _friendRequestService.GetOneByPredicate(f =>
                (f.UserFromId == id && f.UserToId == currentUser.Id) ||
                (f.UserFromId == currentUser.Id && f.UserToId == id));

            _friendRequestService.Delete(friendship);

            ViewBag.IsFriend = false;

            return RedirectToAction("ShowUser", "Profile", new { id = id });
        }
    }
}