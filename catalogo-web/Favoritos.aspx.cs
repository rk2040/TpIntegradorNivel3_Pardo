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
    }
}