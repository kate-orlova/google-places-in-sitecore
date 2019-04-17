using System;

namespace Importer.Models
{
    public class GlassBase
    {
        
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }
    }
}
