using System;
using System.Web.Http;
using DemoLab.Models.LectureManagement;
using DemoLab.Services.LectureManagement;

namespace DemoLab.Controllers
{
    [RoutePrefix("api/v1/lectures")]
    public class LecturesController : ApiController
    {
        private readonly LectureManager _lectureManager;
        private readonly SubsectionManager _subsectionManager;

        public LecturesController()
        {
            _lectureManager = new LectureManager();
            _subsectionManager = new SubsectionManager();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}:guid")]
        public IHttpActionResult GetLecture(Guid id)
        {
            try
            {
                var lecture = _lectureManager.GetLectureById(id);
                return Ok(lecture);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (LectureNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetLectures()
        {
            try
            {
                var lectures = _lectureManager.GetAll();
                return Ok(lectures);
            }
            catch (LectureNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateLecture([FromBody]string lectureName, [FromBody]byte[] text, [FromBody]string subsectionName)
        {
            try
            {
                if (lectureName == null || text == null || subsectionName == null)
                {
                    return BadRequest();
                }

                var lecture = _lectureManager.GetLectureByName(lectureName);

                if (lecture != null)
                {
                    return BadRequest();
                }

                var subsectionId = _subsectionManager.GetSubsectionByName(subsectionName).Id;

                var lectureId = _lectureManager.CreateLecture(lectureName, text, subsectionId);

                return Ok(lectureId);
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
