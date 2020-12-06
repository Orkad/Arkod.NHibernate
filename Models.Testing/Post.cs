using FluentNHibernate.Mapping;
using NHibernate.Criterion;
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
        public virtual ISet<Comment> Comments { get; set; } = new HashSet<Comment>();
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
            HasMany(_ => _.Comments).KeyColumn("POST_ID").Inverse().AsSet();
        }
    }
}
