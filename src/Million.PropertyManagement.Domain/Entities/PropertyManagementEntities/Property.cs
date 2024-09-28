using System;
using System.Collections.Generic;

namespace Million.PropertyManagement.Infrastructure
{
    public partial class Property
    {
        public Property()
        {
            PropertyImage = new HashSet<PropertyImage>();
        }

        /// <summary>
        /// Identificador de la propiedad
        /// </summary>
        public int IdProperty { get; set; }
        /// <summary>
        /// Nombre de la propiedad
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Direccion de la propiedad
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Precio de la propiedad
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// Codigo interno de la propiedad
        /// </summary>
        public string CodeInternal { get; set; }
        /// <summary>
        /// Año de la propiedad
        /// </summary>
        public int? Year { get; set; }
        /// <summary>
        /// Id del propietario de la propiedad
        /// </summary>
        public int? IdOwner { get; set; }

        public virtual Owner IdOwnerNavigation { get; set; }
        public virtual PropertyTrace PropertyTrace { get; set; }
        public virtual ICollection<PropertyImage> PropertyImage { get; set; }
    }
}
