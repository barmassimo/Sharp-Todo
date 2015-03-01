using System.Runtime.Serialization;

namespace MB.SharpTodo.Core.Domain
{
    [DataContract]
    public partial class Category
    {
        [DataMember]
        public virtual int Id { get; set; }
        
        [DataMember]
        public virtual string Description { get; set; }

        public override string ToString()
        {
            return Description;
        }
    }
}
