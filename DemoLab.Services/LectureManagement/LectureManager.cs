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

        public Lecture GetLectureById(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var lecture = _applicationDbContext.Lectures.SingleOrDefault(i => i.Id == id);

            if (lecture == null)
            {
                throw new LectureNotFoundException(nameof(id));
            }

            return lecture;
        }

        public Lecture GetLectureByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var lecture = _applicationDbContext.Lectures.SingleOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (lecture == null)
            {
                throw new LectureNotFoundException(nameof(name));
            }

            return lecture;
        }

        public IEnumerable<Lecture> GetAll()
        {
            return _applicationDbContext.Lectures.ToList();
        }

        public Guid CreateLecture(string lectureName, byte[] text, Guid subsectionId)
        {
            if (lectureName == null || text == null || subsectionId == null)
            {
                throw new ArgumentNullException();
            }

			var lecture = new Lecture() { Id = Guid.NewGuid(), Name = lectureName, Text = text, SubsectionId = subsectionId };

            _applicationDbContext.Lectures.Add(lecture);

            return lecture.Id;
        }
    }
}
