using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoEKSEntities context = new DL.ESantiagoEKSEntities())
                {
                    usuario.PasswordBytes = Encriptar(Encoding.UTF8.GetBytes(usuario.PasswordString));
                    int rowsAffected = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.PasswordBytes);
                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "Usuario agregado correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Error al registrar el nuevo usuario.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Login(string email, string password)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.ESantiagoEKSEntities context = new DL.ESantiagoEKSEntities())
                {
                    byte[] passwordBytes = Encriptar(Encoding.UTF8.GetBytes(password));
                    var query = context.UsuarioLogin(email, passwordBytes).FirstOrDefault();
                    if(query != null)
                    {
                        ML.Usuario usuarioResult = new ML.Usuario();
                        result.Object = new object();
                        usuarioResult.IdUsuario = query.IdUsuario;
                        usuarioResult.Nombre = query.Nombre;
                        usuarioResult.ApellidoPaterno = query.ApellidoPaterno;
                        usuarioResult.ApellidoMaterno = query.ApellidoMaterno;
                        usuarioResult.Email = query.Email;
                        usuarioResult.PasswordBytes = query.Password;
                        result.Correct = true;
                        result.Message = "Acceso concedido.";
                        result.Object = usuarioResult;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "Usuario no encontrado.";
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
        private static byte[] Encriptar(byte[] data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] datosEncriptados = sha256.ComputeHash(data);
                return datosEncriptados;
            }
        }
    }
}
