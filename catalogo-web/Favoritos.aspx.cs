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
            try
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
            catch (Exception ex)
            {
                Session.Add("Error", Seguridad.manejoError("Favoritos_Page_Load", ex));
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvFavoritos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                dgvFavoritos.PageIndex = e.NewPageIndex;
                dgvFavoritos.DataSource = Session["listaFavoritos"];
                dgvFavoritos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", Seguridad.manejoError("Favoritos_dgvFavoritos_PageIndexChanging", ex));
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void dgvFavoritos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {// Compuebo para que no entre el boton editar que tiene commandName = "Select"
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

                List<Articulo> listaActualizada = negocio.listar(usuario.Id);// Actualizo la lista desde la BD
                Session["listaFavoritos"] = listaActualizada; // Actualizo la lista de Session

                dgvFavoritos.DataSource = Session["listaFavoritos"]; 
                dgvFavoritos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", Seguridad.manejoError("Favoritos_dgvFavoritos_RowCommand", ex));
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}