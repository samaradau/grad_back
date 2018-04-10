using System.Linq;
using AutoMapper;
using DemoLab.Data.Access.ExerciseManagement;
using DemoLab.Models.ExerciseManagement;
using EntityTask = DemoLab.Data.Access.ExerciseManagement.CandidateTask;
using EntityTaskResult = DemoLab.Data.Access.ExerciseExecutor.CandidateTaskResult;
using ModelTask = DemoLab.Models.ExerciseManagement.CandidateTask;
using ModelTaskResult = DemoLab.Models.ExerciseExecutor.CandidateTaskResult;

namespace DemoLab.Services.Mapping
{
    /// <summary>
    /// Represents a mapping profile for exercises.
    /// </summary>
    internal class CandidateExerciseMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateExerciseMappingProfile"/> class.
        /// </summary>
        public CandidateExerciseMappingProfile()
        {
            CreateMap<EntityTask, ModelTask>()
                .ForMember(taskInfo => taskInfo.Tips, opt => opt.MapFrom(
                    task => task.Tips.Select(_ => _.Text).ToArray()));

            CreateMap<ModelTask, EntityTask>()
                .ForMember(taskInfo => taskInfo.Tips, opt => opt.MapFrom(
                    task => task.Tips.Select(tip => new CandidateTaskTip
                {
                    Text = tip,
                    CandidateTaskId = task.Id
                })));

            CreateMap<EntityTaskResult, ModelTaskResult>();
            CreateMap<ModelTaskResult, EntityTaskResult>();

            CreateMap<EntityTask, CandidateTaskInfo>()
                .ForMember(taskInfo => taskInfo.AssemblyName, opt => opt.MapFrom(task => task.TestClass.AssemblyInfo.AssemblyName))
                .ForMember(taskInfo => taskInfo.TestClassName, opt => opt.MapFrom(task => task.TestClass.Name))
                .ForMember(taskInfo => taskInfo.TestMethodName, opt => opt.MapFrom(task => task.TestMethod.Name))
                .ForMember(taskInfo => taskInfo.Tips, opt => opt.MapFrom(task => task.Tips.Select(_ => _.Text).ToArray()));
        }
    }
}
