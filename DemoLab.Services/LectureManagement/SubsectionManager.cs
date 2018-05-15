using System;
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

        public Subsection GetSubsectionByName(int subsectionName)
        {
            throw new NotImplementedException();
        }
    }
}
