using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;

namespace Models.Testing
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual string Author { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Table("POST");
            Id(_ => _.Id, "ID");
            Map(_ => _.Title, "TITLE");
            Map(_ => _.Content, "CONTENT");
            Map(_ => _.Author, "AUTHOR");
            HasMany(_ => _.Comments).KeyColumn("POST_ID");
        }
    }
}
