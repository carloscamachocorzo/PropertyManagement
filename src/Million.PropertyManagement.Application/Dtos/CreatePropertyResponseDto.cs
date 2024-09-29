namespace Million.PropertyManagement.Application.Dtos
{
    public class CreatePropertyResponseDto
    {
        public int Id { get; set; }  // ID de la propiedad creada
        public string Name { get; set; }  // Nombre de la propiedad
        public string Address { get; set; }  // Dirección de la propiedad
        public decimal Price { get; set; }  // Precio de la propiedad
        public IEnumerable<string> Messages { get; set; }  // Mensajes adicionales
    }
}
