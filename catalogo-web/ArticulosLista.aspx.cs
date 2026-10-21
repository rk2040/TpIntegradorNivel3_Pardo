using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace catalogo_web
{
    public partial class ArticulosLista : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        public List<Articulo> ListaArticulos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulos = negocio.listarConSP();

            if (!Seguridad.esAdmin(Session["usuario"]))
            {
                btnAgregar.Visible = false;
                dgvArticulos.Columns[7].Visible = false; //Columna de boton visible solo para admin
            }

            if (Session["usuario"] != null)
            {
                Usuario usuario = (Usuario)Session["usuario"];
                FavoritoNegocio favNegocio = new FavoritoNegocio();

                // Recorro la lista de art completa y asigno valor favorito a los art de la lista en Session del usuario
                foreach (var articulo in ListaArticulos)
                    articulo.EsFavorito = favNegocio.esFavorito(usuario.Id, articulo.Id);
            }

            FiltroAvanzado = chkAvanzado.Checked;
            if (!IsPostBack)
            {
                //ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("listaArticulos", ListaArticulos);
                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();
            }
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" +  id);
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            dgvArticulos.DataSource = Session["listaArticulos"];
            dgvArticulos.DataBind();
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];

            //List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));

            List<Articulo> listaFiltrada = lista.FindAll(x => !string.IsNullOrEmpty(x.Nombre) && x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper())); // Agrego : !string.IsNullOrEmpty(x.Nombre) &&  , para evitar que un nombre null pase por ToUpper

            dgvArticulos.DataSource= listaFiltrada;
            dgvArticulos.DataBind();
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cargo los ddl de criterio segun lo que seleccione en el ddl campo
            ddlCriterio.Items.Clear();
            if(ddlCampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Menor a");
                ddlCriterio.Items.Add("Mayor a");
            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Empieza con");
                ddlCriterio.Items.Add("Termina con");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                dgvArticulos.DataSource = negocio.filtrar(ddlCampo.SelectedItem.ToString(), ddlCriterio.SelectedItem.ToString(), txtFiltroAvanzado.Text);

                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("Error", Seguridad.manejoError(ex));
                Response.Redirect("Error.aspx", false);
            }
        }

        // Agregado para los botones de Favorito y Detalle
        protected void dgvArticulos_RowCommand(object sender, GridViewCommandEventArgs e) 
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }
            FavoritoNegocio negocio = new FavoritoNegocio();
            Usuario usuario = (Usuario)Session["usuario"];
            int idSeleccionado = Convert.ToInt32(e.CommandArgument);

            // Pregunto si esFavorito
            bool yaEsFavorito = negocio.esFavorito(usuario.Id, idSeleccionado);

            if (!yaEsFavorito) // Si !NO es favorito, lo agrego
                negocio.agregarFavorito(usuario.Id, idSeleccionado);

            else // Si no es Favorito el boton entra en modo "Quitar de Favoritos"
                negocio.eliminarFavorito(usuario.Id, idSeleccionado);

            FavoritoNegocio favNegocio = new FavoritoNegocio();
            foreach (var articulo in ListaArticulos)
                articulo.EsFavorito = favNegocio.esFavorito(usuario.Id, articulo.Id);

            Session["listaArticulos"] = ListaArticulos; // Actualizo la lista en Session
            dgvArticulos.DataSource = Session["listaArticulos"];
            dgvArticulos.DataBind();
        }
    }
}