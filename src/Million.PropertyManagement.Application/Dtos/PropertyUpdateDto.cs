namespace Million.PropertyManagement.Application.Dtos
{
    public class PropertyUpdateDto
    {
        public string? Name { get; set; }            // Nombre de la propiedad
        public string? Address { get; set; }         // Dirección de la propiedad
        public decimal? Price { get; set; }          // Precio de la propiedad
        public string? CodeInternal { get; set; }    // Código interno de la propiedad
        public int? Year { get; set; }               // Año de construcción de la propiedad
    }
}
