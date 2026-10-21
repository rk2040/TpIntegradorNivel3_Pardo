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

            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                FavoritoNegocio favNegocio = new FavoritoNegocio();

                // Recorro la lista de art completa y asigno valor favorito a los art de la lista en Session del usuario
                foreach (var articulo in ListaArticulos)
                    articulo.EsFavorito = favNegocio.esFavorito(usuario.Id, articulo.Id);
            }

            if (!IsPostBack)
            {
                Session.Add("listaArticulos", ListaArticulos);
                repRepetido.DataSource = Session["listaArticulos"]; 
                repRepetido.DataBind();
            }
        }

        protected void btnFavorito_Click(object sender, EventArgs e) // Eliminarlo 
        {
        }

        protected void btnFavorito_Command(object sender, CommandEventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }
            FavoritoNegocio negocio = new FavoritoNegocio();
            Usuario usuario = (Usuario)Session["usuario"];
            int idSeleccionado = Convert.ToInt32(e.CommandArgument);

            // Pregunto si esFavorito
            bool yaEsFavorito = negocio.esFavorito(usuario.Id, idSeleccionado);

            if(!yaEsFavorito) // Si !NO es favorito, lo agrego
                negocio.agregarFavorito(usuario.Id, idSeleccionado);

            else // Si no es Favorito el boton entra en modo "Quitar de Favoritos"
                negocio.eliminarFavorito(usuario.Id, idSeleccionado);

            FavoritoNegocio favNegocio = new FavoritoNegocio();
            foreach (var articulo in ListaArticulos)
                articulo.EsFavorito = favNegocio.esFavorito(usuario.Id, articulo.Id);

            Session.Add("listaArticulos", ListaArticulos); // Actualizo datos en la Session
            repRepetido.DataSource = Session["listaArticulos"];
            repRepetido.DataBind();
        }
    }
}