using DemoDI.BL.Interfaces;
using DemoDI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoDI.Controllers
{
    public class ContactController : Controller
    {
        private IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        // GET: Contact
        public ActionResult Index()
        {
            List<Contact> lstContacts = this._service.GetAllContacts();
            return View(lstContacts);
        }

        [HttpGet]
        public PartialViewResult AddContact(int? id)
        {
            Contact objContact = null;

            if (id == null)
            {
                objContact = new Contact();
            }
            else
            {
                objContact = this._service.GetContactById((int)id);
            }

            return PartialView(objContact);
        }

        [HttpPost]
        public ActionResult SaveContact(Contact objContact)
        {
            if (ModelState.IsValid)
            {
                Contact result = this._service.AddOrUpdateContact(objContact);
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(objContact, JsonRequestBehavior.AllowGet);
        }
    }
}