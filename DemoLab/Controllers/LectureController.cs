using System.Web.Http;
using DemoLab.Services.LectureManagement;

namespace DemoLab.Controllers
{
    [RoutePrefix("api/v1/lectures")]
    public class LectureController : ApiController
    {
        private readonly LectureManager _lectureManager;
        private readonly SubsectionManager _subsectionManager;

        public LectureController()
        {
            _lectureManager = new LectureManager();
            _subsectionManager = new SubsectionManager();
        }

        public IHttpActionResult GetLectures()
        {
            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateLecture([FromBody]string lectureName, [FromBody]byte[] text, [FromBody]int subsectionName)
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

                return Ok();
            }
            catch
            {
                return InternalServerError();
            }
        }
    }
}
