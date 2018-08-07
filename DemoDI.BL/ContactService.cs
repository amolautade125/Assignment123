using DemoDI.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoDI.Model;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Configuration;

namespace DemoDI.BL
{
    public class ContactService : IContactService
    {
        public List<Contact> GetAllContacts()
        {
            List<Contact> lstContacts = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIBaseUrl"].ToString());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/Contact/Getcontacts").Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    lstContacts = JsonConvert.DeserializeObject<List<Contact>>(responseString);
                }
            }

            return lstContacts;
        }

        public Contact GetContactById(int id)
        {
            Contact Contact = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIBaseUrl"].ToString());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/Contact/GetContact/"+id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().Result;
                    Contact = JsonConvert.DeserializeObject<Contact>(responseString);
                }
            }

            return Contact;
        }

        public Contact AddOrUpdateContact(Contact objContact)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["APIBaseUrl"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    StringContent content = new StringContent(JsonConvert.SerializeObject(objContact), System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = client.PostAsync("api/Contact/AddOrUpdateContact", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = response.Content.ReadAsStringAsync().Result;
                        objContact = JsonConvert.DeserializeObject<Contact>(responseString);
                    }
                }
            }
            catch
            {}

            return objContact;
        }

        public bool DeleteContactById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
