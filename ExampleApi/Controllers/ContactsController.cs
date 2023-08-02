using ExampleApi.Data;
using ExampleApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactsApiDbContext dbContext;

        public ContactsController(ContactsApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            //talk to database and return list

            //wrap this inside with ok response for explicitly
            return Ok(await dbContext.Contacts.ToListAsync());

        }


        [HttpGet]
        [Route("{id:guid}")]
        //use contact without s coz its a single annotaion 
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            {
                if (contact == null)
                {
                    return BadRequest();
                }
                return Ok(contact);
            }
        }

        [HttpPost]

        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Email = addContactRequest.Email,
                Address = addContactRequest.Address,
                Phone = addContactRequest.Phone
            };


            //to talk to db 

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();


            //give return as ok response
            return Ok(contact);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContact updateContact)
        {

            //here it will find the data in data through id 
            var contact = dbContext.Contacts.Find(id);


            if (contact != null)
            {


                contact.Name = updateContact.Name;
                contact.Email = updateContact.Email;
                contact.Address = updateContact.Address;
                contact.Phone = updateContact.Phone;


                await dbContext.SaveChangesAsync();

                //retun update response
                return Ok(contact);

            }

            return BadRequest("notfound");

        }


        //delete

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            {
                if(contact != null)
                {
                    dbContext.Contacts.Remove(contact);
                    dbContext.SaveChanges();
                    return Ok();
                }
                return Ok(contact);
            }

        }
            



    }
}
