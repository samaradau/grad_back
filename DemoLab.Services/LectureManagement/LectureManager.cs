using System;
using System.Collections.Generic;
using System.Linq;
using DemoLab.Data.Access.Context;
using DemoLab.Models.LectureManagement;

namespace DemoLab.Services.LectureManagement
{
	public class LectureManager
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LectureManager()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public Lecture GetLectureByName(string lectureName)
        {
            if (lectureName == null)
            {
                throw new ArgumentNullException(nameof(lectureName));
            }

            var lecture = _applicationDbContext.Lectures.SingleOrDefault(i => i.Name.Equals(lectureName, StringComparison.OrdinalIgnoreCase));

            if (lecture == null)
            {
                throw new LectureNotFoundException(nameof(lectureName));
            }
            else
            {
                return lecture;
            }
        }

        public IEnumerable<Lecture> GetAll()
        {
            return _applicationDbContext.Lectures.ToList();
        }

        public Guid CreateLecture(string lectureName, byte[] text, object subsectionId)
        {
            throw new NotImplementedException();
        }
    }
}
