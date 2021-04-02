using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LearnTodayWebAPI.Models;


namespace LearnTodayWebAPI.Controllers
{
    public class AdminController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetAllCourses()
        {
            LearnTodayWenAPIContext context = new LearnTodayWenAPIContext();
           
            //List<Course> list=context.Courses.ToList();
            var k = (from c in context.Courses select new { CourseId = c.CourseId, Title = c.Title, Fees = c.Fees, Description = c.Description, Trainer = c.Trainer, Start_Date = c.Start_Date }).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, k);


        }
        [HttpGet]
        public HttpResponseMessage GetCourseById(int id)
        {
            try
            {

                using (LearnTodayWenAPIContext context = new LearnTodayWenAPIContext())
                {
                    Course c = context.Courses.Where(x => x.CourseId == id).SingleOrDefault();
                    if (c != null)
                    {
                        var op = new { CourseId = c.CourseId, Title = c.Title, Fees = c.Fees, Description = c.Description, Trainer = c.Trainer, Start_Date = c.Start_Date };
                        return Request.CreateResponse(HttpStatusCode.OK, op);
                    }
                    else
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Searched Data not Found");
                }
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,e);
            }
        }

    }
}
