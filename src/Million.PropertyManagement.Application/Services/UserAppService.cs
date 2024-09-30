﻿using Million.PropertyManagement.Application.Dtos.Authentication;
using Million.PropertyManagement.Application.Services.Interfaces;
using Million.PropertyManagement.Common;
using Million.PropertyManagement.Domain.Interfaces;
using Million.PropertyManagement.Infrastructure;

namespace Million.PropertyManagement.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        #region Builder       
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="UserAppService"/>.
        /// </summary>
        /// <param name="userRepository">Repositorio para la gestión de usuarios.</param>
        /// <param name="passwordHasher">Servicio para hashear contraseñas.</param>
        public UserAppService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;

        }
        #endregion

        #region Metodos publicos       
        public async Task<RequestResult<bool>> CreateUserAsync(RegisterUserRequestDto request)
        {
            // Validar si el usuario ya existe
            var existingUser =  _userRepository.GetUserByUsername(request.Username);
            if (existingUser != null)
            {
                return new RequestResult<bool> { IsSuccessful = false, Messages = new string[] { "usuario ya existe" } };
            }

            // Hashear la contraseña
            var (passwordHash, passwordSalt) = _passwordHasher.HashPassword(request.Password);

            // Crear la entidad usuario
            var user = new Users
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = request.Email,
                IsActive = true,
                CreatedAt = DateTime.UtcNow                

            };

            // Guardar el usuario
            await _userRepository.AddAsync(user);

            // Retornar el resultado exitoso
            return new RequestResult<bool> { IsSuccessful = true,  Messages = new string[] { "usuario creado correctamente" } };
        }
        #endregion
    }
}
