using FluentNHibernate.Mapping;

namespace Models.Testing
{
    public class Comment
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual string Author { get; set; }
        public virtual Post Post { get; set; }
    }

    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Table("COMMENT");
            Id(_ => _.Id, "ID");
            Map(_ => _.Title, "TITLE");
            Map(_ => _.Content, "CONTENT");
            Map(_ => _.Author, "AUTHOR");
            References(_ => _.Post, "POST_ID");
        }
    }
}
