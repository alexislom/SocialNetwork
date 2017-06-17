using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Interfaces;
using PL.Models.User;
using PL.Providers;
using System.Web.Security;
using PL.Infrastructure.Mappers;
using PL.Infrastructure;
using PagedList;

namespace PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IRoleService _roleService;
        private readonly IFriendRequestService _friendRequestService;
        private readonly IPhotoService _photoService;
        private readonly IMessageService _messageService;

        public AccountController(IUserService userService, IUserProfileService userProfileService, IRoleService roleService,
            IFriendRequestService friendRequestService, IPhotoService photoService, IMessageService messageService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
            _roleService = roleService;
            _messageService = messageService;
            _photoService = photoService;
            _friendRequestService = friendRequestService;
        }

        #region Registration, login and logoff methods

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (new CustomMembershipProvider().ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (_userService.GetOneByPredicate(u => u.UserName == viewModel.UserName) != null)
            {
                ModelState.AddModelError(string.Empty, "User with this name already registered.");
                return View(viewModel);
            }

            if (_userService.GetOneByPredicate(u => u.Email == viewModel.UserEmail) != null)
            {
                ModelState.AddModelError(string.Empty, "User with this email already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                MembershipRegistration(viewModel);
                FormsAuthentication.SetAuthCookie(viewModel.UserName, false);
                return RedirectToAction("Index", "Profile");
            }

            return View(viewModel);
        }

        [ChildActionOnly]
        public void MembershipRegistration(RegisterViewModel viewModel)
        {
            var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                .CreateUser(viewModel.UserEmail, viewModel.UserName, viewModel.UserPassword);
            if (membershipUser != null)
            {
                var profile = _userProfileService.GetOneByPredicate(p => p.NickName == viewModel.UserName);
                profile.FirstName = viewModel.FirstName;
                profile.LastName = viewModel.LastName;
                profile.Gender = viewModel.Gender;
                _userProfileService.Update(profile);
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region For admin only

        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult ChangeRole(int id, string value)
        {
            var user = _userService.GetById(id);
            user.RoleId = _roleService.GetOneByPredicate(r => r.Id.ToString() == value).Id;
            _userService.Update(user);

            return RedirectToAction("GetAllUsers");
        }

        [Authorize(Roles = "Admin, Moderator")]
        [HttpGet]
        public ActionResult GetAllUsers(int? page)
        {
            var users = _userService.GetAllByPredicate(u => u.UserName != User.Identity.Name);
            var mvcUsers = users.Select(u => u.ToMvcUser()).ToList();
            var roles = _roleService.GetAllByPredicate(r => r.Name != "Admin").Select(r => r.ToMvcRole()).ToList();

            var model = new List<UsersEditModel>();
            foreach (var item in mvcUsers)
            {
                model.Add(new UsersEditModel
                {
                    User = item,
                    Roles = from role in roles
                        select new SelectListItem
                        {
                            Text = role.Name,
                            Value = role.Id.ToString(),
                        }
                });
            }

            int pageNumber = (page ?? 1);

            ViewBag.Title = "Filter user messages";

            if (Request.IsAjaxRequest())
                return PartialView("GetAllUsers", model.ToPagedList(pageNumber, Constants.PAGESIZE));
            return View("GetAllUsers", model.ToPagedList(pageNumber, Constants.PAGESIZE));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult GetUsers(int? page)
        {
            var userProfiles = _userProfileService.GetAllByPredicate(p => p.NickName != User.Identity.Name)
                .Select(p => p.ToMvcProfile()).ToList();

            int pageNumber = (page ?? 1);

            if (Request.IsAjaxRequest())
                return PartialView("_MessageFilterProfilesViewList", userProfiles.ToPagedList(pageNumber, Constants.PAGESIZE));
            return View("_MessageFilterProfilesViewList", userProfiles.ToPagedList(pageNumber, Constants.PAGESIZE));

        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(int id)
        {
            _friendRequestService.DeleteAllUserRelationById(id);
            _messageService.DeleteAllUserMessagesById(id);

            var userToDelete = _userService.GetById(id);
            _userService.Delete(userToDelete);

            var userProfile = _userProfileService.GetById(id);
            _userProfileService.Delete(userProfile);

            var userPhoto = _photoService.GetById(id);
            _photoService.Delete(userPhoto);

            return RedirectToAction("GetAllUsers");
        }

        #endregion
    }
}