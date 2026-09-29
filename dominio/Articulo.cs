using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        // marca
        [DisplayName("Marca")]
        public Marca MarcaTipo { get; set; }
        // categoria
        [DisplayName("Categoria")]
        public Categoria CategoriaTipo { get; set; }
        public string ImagenUrl { get; set; }
        public decimal Precio { get; set; }



    }
}
