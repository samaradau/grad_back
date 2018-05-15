using System;

namespace DemoLab.Models.LectureManagement
{
	public class Subsection
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid SectionId { get; set; }
    }
}
