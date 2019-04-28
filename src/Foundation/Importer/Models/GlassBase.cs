using System;

namespace Importer.Models
{
    public class GlassBase
    {
        
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Language { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string DisplayNameString { get; set; }
    }
}
