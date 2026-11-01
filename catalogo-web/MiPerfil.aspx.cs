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
                if (!Seguridad.sesionActiva(Session["usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }

                if (!Seguridad.esAdmin(Session["usuario"]))
                    btnListaUsuarios.Visible = false;

                if (!IsPostBack)
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
            catch (Exception ex)
            {
                Session.Add("Error", Seguridad.manejoError("MiPerfil_Page_Load", ex));
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = (Usuario)Session["usuario"];
                // Para escribir la ruta de la img si se carga algo
                if (txtImagenPerfil.PostedFile != null && !string.IsNullOrEmpty(txtImagenPerfil.PostedFile.FileName))
                {
                    string ruta = Server.MapPath("./Images/");

                    if (!System.IO.Directory.Exists(ruta)) // Si no exixte la carpeta Images la crea
                    {
                        System.IO.Directory.CreateDirectory(ruta);
                    }

                    string nombreArchivo = "perfil-" + usuario.Id + ".jpg";
                    string rutaCompleta = System.IO.Path.Combine(ruta, nombreArchivo); // une la ruta completa

                    txtImagenPerfil.PostedFile.SaveAs(rutaCompleta);
                    usuario.ImagenPerfil = nombreArchivo;
                }

                // Guardo los datos del usuario
                //usuario.Email = txtEmail.Text;
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                negocio.actualizar(usuario);

                // Actualizo la imagen de perfil en el icono
                // Para leer la ruta de la img
                string versionImg = DateTime.Now.Ticks.ToString(); // Para cambiar el nombre de Url entre una foto y otra y no genere problemas el cache
                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = "~/Images/" + usuario.ImagenPerfil + "?v=" + versionImg;
            }
            catch (Exception ex)
            {
                Session.Add("Error",Seguridad.manejoError("MiPerfil_btnGuardar_Click", ex));
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}