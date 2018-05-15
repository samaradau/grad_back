using System;

namespace DemoLab.Models.LectureManagement
{
    public class LectureNotFoundException : Exception
    {
        public LectureNotFoundException()
            : base()
        {
        }

        public LectureNotFoundException(string message)
            : base(message)
        {
        }

        public LectureNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
