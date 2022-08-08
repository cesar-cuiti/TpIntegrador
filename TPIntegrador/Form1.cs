using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TPIntegrador
{
    public partial class Catalogo : Form
    {
        private List<Articulo> listaAticulos;
        public Catalogo()
        {
            InitializeComponent();
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxCatalogo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxCatalogo.Load("https://efectocolibri.com/wp-content/uploads/2021/01/placeholder.png");
            }
        }

        private void dataCatalogo_SelectionChanged(object sender, EventArgs e)
        {

            if (dataCatalogo.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dataCatalogo.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            cboCampo.Items.Add("Codigo");
            cboCampo.Items.Add("Nombre");
            cboCampo.Items.Add("Descripcion");
        }
        private void ocultarColumnas()
        {

            dataCatalogo.Columns["Id"].Visible = false;
            dataCatalogo.Columns["ImagenUrl"].Visible = false;
        }

        private void cargar()
        {
            NegocioArticulo negocio = new NegocioArticulo();
            try
            {
                listaAticulos = negocio.listar();
                dataCatalogo.DataSource = listaAticulos;
                ocultarColumnas();
                cargarImagen(listaAticulos[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
       

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FormAltaArticulo alta = new FormAltaArticulo();
           
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dataCatalogo.CurrentRow.DataBoundItem;
            FormAltaArticulo modificar = new FormAltaArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnElimFisico_Click(object sender, EventArgs e)
        {
            NegocioArticulo articulo = new NegocioArticulo();
            Articulo seleccionado;
            try
            {
                DialogResult respuesta = MessageBox.Show("¿Desea eliminar?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dataCatalogo.CurrentRow.DataBoundItem;
                    articulo.eliminarFisico(seleccionado.Id);
                    cargar();
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool validarFiltro()
        {
            if(cboCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione el campo");
                return true;
            }
            if(cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione criterio");
                return true;
            }
           
            return false;
        }

        private void btnBuscarFiltro_Click(object sender, EventArgs e)
        {
            NegocioArticulo negocio = new NegocioArticulo();
            try
            {
                if (validarFiltro())
                    return;

                string campo = cboCampo.Text;
                string criterio = cboCriterio.Text;
                string filtroAvan = txtFiltroAvanzado.Text;
                dataCatalogo.DataSource = negocio.filtrar(campo, criterio,filtroAvan);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltroRapido_TextChanged(object sender, EventArgs e)
        {

            List<Articulo> listaFiltrado;

            string filtro = txtFiltroRapido.Text;

            if (filtro != "")
            {
                listaFiltrado = listaAticulos.FindAll(x => x.nombre.ToUpper().Contains(filtro.ToUpper()) || x.codigo.ToUpper().Contains(filtro.ToUpper()));
            }
            else
            {
                listaFiltrado = listaAticulos;
            }
            dataCatalogo.DataSource = null;
            dataCatalogo.DataSource = listaFiltrado;
            ocultarColumnas();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCampo.SelectedItem.ToString();
            if(opcion == "Numero")
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Mayor a");
                cboCriterio.Items.Add("Menor a");
                cboCriterio.Items.Add("Igual a");
            }
            else
            {
                cboCriterio.Items.Clear();
                cboCriterio.Items.Add("Comienza con");
                cboCriterio.Items.Add("Termina con");
                cboCriterio.Items.Add("Contiene");
            }
        }
    }
}
