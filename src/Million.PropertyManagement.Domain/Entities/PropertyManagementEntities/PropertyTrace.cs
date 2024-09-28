using System;
using System.Collections.Generic;

namespace Million.PropertyManagement.Infrastructure
{
    public partial class PropertyTrace
    {
        /// <summary>
        /// Identificador del Registro
        /// </summary>
        public int IdPropertyTrace { get; set; }
        /// <summary>
        /// Fecha de Venta
        /// </summary>
        public DateTime? DateSale { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Valor
        /// </summary>
        public decimal? Value { get; set; }
        /// <summary>
        /// Valor de impuesto
        /// </summary>
        public decimal? Tax { get; set; }
        /// <summary>
        /// Identificador de la propiedad
        /// </summary>
        public int? IdProperty { get; set; }

        public virtual Property IdPropertyTraceNavigation { get; set; }
    }
}
