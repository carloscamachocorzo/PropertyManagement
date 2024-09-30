namespace Million.PropertyManagement.Application.Dtos
{
    public class PropertyFilterDto
    {
        public string Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Year { get; set; }
        // Paginación
        public int PageNumber { get; set; } = 1; // Por defecto, primera página
        public int PageSize { get; set; } = 10;  // Por defecto, 10 elementos por página
    }
}
