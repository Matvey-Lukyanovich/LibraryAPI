using System.Reflection;
using AutoMapper;

namespace LibraryAPI.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly) => 
            AppleMappingsFromAssembly(assembly);

        public void AppleMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();
            
            foreach (var type in types) 
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
