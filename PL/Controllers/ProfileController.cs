using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using PL.Infrastructure;
using PL.Infrastructure.Mappers;
using PL.Models.Profile;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace PL.Controllers
{
    [CustomAuthorize]
    public class ProfileController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IPhotoService _photoService;
        private readonly IFriendRequestService _friendRequestService;

        public ProfileController(IUserProfileService userProfileService, IPhotoService photoService, IFriendRequestService friendRequestService)
        {
            _userProfileService = userProfileService;
            _photoService = photoService;
            _friendRequestService = friendRequestService;
        }

        #region User page

        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var profile = _userProfileService.GetOneByPredicate(p => p.NickName == User.Identity.Name);
                var model = profile.ToFullMvcProfile();

                if (Request.IsAjaxRequest())
                    return PartialView("_Profile", model);
                return View("_Profile", model);
            }
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Search

        [HttpPost]
        public ActionResult Search(SearchUserModel model, int? page)
        {
            if (ModelState.IsValid)
            {
                var result = _userProfileService.Search(new BllUserProfile
                {
                    NickName = model.NickName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    City = model.City,
                    Gender = model.Gender
                }).Select(x => x.ToMvcProfile()).ToList();

                int pageNumber = (page ?? 1);

                ViewBag.Title = "Search Results";
                ViewBag.EmptyMessage = "No results...";
                return View("_ProfilesViewList", result.ToPagedList(pageNumber, Constants.PAGESIZE));
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Search()
        {
            if (Request.IsAjaxRequest())
                return PartialView();
            return View();
        }

        #endregion

        #region Edit

        [HttpGet]
        public ActionResult Edit()
        {
            var user = _userProfileService.GetOneByPredicate(p => p.NickName == User.Identity.Name);

            return View(user.ToEditUserProfile());
        }

        [HttpPost]
        public ActionResult Edit(ProfileEditModel model, HttpPostedFileBase file = null)
        {
            var profileToEdit = _userProfileService.GetOneByPredicate(p => p.NickName == User.Identity.Name);
            model.Id = profileToEdit.Id;
            ImageSetUp(model.Id, file);
            _userProfileService.Update(model.ToUpdatingBllProfile());
            FullProfileViewModel updatedModel = _userProfileService.GetById(model.Id).ToFullMvcProfile();
            
            if(Request.IsAjaxRequest())
                return PartialView("_Profile", updatedModel);
            return View("_Profile", updatedModel);
        }

        #endregion

        [HttpGet]
        public ActionResult ShowUser(int id)
        {
            var profile = _userProfileService.GetById(id).ToFullMvcProfile();

            var curUserProfileId = _userProfileService.GetOneByPredicate(u => u.NickName == User.Identity.Name).Id;

            if (_friendRequestService.IsFriend(curUserProfileId, id))
                ViewBag.IsFriend = true;
            else if (_friendRequestService.IsRequested(curUserProfileId, id))
                ViewBag.IsFriend = null;
            else ViewBag.IsFriend = false;

            return View("_Profile", profile);
        }

        #region Images

        public FileResult GetImage(int id)
        {
            var image = _photoService.GetById(id);

            if (image.Data != null && image.MimeType != null)
                return File(image.Data, image.MimeType);

            var path = Server.MapPath("~/Content/noavatar.png");
            var type = "image/png";

            return File(path, type);
        }

        [ChildActionOnly]
        public void ImageSetUp(int profileId, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (file.ContentType == "image/jpg" ||
                    file.ContentType == "image/png" ||
                    file.ContentType == "image/jpeg")
                {
                    var photo = new BllPhoto
                    {
                        Id = profileId,
                        Date = DateTime.Now,
                        MimeType = file.ContentType,
                        Data = new byte[file.ContentLength]
                    };

                    file.InputStream.Read(photo.Data, 0, file.ContentLength);
                    _photoService.Update(photo);
                }
            }
        }

        #endregion
    }
}