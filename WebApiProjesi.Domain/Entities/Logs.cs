using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProjesi.Domain.Entities
{
    public enum AppLogLevel
    {
        Info,
        Warning,
        Error
    }
    public class Logs
    {
        public int Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public AppLogLevel Level { get; set; } = AppLogLevel.Info;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
