using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLab.Models.LectureManagement
{
    public class Lecture
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public byte[] Text { get; set; }

        public Guid SubsectionId { get; set; }
    }
}
