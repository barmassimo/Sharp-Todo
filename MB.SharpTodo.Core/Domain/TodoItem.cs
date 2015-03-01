using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using MB.SharpTodo.Core.Domain.Services;


namespace MB.SharpTodo.Core.Domain
{
    [DataContract]
    public partial class TodoItem
    {
        private int? _priority;

        [DataMember]
        public virtual int? Priority
        {
            get
            {
                return _priority;
            }
            set
            {
                if (value <= 0) throw new SharpTodoException("Priority must be > 0.");
                _priority = value;
            }
        }

        [DataMember]
        public virtual int Id { get; set; }

        [DataMember]
        public virtual string Description { get; set; }

        
        [DataMember]
        public virtual DateTime? DueDate { get; set; }
        
        [DataMember]
        public virtual Category Category { get; set; }
        
        [DataMember]
        public virtual ICollection<TodoComment> Comments { get; private set; }

        public TodoItem()
        {
            Comments = new HashSet<TodoComment>();
        }

        public TodoItem(string richDescription, ICategoryResolverService categoryResolverService): this()
        {
            string[] words = richDescription.Split(' ');

            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (word.StartsWith("!") && this.Priority==null) // !3 => priority: 3
                {
                    int val;

                    if (int.TryParse(word.Substring(1), out val))
                    {
                        Priority = val;
                        words[i] = null;
                    }
                }
                else if (word.StartsWith("#") && this.Category==null) // #home => Category: home
                {
                    Category = categoryResolverService.FindOrCreateCategory(word.Substring(1));
                    words[i] = null;
                }
            }

            var notParsed = words.Where(w => w != null);
            Description = string.Join(" ", notParsed.ToArray());
        }

        public void AddComment(TodoComment todoComment)
        {
            todoComment.TodoItem = this;
            Comments.Add(todoComment);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendFormat("[{0}] {1}",Id, Description);
            if (DueDate.HasValue) result.AppendFormat(" @{0}", DueDate);
            if (Priority.HasValue) result.AppendFormat(" !{0}", Priority);
            if (Category != null) result.AppendFormat(" #{0}", Category);
            if (Comments.Count > 0) result.AppendFormat(" ({0} comment{1})", Comments.Count, Comments.Count>1?"s":string.Empty);

            return result.ToString();
        }
    }

   

}
