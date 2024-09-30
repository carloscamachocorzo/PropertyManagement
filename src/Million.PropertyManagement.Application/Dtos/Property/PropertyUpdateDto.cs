namespace Million.PropertyManagement.Application.Dtos.Property
{
    /// <summary>
    /// DTO que representa los datos necesarios para actualizar una propiedad.
    /// </summary>
    public class PropertyUpdateDto
    {
        /// <summary>
        /// Obtiene o establece el nombre de la propiedad. 
        /// Esta propiedad es opcional.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección de la propiedad.
        /// Esta propiedad es opcional.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Obtiene o establece el precio de la propiedad.
        /// Esta propiedad es opcional.
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Obtiene o establece el código interno de la propiedad.
        /// Esta propiedad es opcional.
        /// </summary>
        public string? CodeInternal { get; set; }

        /// <summary>
        /// Obtiene o establece el año de construcción de la propiedad.
        /// Esta propiedad es opcional.
        /// </summary>
        public int? Year { get; set; }
    }

}
