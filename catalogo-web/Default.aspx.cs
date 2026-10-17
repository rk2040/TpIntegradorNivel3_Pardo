using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace catalogo_web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulos {  get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulos = negocio.listarConSP();

            if (!IsPostBack)
            {
                repRepetido.DataSource = ListaArticulos; 
                repRepetido.DataBind();
            }
        }

        protected void btnFavorito_Click(object sender, EventArgs e)
        {
        }

        protected void btnFavorito_Command(object sender, CommandEventArgs e)
        {
            FavoritoNegocio negocio = new FavoritoNegocio();
            Usuario usuario = (Usuario)Session["usuario"];
            int idSeleccionado = Convert.ToInt32(e.CommandArgument);

            negocio.agregarFavorito(usuario.Id, idSeleccionado);
            Response.Redirect("Favoritos.aspx");
        }
    }
}