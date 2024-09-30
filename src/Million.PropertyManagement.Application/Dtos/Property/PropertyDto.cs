namespace Million.PropertyManagement.Application.Dtos.Property
{
    /// <summary>
    /// DTO que representa las propiedades de una propiedad.
    /// </summary>
    public class PropertyDto
    {
        /// <summary>
        /// Obtiene o establece el nombre de la propiedad.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Obtiene o establece la dirección de la propiedad.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Obtiene o establece el precio de la propiedad.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Obtiene o establece el código interno de la propiedad.
        /// </summary>
        public string CodeInternal { get; set; }

        /// <summary>
        /// Obtiene o establece el año de construcción de la propiedad.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Obtiene o establece el ID del propietario de la propiedad.
        /// </summary>
        public int IdOwner { get; set; }
    }

}
