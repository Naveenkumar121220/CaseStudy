using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LearnTodayWebAPI.Models;

namespace LearnTodayWebAPI.Controllers
{
    
    public class TrainerController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage TrainerSignUp([FromBody] Trainer model)
        {
            try
            {
                LearnTodayWenAPIContext con = new LearnTodayWenAPIContext();

                con.Trainers.Add(model);
                con.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.Created, model);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdatePassword(int id, [FromBody] Trainer model)
        {
            try
            {
                LearnTodayWenAPIContext c = new LearnTodayWenAPIContext();
                Trainer t = c.Trainers.FirstOrDefault(x => x.TrainerId == id);
                if (t != null)
                {
                    t.Password = model.Password;
                    c.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Data Updated Successfully");
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Searched Data not Found");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}
