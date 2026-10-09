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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sesionActiva(Session["usuario"]))
                    {
                        Usuario usuario = (Usuario)Session["usuario"];
                        // Cargo los datos del usuario que esta en sesion al formulario de perfil
                        txtEmail.Text = usuario.Email;
                        txtEmail.ReadOnly = true;
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        if (!string.IsNullOrEmpty(usuario.ImagenPerfil))
                            imgPerfil.ImageUrl = "~/Images/" + usuario.ImagenPerfil;
                        else
                            imgPerfil.ImageUrl = "https://icones.pro/wp-content/uploads/2021/06/icone-d-image-grise.png";
                        //imgPerfil.ImageUrl = "https://cdn-icons-png.flaticon.com/512/12225/12225935.png";
                    }
                }
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex.ToString());
            }
        }

        protected void btnGuardae_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = (Usuario)Session["usuario"];
            // Para escribir la ruta de la img si se carga algo
            if (txtImagenPerfil.PostedFile.FileName != "")
            {
                string ruta = Server.MapPath("./Images/");
                txtImagenPerfil.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");
                usuario.ImagenPerfil = "perfil-" + usuario.Id + ".jpg";
            }

            // Guardo los datos del usuario
            //usuario.Email = txtEmail.Text;
            usuario.Nombre = txtNombre.Text;
            usuario.Apellido = txtApellido.Text;
            negocio.actualizar(usuario);

            // Actualizo la imagen de perfil en el icono
            // Para leer la ruta de la img
            Image img = (Image)Master.FindControl("imgAvatar");
            img.ImageUrl = "~/Images" + usuario.ImagenPerfil;
        }
    }
}