using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : ControllerBase
    {
        private static IEnumerable<Type> GetTypesSafely(Assembly asm)
        {
            try
            {
                return asm.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                return ex.Types.Where(t => t != null)!;
            }
        }

        [HttpGet("attribute-map")]
        public IActionResult GetAttributeMap()
        {
            var asm = Assembly.GetExecutingAssembly();
            var controllers = GetTypesSafely(asm)
                .Where(t => t.IsClass && !t.IsAbstract &&
                            (t.IsSubclassOf(typeof(ControllerBase)) || t.Name.EndsWith("Controller")))
                .ToList();

            var map = new List<object>();

            foreach (var ctrl in controllers)
            {
                var actions = ctrl.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(m => !m.IsSpecialName)
                    .Select(m => new
                    {
                        Action = m.Name,
                        ReturnType = m.ReturnType.Name,
                        Attributes = m.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")),
                        Parameters = m.GetParameters().Select(p => new { p.Name, Type = p.ParameterType.Name })
                    }).ToList();

                map.Add(new
                {
                    Controller = ctrl.FullName,
                    Attributes = ctrl.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", "")),
                    Actions = actions
                });
            }

            return Ok(new { GeneratedAt = DateTime.UtcNow, Controllers = map });
        }
    }
}
