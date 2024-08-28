using System.ComponentModel.DataAnnotations.Schema;

namespace Tabylr.Client.Models
{
    public class Book
    {
        // need to change to different model.
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public int TopicId { get; set; }
        public int PublisherId { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
    }
}
