using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactsApi.Models;
using ContactsApi.Repository;

namespace ContactsApi.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private IContactsRepository ContactsRepository { get; set; }

        public ContactsController(IContactsRepository repo)
        {
            ContactsRepository = repo;
        }

        [HttpGet]
        public IEnumerable<Contact> GetAll()
        {
            return ContactsRepository.GetAll();
        }

        [HttpGet("{id}", Name="GetContact")]
        public IActionResult GetContact(string id)
        {
            var contactId = Guid.Parse(id);
            var contact = ContactsRepository.Find(contactId);
            if(contact == null)
            {
                return NotFound();
            }
            return new ObjectResult(contact);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Contact newContact)
        {
            if(newContact == null)
            {
                return BadRequest();
            }
            ContactsRepository.Add(newContact);
            return CreatedAtRoute("GetContact", 
                                  new { Controller = "Contacts", id = newContact.Id.ToString() }, 
                                  newContact);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Contact item)
        {
            if(item == null)
            {
                return BadRequest();
            }
            if(item.Id.HasValue) 
            {
                var update = ContactsRepository.Find(item.Id.GetValueOrDefault());
                if(update == null)
                {
                    return NotFound();
                }
                ContactsRepository.Update(item);
                return CreatedAtRoute("GetContact", 
                                      new { Controller = "Contact", id = item.Id }, 
                                      item);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var contactId = Guid.Parse(id);
            if(contactId == null)
            {
                return BadRequest("Invalid id value");
            }

            ContactsRepository.Remove(contactId);
            return Ok();
        }

    }
}
