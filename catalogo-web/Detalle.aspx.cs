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
    public partial class Detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Carga inicial de la page Formulario
                if (!IsPostBack)
                {
                    MarcaNegocio marcaNegocio = new MarcaNegocio();
                    List<Marca> marcaLista = marcaNegocio.listar();

                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    List<Categoria> categoriaLista = categoriaNegocio.listar();

                    ddlMarca.DataSource = marcaLista;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    ddlCategoria.DataSource = categoriaLista;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();
                }

                // Configuracion para saber si trajo un Id de la lista. Si trajo, es que entro para Modificar, no para crear uno Nuevo. Si no trae Id, lo cargamos como string vacio
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"] : string.Empty;
                int idArticulo = 0;

                // Si trae un id de la lista, cargamos los datos del producto de ese id, para ver el detalle
                ArticuloNegocio negocio = new ArticuloNegocio();

                if (!int.TryParse(id, out idArticulo)) // Intento de casteo para el int, sino puede, Exception
                    throw new Exception("El ID del articulo no es valido.");
                List<Articulo> lista = negocio.listarConSP(idArticulo);
                Articulo seleccionado = lista[0]; // devuelve una lista con un unico elemento, el del id

                // Guarto el Articulo en Session
                Session.Add("articuloSeleccionado", seleccionado);

                // Precargo los datos en el formulado del artSeleccionado
                txtId.Text = id;
                txtCodigo.Text = seleccionado.Codigo;
                txtNombre.Text = seleccionado.Nombre;
                txtDescripcion.Text = seleccionado.Descripcion;

                ddlMarca.SelectedValue = seleccionado.MarcaTipo.Id.ToString();
                ddlCategoria.SelectedValue = seleccionado.CategoriaTipo.Id.ToString();

                txtPrecio.Text = seleccionado.Precio.ToString();

                imgArticulo.ImageUrl = seleccionado.ImagenUrl;

            }
            catch (Exception ex)
            {

                Session.Add("Error", Seguridad.manejoError(ex));
                Response.Redirect("Error.aspx", false);
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            // Traigo el dato del html para saber a que page volver
            string origen = Request.QueryString["origen"];

            // Probando como volver a distintas pages sin usar codigo Js en el html: OnClientClick="javascript:history.back(); return false;"
            if (origen == "grid")
                Response.Redirect("ArticulosLista.aspx", false);
            else if (origen == "favorito")
                Response.Redirect("Favoritos.aspx", false);
            else
                Response.Redirect("Default.aspx", false);
        }
    }
}