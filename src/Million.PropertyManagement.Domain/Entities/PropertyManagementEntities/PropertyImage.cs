using System;
using System.Collections.Generic;

namespace Million.PropertyManagement.Infrastructure
{
    public partial class PropertyImage
    {
        /// <summary>
        /// Identificador de la imagen de la propiedad
        /// </summary>
        public int IdPropertyImage { get; set; }
        /// <summary>
        /// Id de la propiedad relacionada
        /// </summary>
        public int? IdProperty { get; set; }
        /// <summary>
        /// Url de la imagen del archivo de la propiedad
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// estado Habilitado de la propiedad
        /// </summary>
        public bool? Enabled { get; set; }

        public virtual Property IdPropertyNavigation { get; set; }
    }
}
