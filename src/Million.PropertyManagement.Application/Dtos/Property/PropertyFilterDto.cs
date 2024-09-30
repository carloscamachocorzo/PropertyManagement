namespace Million.PropertyManagement.Application.Dtos.Property
{
    /// <summary>
    /// DTO que representa los filtros aplicables al obtener una lista de propiedades.
    /// </summary>
    public class PropertyFilterDto
    {
        /// <summary>
        /// Obtiene o establece el nombre de la propiedad para filtrar.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtiene o establece el precio mínimo para filtrar las propiedades.
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Obtiene o establece el precio máximo para filtrar las propiedades.
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Obtiene o establece el año de la propiedad para filtrar.
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Obtiene o establece el número de la página a obtener.
        /// Por defecto, es la primera página.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Obtiene o establece el tamaño de la página (número de elementos por página).
        /// Por defecto, es 10 elementos por página.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }

}
