namespace OtoMangaStore.Application.DTOs.Authors
{
    public class UpdateAuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
