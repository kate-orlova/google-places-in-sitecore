using System;

namespace Importer.Models
{
    public class GlassBase : IGlassBase
    {
        public virtual Guid Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Language { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string DisplayNameString { get; set; }
    }
}