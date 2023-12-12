using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ESantiagoEKSEntities context = new DL.ESantiagoEKSEntities())
                {
                    var query = context.ProductoGetAll().ToList();
                    if(query != null)
                    {
                        result.Objects = new List<object>();
                        foreach(var item in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.Marca = new ML.Marca();

                            producto.IdProducto = item.IdProducto;
                            producto.Nombre = item.Nombre;
                            producto.Descripcion = item.Descripcion;
                            producto.Costo = item.Costo;
                            producto.Marca.IdMarca = item.IdMarca;
                            producto.Marca.Nombre = item.NombreMarca;

                            result.Objects.Add(producto);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se han podido recuperar los productos.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoEKSEntities context = new DL.ESantiagoEKSEntities())
                {
                    var query = context.ProductoGetById(idProducto).FirstOrDefault();
                    if(query != null)
                    {
                        result.Object = new object();
                        ML.Producto producto = new ML.Producto();
                        producto.Marca = new ML.Marca();

                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.Descripcion = query.Descripcion;
                        producto.Costo = query.Costo;
                        producto.Marca.IdMarca = query.IdMarca;
                        producto.Marca.Nombre = query.NombreMarca;

                        result.Object = producto;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al consultar el producto.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int idProducto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoEKSEntities context = new DL.ESantiagoEKSEntities())
                {
                    int rowsAffected = context.ProductoDelete(idProducto);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Producto eliminado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al eliminar el producto.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoEKSEntities context = new DL.ESantiagoEKSEntities())
                {
                    int rowsAffected = context.ProductoAdd(producto.Nombre, producto.Descripcion, producto.Marca.IdMarca, producto.Costo);
                    if(rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Producto agregado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al agregar el producto.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoEKSEntities context = new DL.ESantiagoEKSEntities())
                {
                    int rowsAffected = context.ProductoUpdate(producto.IdProducto, producto.Nombre, producto.Descripcion, producto.Marca.IdMarca, producto.Costo);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Producto actualizado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al actualizar el producto.";
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
