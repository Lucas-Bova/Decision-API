using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DecisionAPI2.Models;

namespace DecisionAPI2.Controllers
{
    [Authorize]
    public class OptionsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Options
        //public IQueryable<Option> GetOptions()
        //{
        //    return db.Options;
        //}

        //get options by room id
        //GET: api/Options/5
        //[ResponseType(typeof(Option))]
        public List<Option> GetOptions(int id)
        {
            var options = (from o in db.Options
                          where o.Room_Id == id
                          select o).ToList();

            if (options == null)
            {
                return null;
            }

            return options;
        }


        // PUT: api/Options/5
        //[ResponseType(typeof(void))]
        //public async Task<IHttpActionResult> PutOption(int id, Option option)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != option.Option_Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(option).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OptionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Options
        //[ResponseType(typeof(Option))]
        //public async Task<IHttpActionResult> PostOption(Option option)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Options.Add(option);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = option.Option_Id }, option);
        //}

        //post array of options

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOptionUpdateVote([System.Web.ModelBinding.QueryString]int id)
        {
            //find the right option
            var option = await db.Options.Where(o => o.Option_Id == id).FirstOrDefaultAsync();
            //update the option vote count
            option.VoteCount += 1;
            //save to db

            db.Entry(option).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.OK);
        }

        [ResponseType(typeof(Option))]
        public async Task<IHttpActionResult> PostOptionArr(List<Option> options)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach(Option op in options)
            {
                db.Options.Add(op);
            }
            await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = option.Option_Id }, option);
            return StatusCode(HttpStatusCode.Created);
        }

        // DELETE: api/Options/5
        [ResponseType(typeof(Option))]
        public async Task<IHttpActionResult> DeleteOption(int id)
        {
            Option option = await db.Options.FindAsync(id);
            if (option == null)
            {
                return NotFound();
            }

            db.Options.Remove(option);
            await db.SaveChangesAsync();

            return Ok(option);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OptionExists(int id)
        {
            return db.Options.Count(e => e.Option_Id == id) > 0;
        }
    }
}