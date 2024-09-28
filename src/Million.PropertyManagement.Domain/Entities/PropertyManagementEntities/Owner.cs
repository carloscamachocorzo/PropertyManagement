using System;
using System.Collections.Generic;

namespace Million.PropertyManagement.Infrastructure
{
    public partial class Owner
    {
        public Owner()
        {
            Property = new HashSet<Property>();
        }

        /// <summary>
        /// Identificador del registro de propietarios
        /// </summary>
        public int IdOwner { get; set; }
        /// <summary>
        /// Nombre Completo del propietario
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Direccion del propietario
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Foto del propietario
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// Fecha de cumpleaños del propietario
        /// </summary>
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Property> Property { get; set; }
    }
}
