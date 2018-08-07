using DemoDI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoWebAPI.Models;

namespace DemoWebAPI.Controllers
{
    public class ContactController : ApiController
    {
        public IHttpActionResult GetContacts()
        {
            List<Contact> lstContacts = null;

            try
            {
                using (EvolentDBEntities entity = new EvolentDBEntities())
                {
                    lstContacts = entity.tblContacts
                                    .Select(s => new Contact()
                                    {
                                        RecordId = s.RecordId,
                                        FirstName = s.FirstName,
                                        LastName = s.LastName == null ? "" : s.LastName,
                                        Email = s.Email,
                                        PhoneNumber = s.PhoneNumber
                                    }).ToList<Contact>();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

            return Ok(lstContacts);
        }

        public IHttpActionResult GetContact(int id)
        {
            Contact objContact = null;

            try
            {
                objContact = GetContactByID(id);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

            return Ok(objContact);
        }



        public IHttpActionResult AddOrUpdateContact(Contact objContact)
        {
            using (EvolentDBEntities entity = new EvolentDBEntities())
            {
                if (objContact.RecordId == 0)
                {
                    // Add

                    tblContact objtblContact = new tblContact()
                    {
                        FirstName = objContact.FirstName,
                        LastName = objContact.LastName,
                        Email = objContact.Email,
                        PhoneNumber = objContact.PhoneNumber,
                        Status = objContact.Status
                    };

                    entity.tblContacts.Add(objtblContact);
                    entity.SaveChanges();

                    objContact.RecordId = objtblContact.RecordId;
                }
                else
                {
                    // Update
                    var objContactFromDB = entity.tblContacts
                                            .Where(p => p.RecordId == objContact.RecordId)
                                            .FirstOrDefault();

                    objContactFromDB.FirstName = objContact.FirstName;
                    objContactFromDB.LastName = objContact.LastName;
                    objContactFromDB.Email = objContact.Email;
                    objContactFromDB.PhoneNumber = objContact.PhoneNumber;
                    objContactFromDB.Status = objContact.Status;

                    entity.SaveChanges();
                }
            }

            return Ok(objContact);
        }



        #region Private Methods
        [NonAction]
        private Contact GetContactByID(int id)
        {
            Contact objContact = null;

            try
            {
                using (EvolentDBEntities entity = new EvolentDBEntities())
                {
                    objContact = entity.tblContacts
                                    .Where(p => p.RecordId == id)
                                    .Select(p => new Contact
                                    {
                                        RecordId = p.RecordId,
                                        FirstName = p.FirstName,
                                        LastName = p.LastName,
                                        Email = p.Email,
                                        PhoneNumber = p.PhoneNumber,
                                        Status = p.Status
                                    }).FirstOrDefault();
                }
            }
            catch (Exception)
            {
            }
            return objContact;
        }
        #endregion
    }
}
