using System;
using System.Linq;
using AutoMapper;

namespace DemoLab
{
    /// <summary>
    /// Represents a mapper configuration.
    /// </summary>
    public static class MapperConfig
    {
        /// <summary>
        /// Configures a mapper with static configuration.
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("DemoLab")).ToArray();
                cfg.AddProfiles(assemblies);
            });
        }
    }
}
