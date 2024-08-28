using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Tabylr.Models
{
    
        [Table("books")]
        public class Book : BaseModel
        {
            [PrimaryKey("id")]
            public int Id { get; set; }
            [Column("title")]
            public string Title { get; set; }
            [Column("created_at")]
            public DateTime CreatedAt { get; set; }
            [Column("author_id")]
            public int AuthorId { get; set; }
            [Column("topic_id")]
            public int TopicId { get; set; }
            [Column("publisher_id")]
            public int PublisherId { get; set; }
            [Column("language_id")]
            public int LanguageId { get; set; }
            [Column("description")]
            public string Description { get; set; }
            [Column("cover_image")]
            public string CoverImage { get; set; }


        }
    }

