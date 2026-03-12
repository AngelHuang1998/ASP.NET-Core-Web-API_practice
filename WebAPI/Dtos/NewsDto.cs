namespace WebAPI.Dtos
{
    public class NewsDto
    {
        public Guid NewsId { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int Click { get; set; }
    }
}
