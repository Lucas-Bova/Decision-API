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
using System.Security;

namespace DecisionAPI2.Controllers
{
    public class RoomsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Rooms
        //public IQueryable<Room> GetRoomModels()
        //{
        //    return db.RoomModels;
        //}

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> GetRoom(int id)
        {
            var room = await db.RoomModels.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        //get by guid
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> GetRoomByHash(Guid guid)
        {
            Room room = db.RoomModels.Where(x => x.Guid == guid)
                                     .FirstOrDefault();
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Room_Id)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rooms
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //generate random identifier here (guid)
            room.Guid = Guid.NewGuid();


            db.RoomModels.Add(room);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = room.Room_Id }, room);

        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> DeleteRoom(int id)
        {
            Room room = await db.RoomModels.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            db.RoomModels.Remove(room);
            await db.SaveChangesAsync();

            return Ok(room);
        }

        //delete by guid
        // DELETE: api/Rooms/{guid} 
        [ResponseType(typeof(Room))]
        public async Task<IHttpActionResult> DeleteRoomByGuid(Guid guid)
        {
            Room room = await db.RoomModels.FindAsync(guid);
            if (room == null)
            {
                return NotFound();
            }

            db.RoomModels.Remove(room);
            await db.SaveChangesAsync();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.RoomModels.Count(e => e.Room_Id == id) > 0;
        }
    }
}