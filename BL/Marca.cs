using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Marca
    {
        public static List<object> GetAll()
        {
            List<object> marcas = new List<object>();
            using(DL.ESantiagoEKSEntities context = new DL.ESantiagoEKSEntities())
            {
                var query = context.MarcaGetAll().ToList();
                if(query != null)
                {
                    foreach(var item in query)
                    {
                        ML.Marca marca = new ML.Marca();
                        marca.IdMarca = item.IdMarca;
                        marca.Nombre = item.Nombre;

                        marcas.Add(marca);
                    }
                }
            }
            return marcas;
        }
    }
}
