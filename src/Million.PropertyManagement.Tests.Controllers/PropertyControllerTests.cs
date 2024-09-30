﻿using Microsoft.AspNetCore.Mvc;
using Million.PropertyManagement.Api.Controllers;
using Million.PropertyManagement.Application.Dtos.Property;
using Million.PropertyManagement.Application.Services.Interfaces;
using Moq;

namespace Million.PropertyManagement.Tests.Controllers
{
    public class PropertyControllerTests
    {
        [Fact]
        public async Task GetPropertiesAsync_Should_Return_Ok_With_Properties()
        {
            // Arrange: Creamos mock de IPropertyAppService e IPropertyImageAppService
            var mockPropertyAppService = new Mock<IPropertyAppService>();
            var mockPropertyImageAppService = new Mock<IPropertyImageAppService>();

            // Simulamos que GetPropertiesAsync devuelva una lista de propiedades
            var properties = new List<PropertyDto>
        {
            new PropertyDto { Name = "Property 1", Price = 1000 },
            new PropertyDto { Name = "Property 2", Price = 2000 }
        };

            // Configuramos el mock para que devuelva esta lista cuando se llame con cualquier PropertyFilterDto
            mockPropertyAppService.Setup(service => service.GetPropertiesAsync(It.IsAny<PropertyFilterDto>()))
                                  .ReturnsAsync(properties);

            // Creamos una instancia del PropertiesController e inyectamos los mocks
            var controller = new PropertiesController(mockPropertyAppService.Object, mockPropertyImageAppService.Object);

            // Creamos un filtro simulado que pasaremos al método
            var filter = new PropertyFilterDto
            {
                Name = "Hotel",
                MinPrice = 0,
                MaxPrice = 5000
            };

            // Act: Llamamos al método que estamos probando, pasando el filtro
            var result = await controller.GetPropertiesWithFilters(filter);

            // Assert: Verificamos que el resultado sea Ok y contenga las propiedades
            var okResult = Assert.IsType<OkObjectResult>(result.ExecuteResultAsync);  // Verifica que el resultado sea Ok
            var returnedProperties = Assert.IsAssignableFrom<IEnumerable<PropertyDto>>(okResult.Value);  // Verifica el tipo devuelto
            Assert.Equal(2, returnedProperties.Count());  // Verifica que hay 2 propiedades en la lista
        }

        [Fact]
        public async Task GetPropertiesAsync_Should_Return_NotFound_When_No_Properties()
        {
            // Arrange: Creamos mock de IPropertyAppService e IPropertyImageAppService
            var mockPropertyAppService = new Mock<IPropertyAppService>();
            var mockPropertyImageAppService = new Mock<IPropertyImageAppService>();

            // Simulamos que GetPropertiesAsync devuelva una lista vacía
            mockPropertyAppService.Setup(service => service.GetPropertiesAsync(It.IsAny<PropertyFilterDto>()))
                                  .ReturnsAsync(new List<PropertyDto>());

            // Creamos una instancia del PropertiesController e inyectamos los mocks
            var controller = new PropertiesController(mockPropertyAppService.Object, mockPropertyImageAppService.Object);

            // Creamos un filtro simulado que pasaremos al método
            var filter = new PropertyFilterDto
            {
                Name = "Hotel",
                MinPrice = 0,
                MaxPrice = 5000
            };

            // Act: Llamamos al método que estamos probando, pasando el filtro
            var result = await controller.GetPropertiesWithFilters(filter);

            // Assert: Verificamos que el resultado sea NotFound
            Assert.IsType<NotFoundResult>(result.ExecuteResultAsync);  // Verifica que el resultado sea NotFound
        }
    }
}