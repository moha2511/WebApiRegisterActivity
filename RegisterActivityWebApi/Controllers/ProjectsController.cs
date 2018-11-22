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
using RegisterActivityWebApi.DTO;
using RegisterActivityWebApi.EF;

namespace RegisterActivityWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjectsController : ApiController
    {
        private TimeRegestrationWebApiDBEntities db = new TimeRegestrationWebApiDBEntities();

        // GET: api/Projects
        public IQueryable<ProjectDTO> GetProjects()
        {
            var projects = from project in db.Projects
                           select new ProjectDTO
                           {
                               ID = project.ID,
                               Title = project.Title,
                               Info = project.Info,
                               isDone = project.isDone,
                               OrganzationName = project.Organization.OrganizationName
                           };
           
            return projects;
        }

        // GET: api/Projects/5
        [ResponseType(typeof(ProjectDTO))]
        public async Task<IHttpActionResult> GetProject(int id)
        {

           

            Project project = await db.Projects.FindAsync(id);

            ProjectDTO projectDTO = new ProjectDTO
            {
                 ID = project.ID,
                 Info = project.Info,
                 isDone = project.isDone,
                 OrganzationName = project.Organization.OrganizationName,
                 Title = project.Title
            };


            if (project == null)
            {
                return NotFound();
            }

            return Ok(projectDTO);
        }

        // PUT: api/Projects/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProject(int id, Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != project.ID)
            {
                return BadRequest();
            }

            db.Entry(project).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(id))
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

        // POST: api/Projects
        [ResponseType(typeof(Project))]
        public async Task<IHttpActionResult> PostProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Projects.Add(project);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProjectExists(project.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = project.ID }, project);
        }

        // DELETE: api/Projects/5
        [ResponseType(typeof(Project))]
        public async Task<IHttpActionResult> DeleteProject(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            db.Projects.Remove(project);
            await db.SaveChangesAsync();

            return Ok(project);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProjectExists(int id)
        {
            return db.Projects.Count(e => e.ID == id) > 0;
        }
    }
}