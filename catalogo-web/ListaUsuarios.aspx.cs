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
    public partial class ListaUsuarios : System.Web.UI.Page
    {
        List<Usuario> ListaDeUsuarios { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.esAdmin(Session["usuario"]))
            {
                Session.Add("Error", "Necesita permisos de Admin para acceder a esta sección.");
                Response.Redirect("Error.aspx");
                return;
            }

            if (!IsPostBack)
            {
                cargarListaUsuarios();
            }
        }

        protected void dgvListaUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "CambiarAdmin")
            {
                try
                {
                    int idUsuario = Convert.ToInt32(e.CommandArgument); // Campturo el id desde el html del CommandArgument
                    if(idUsuario == 1)
                    {
                        Session.Add("Error", "No se puede cambiar el permiso de Admin al Usuario Admin original.");
                        Response.Redirect("Error.aspx");
                        return;
                    }

                    UsuarioNegocio negocio = new UsuarioNegocio();
                    negocio.cambiarPermisosAdmin(idUsuario);
                    cargarListaUsuarios();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        protected void dgvListaUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvListaUsuarios.PageIndex = e.NewPageIndex;
            cargarListaUsuarios();
        }

        private void cargarListaUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            List<Usuario> listaDeUsuarios = negocio.listarUsuarios();
            dgvListaUsuarios.DataSource = listaDeUsuarios;
            dgvListaUsuarios.DataBind();
        }
    }
}