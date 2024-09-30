namespace Million.PropertyManagement.Application.Dtos.Property
{
    /// <summary>
    /// DTO que representa la respuesta de la creación de una nueva propiedad.
    /// </summary>
    public class CreatePropertyResponseDto
    {
        /// <summary>
        /// Obtiene o establece el ID de la propiedad creada.
        /// </summary>
        public int Id { get; set; }

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
        /// Obtiene o establece los mensajes adicionales relacionados con la creación de la propiedad.
        /// </summary>
        public IEnumerable<string> Messages { get; set; }
    }

}
