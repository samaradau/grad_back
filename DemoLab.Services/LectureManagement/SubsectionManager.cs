using System;
using System.Linq;
using DemoLab.Data.Access.Context;
using DemoLab.Models.LectureManagement;

namespace DemoLab.Services.LectureManagement
{
    public class SubsectionManager
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SubsectionManager()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        public Subsection GetSubsectionByName(string subsectionName)
        {
            if (subsectionName == null)
            {
                throw new ArgumentNullException(nameof(subsectionName));
            }

            var subsection = _applicationDbContext.Subsections.SingleOrDefault(x => x.Name.Equals(subsectionName, StringComparison.OrdinalIgnoreCase));

            if (subsection == null)
            {
                throw new SubsectionNotFoundException(nameof(subsectionName));
            }

            return subsection;
        }
    }
}
