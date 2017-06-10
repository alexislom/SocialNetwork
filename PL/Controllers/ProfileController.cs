using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using PL.Infrastructure;
using PL.Infrastructure.Mappers;
using PL.Models;
using PL.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    [CustomAuthorize]
    public class ProfileController : Controller
    {
        private readonly IUserProfileService profileService;
        private readonly IPhotoService photoService;
        private readonly IFriendRequestService friendshipService;

        public ProfileController(IUserProfileService profileService, IPhotoService photoService, IFriendRequestService friendshipService)
        {
            this.photoService = photoService;
            this.profileService = profileService;
            this.friendshipService = friendshipService;
        }

        [HttpPost]
        public ActionResult Search(SearchModel model)
        {
            if (ModelState.IsValid)
            {
                var result = profileService.Search(new BllUserProfile()
                {
                    NickName = model.NickName,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    City = model.City,
                    Gender = model.Gender
                }).Select(p => p.ToMvcProfile()).ToList();
                ViewBag.Title = "Search Results";
                ViewBag.EmptyMessage = "No results...";
                return View("_ProfilesViewList", result);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Search()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var profile = profileService.GetOneByPredicate(p => p.NickName == User.Identity.Name);
                var model = profile.ToFullMvcProfile();
                if (Request.IsAjaxRequest())
                    return PartialView("_Profile", model);
                return View("_Profile", model);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = profileService.GetById(id);
            
            return View(user.ToEditUserProfile());
        }

        [HttpPost]
        public String Edit(ProfileEditModel model, HttpPostedFileBase file = null)
        {
            var profileToEdit = profileService.GetOneByPredicate(p => p.NickName == User.Identity.Name);

            model.Id = profileToEdit.Id;

            ImageSetUp(model.Id, file);

            profileService.Update(model.ToUpdatingBllProfile());

            return "Yippekiyay mfk";
            
        }

        public FileResult GetImage(int id)
        {
            var image = photoService.GetById(id);

            if (image.Data != null && image.MimeType != null)
                return File(image.Data, image.MimeType);

            var path = Server.MapPath("~/Content/noavatar.png");
            var type = "image/png";

            return File(path, type);
        }

        public ActionResult ShowUser(int id)
        {
            var profile = profileService.GetById(id).ToFullMvcProfile();

            var curUserProfileId = profileService.GetOneByPredicate(u => u.NickName == User.Identity.Name).Id;

            if (friendshipService.IsFriend(curUserProfileId, id))
                ViewBag.IsFriend = true;
            else if (friendshipService.IsRequested(curUserProfileId, id))
                ViewBag.IsFriend = null;
            else ViewBag.IsFriend = false;

            return View("_Profile", profile);
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
                    var photo = new BllPhoto()
                    {
                        Id = profileId,
                        Date = DateTime.Now,
                        MimeType = file.ContentType,
                        Data = new byte[file.ContentLength]
                    };

                    file.InputStream.Read(photo.Data, 0, file.ContentLength);
                    photoService.Update(photo);
                }
            }
        }


    }
}