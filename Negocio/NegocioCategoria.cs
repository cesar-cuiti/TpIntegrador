using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class NegocioCategoria
    {
       
            public List<Categoria> listar()
            {
                AccesoDatos datos = new AccesoDatos();
                List<Categoria> lista = new List<Categoria>();

                try
                {
                    datos.setearConsulta("select Id, Descripcion from CATEGORIAS");
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {
                        Categoria aux = new Categoria();
                        aux.Id = (int)datos.Lector.GetInt32(0);
                        aux.descripcion = (string)datos.Lector["Descripcion"];

                        lista.Add(aux);
                    }

                    return lista;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        
    }
}
