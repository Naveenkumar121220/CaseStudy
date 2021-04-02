using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LearnTodayWebAPI.Models;

namespace LearnTodayWebAPI.Controllers
{
    public class StudentController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllCourses()
        {
            LearnTodayWenAPIContext context = new LearnTodayWenAPIContext();
            var list=(from c in context.Courses select new { CourseId = c.CourseId, Title = c.Title, Fees = c.Fees, Description = c.Description, Trainer = c.Trainer, Start_Date = c.Start_Date }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, list);
            
        }
        [HttpPost]
        public HttpResponseMessage PostStudent([FromBody]Student model)
        {
            try
            {
                using(LearnTodayWenAPIContext context=new LearnTodayWenAPIContext())
                {
                    context.Students.Add(model);
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created,model);

                }
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteStudentEnrollment(int id)
        {
            try
            {
                using (LearnTodayWenAPIContext co = new LearnTodayWenAPIContext())
                {
                    var s = co.Students.FirstOrDefault(x => x.EnrollementId == id);
                    if (s != null)
                    { 
                    co.Students.Remove(s);
                    co.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK,"Deleted successfully");
                     }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "No Enrollement information found");
                    }
                }
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
