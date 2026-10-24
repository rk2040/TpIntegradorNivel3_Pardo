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
        public bool CambioAdmin { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.esAdmin(Session["usuario"]))
            {
                Session.Add("Error", "Necesita permisos de Admin para acceder a esta sección.");
                Response.Redirect("Error.aspx");
                return;
            }
            CambioAdmin = false;

            if (!IsPostBack)
                cargarListaUsuarios();
        }

        protected void dgvListaUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CambiarAdmin")
            {
                try
                {
                    Usuario usuarioLogueado = (Usuario)Session["usuario"]; // Para poder actualizarlo luego
                    int idUsuario = Convert.ToInt32(e.CommandArgument); // Capturo el id desde el html del CommandArgument
                    if (idUsuario == 1)
                    {
                        Session.Add("Error", "No se puede cambiar el permiso de Admin al Usuario Admin original.");
                        Response.Redirect("Error.aspx");
                        return;
                    }

                    if (usuarioLogueado.Id == idUsuario && usuarioLogueado.Admin) // Esta parte es solo para actualizar Session en el caso de que uno quiera auto cambiarse el estado de admin
                    {
                        CambioAdmin = true;
                        ViewState["idUsuarioACambiar"] = idUsuario; // Guarda el id para usar con los botones
                        return; // Corto el flujo para ir a los botones "aceptar" "cancelar"
                    }
                    // Para otros usuarios hace directamente el cambio sin preguntar
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

        protected void btnConfirmaAdmin_Click(object sender, EventArgs e)
        {
            if (ViewState["idUsuarioACambiar"] != null) 
            {
                int idUsuario = Convert.ToInt32(ViewState["idUsuarioACambiar"]);
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.cambiarPermisosAdmin(idUsuario); // Guardo el cambio en BD

                Usuario usuarioLogueado = (Usuario)Session["usuario"];
                usuarioLogueado.Admin = !usuarioLogueado.Admin; // Invierte el true/false que tiene de Admin
                Session["usuario"] = usuarioLogueado; // Actualizo la Session para que el cambio se impacte al instante, sino tendría que salir y volver a loguearse para ver el cambio.

                CambioAdmin = false; // Vuelvo a ocultar los botones
                ViewState["idUsuarioACambiar"] = null; // Vuelvo el ViewState a null
                cargarListaUsuarios(); // Actualizo el grid
                Response.Redirect("ListaUsuarios.aspx", false); // Para confirmar que ya no tiene permisos de admin
                Context.ApplicationInstance.CompleteRequest(); // Fuerza una redirección fuera UpdatePanel para refrescar el estado admin
            }
        }

        protected void btnCancelarAdmin_Click(object sender, EventArgs e)
        {
            CambioAdmin = false; // Vuelvo a ocultar los botones
            ViewState["idUsuarioACambiar"] = null; // Vuelvo el ViewState a null
            cargarListaUsuarios(); // Actualizo el grid
        }

        private void cargarListaUsuarios()
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            List<Usuario> listaDeUsuarios = negocio.listarUsuarios();
            dgvListaUsuarios.DataSource = listaDeUsuarios;
            dgvListaUsuarios.DataBind();
        }

        protected void dgvListaUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvListaUsuarios.PageIndex = e.NewPageIndex;
            cargarListaUsuarios();
        }

    }
}