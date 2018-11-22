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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using RegisterActivityWebApi.EF;

namespace RegisterActivityWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TimeSpentsController : ApiController
    {
        private TimeRegestrationWebApiDBEntities db = new TimeRegestrationWebApiDBEntities();

        // GET: api/TimeSpents
        public IQueryable<TimeSpent> GetTimeSpents()
        {
            return db.TimeSpents;
        }

        // GET: api/TimeSpents/5
        [ResponseType(typeof(TimeSpent))]
        public async Task<IHttpActionResult> GetTimeSpent(int id)
        {
            TimeSpent timeSpent = await db.TimeSpents.FindAsync(id);
            if (timeSpent == null)
            {
                return NotFound();
            }

            return Ok(timeSpent);
        }

        // PUT: api/TimeSpents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTimeSpent(int id, TimeSpent timeSpent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != timeSpent.ID)
            {
                return BadRequest();
            }

            db.Entry(timeSpent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TimeSpentExists(id))
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

        // POST: api/TimeSpents
        [ResponseType(typeof(TimeSpent))]
        public async Task<IHttpActionResult> PostTimeSpent(TimeSpent timeSpent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TimeSpents.Add(timeSpent);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TimeSpentExists(timeSpent.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = timeSpent.ID }, timeSpent);
        }

        // DELETE: api/TimeSpents/5
        [ResponseType(typeof(TimeSpent))]
        public async Task<IHttpActionResult> DeleteTimeSpent(int id)
        {
            TimeSpent timeSpent = await db.TimeSpents.FindAsync(id);
            if (timeSpent == null)
            {
                return NotFound();
            }

            db.TimeSpents.Remove(timeSpent);
            await db.SaveChangesAsync();

            return Ok(timeSpent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TimeSpentExists(int id)
        {
            return db.TimeSpents.Count(e => e.ID == id) > 0;
        }
    }
}