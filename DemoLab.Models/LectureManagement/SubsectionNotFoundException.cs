using System;

namespace DemoLab.Models.LectureManagement
{
    public class SubsectionNotFoundException : Exception
    {
        public SubsectionNotFoundException()
            : base()
        {
        }

        public SubsectionNotFoundException(string message)
            : base(message)
        {
        }

        public SubsectionNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
