using System;
using System.Runtime.Serialization;

namespace MB.SharpTodo.Core
{
    [Serializable]
    public class SharpTodoException : Exception
    {
        public SharpTodoException()
        {
        }

        public SharpTodoException(string message)
            : base(message)
        {
        }

        public SharpTodoException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected SharpTodoException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }       
}
