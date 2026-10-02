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
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion {  get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;

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
                    ddlCategoria.DataValueField= "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();
                }

                // Configuracion para saber si trajo un Id de la lista. Si trajo, es que entro para Modificar, no para crear uno Nuevo. Si no trae Id, lo cargamos como string vacio
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"] : string.Empty;


                // Si trae un id de la lista, cargamos los datos del producto de ese id, para ver y/o modificar
                if( id != string.Empty && !IsPostBack)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    List<Articulo> lista = negocio.listar(id);
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
                    txtUrlImagen.Text = seleccionado.ImagenUrl;

                    // Crear un metodo para reemplazar esta llamada
                    txtUrlImagen_TextChanged(sender, e); //Forzamos la llamada a la funcion para que cargue la img

                }
            }
            catch (Exception ex)
            {

                Session.Add("Error ", ex.ToString());
                Response.Redirect("ArticulosLista.aspx");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            imgArticulo.ImageUrl = txtUrlImagen.Text;
        }
    }
}