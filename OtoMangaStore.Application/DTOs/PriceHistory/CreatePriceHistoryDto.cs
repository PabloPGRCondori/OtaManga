namespace OtoMangaStore.Application.DTOs.PriceHistory
{
    public class CreatePriceHistoryDto
    {
        public int ContentId { get; set; }
        public decimal NewPrice { get; set; }
    }
}