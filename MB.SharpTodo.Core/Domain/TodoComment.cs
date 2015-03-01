using System;
using System.Runtime.Serialization;

namespace MB.SharpTodo.Core.Domain
{
    [DataContract]
    public partial class TodoComment
    {
        [DataMember]
        public virtual int Id { get; set; }
        
        [DataMember]
        public virtual string Text { get; set; }
        
        [DataMember]
        public virtual DateTime Date { get; set; }

        public virtual TodoItem TodoItem { get; set; }

        public TodoComment()
        {
            Date = DateTime.Now;
        }
    }
}
