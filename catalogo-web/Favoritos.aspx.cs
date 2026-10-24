using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace catalogo_web
{
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sesionActiva(Session["usuario"]))
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                FavoritoNegocio negocio = new FavoritoNegocio();
                Usuario usuario = (Usuario)Session["usuario"];
                Session.Add("listaFavoritos", negocio.listar(usuario.Id));
                dgvFavoritos.DataSource = Session["listaFavoritos"];
                dgvFavoritos.DataBind();
            }
        }

        protected void dgvFavoritos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvFavoritos.PageIndex = e.NewPageIndex;
            dgvFavoritos.DataSource = Session["listaFavoritos"];
            dgvFavoritos.DataBind();
        }

        protected void dgvFavoritos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Compuebo para que no entre el boton editar que tiene commandName = "Select"
            if (e.CommandName != "Favorito")
                return;

            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }
            FavoritoNegocio negocio = new FavoritoNegocio();
            Usuario usuario = (Usuario)Session["usuario"];
            int idSeleccionado = Convert.ToInt32(e.CommandArgument);

            negocio.eliminarFavorito(usuario.Id, idSeleccionado);

            dgvFavoritos.DataSource = negocio.listar(usuario.Id); // Actualizo la lista desde la BD
            dgvFavoritos.DataBind();
        }
    }
}