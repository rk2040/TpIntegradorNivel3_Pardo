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
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                imgAvatar.ImageUrl = "https://cdn-icons-png.flaticon.com/512/12225/12225935.png";

                if (!(Page is Default || Page is Login || Page is Registro || Page is Error))
                {
                    if (!Seguridad.sesionActiva(Session["usuario"]))
                        Response.Redirect("Login.aspx", false);
                }

                if (Seguridad.sesionActiva(Session["usuario"]))
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    lblUser.Text = usuario.Email;
                    if (!string.IsNullOrEmpty(usuario.ImagenPerfil))
                        imgAvatar.ImageUrl = "~/Images/" + usuario.ImagenPerfil;
                }
            }
            catch (Exception ex)
            {
                Seguridad.manejoError("Master_Page_Load", ex); // Guardo error en la carpeta errores
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                Seguridad.manejoError("Master_btnSalir_Click", ex);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}