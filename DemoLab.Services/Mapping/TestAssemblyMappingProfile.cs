using AutoMapper;
using EntityAssembliesInfo = DemoLab.Data.Access.ExerciseExecutor.TestAssemblyInfo;
using EntityClassInfo = DemoLab.Data.Access.ExerciseExecutor.TestClassInfo;
using EntityMethodInfo = DemoLab.Data.Access.ExerciseExecutor.TestMethodInfo;
using ModelAssemliesInfo = DemoLab.Models.ExerciseExecutor.TestAssemblyInfo;
using ModelClassInfo = DemoLab.Models.ExerciseExecutor.TestClassInfo;
using ModelMethodInfo = DemoLab.Models.ExerciseExecutor.TestMethodInfo;

namespace DemoLab.Services.Mapping
{
    /// <summary>
    /// Represents a mapping profile for assemblies.
    /// </summary>
    internal class TestAssemblyMappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestAssemblyMappingProfile"/> class.
        /// </summary>
        public TestAssemblyMappingProfile()
        {
            CreateMap<EntityAssembliesInfo, ModelAssemliesInfo>();
            CreateMap<ModelAssemliesInfo, EntityAssembliesInfo>();

            CreateMap<EntityClassInfo, ModelClassInfo>();
            CreateMap<ModelClassInfo, EntityClassInfo>();

            CreateMap<EntityMethodInfo, ModelMethodInfo>();
            CreateMap<ModelMethodInfo, EntityMethodInfo>();
        }
    }
}
