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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Seguridad.sesionActiva(Session["usuario"]))
            {
                Response.Redirect("Default.aspx", false);
                return;
            }
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;

            try
            {
                Usuario usuario = new Usuario();
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                // Agregar la parte de enviar email
                EmailService emailService = new EmailService();
                //usuario.Id = int.Parse(txtId.Text);
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;
                usuario.ImagenPerfil = txtUrlImagen.Text;

                usuario.Id = usuarioNegocio.agregarUsuario(usuario); 

                Session.Add("usuario", usuario); // Con esto dejo iniciada la Session (Logueado) cuando se registra

                // Hacer la parte de enviar email automatico de bienvenida ACÁ
                emailService.armarCorreo(usuario.Email, "Registro exitoso al catalogo-web", "<h1> Bienvenido al nuevo Catalogo-web</h1> <br/><br/><h3>Te damos la bienvenida y te dejamos tus datos de acceso a continuación:</h3><br/><br/>Usuario: " + usuario.Email + "<br/>Contraseña: " + usuario.Pass + "<br/><br/>Recuenda no darle esta información a nadie.<br/>Te damos la bienvenida y gracias por elegirnos.");
                emailService.enviarEmail();

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", Seguridad.manejoError(ex));
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}