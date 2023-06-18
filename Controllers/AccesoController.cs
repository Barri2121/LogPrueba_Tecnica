using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Prueba_Tecnica.Models;
using System.Data.SqlClient;
using System.Data;

namespace Prueba_Tecnica.Controllers
{
    public class AccesoController : Controller
    {
        static string conexion = @"Data Source =LAPTOP-1QS177RS\SQLEXPRESS;Initial Catalog=BD_Acceso; Integrated Security=true";


        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult Olvido()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;



            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("Registro_Usuarios", cn);
                cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("clave", oUsuario.Clave);
                cmd.Parameters.AddWithValue("Fecha_Nacimiento", oUsuario.Fecha_Nacimiento);
                cmd.Parameters.Add("registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                cmd.ExecuteNonQuery();
                registrado = Convert.ToBoolean(cmd.Parameters["registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }
            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Login(Usuario oUsuario )
        {
            oUsuario.Clave = oUsuario.Clave;
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("Validacion_Usuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("clave", oUsuario.Clave);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                oUsuario.IdUsuario = Convert.ToInt32(cmd.ExecuteScalar()); //Escalarsolo lee la primera columna
            
            }
            if(oUsuario.IdUsuario != 0)
            {
                Session["usuario"] = oUsuario;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }

        }

        //[HttpPost] A CONTINUACION ES EL METODO PARA ENVIAR EL CORREO USANDO LA BIBLIOTECA MAILKIT
        //, NO PUDE LOGRAR QUE COMPILARA CORRECTAMENTE 
        //public ActionResult Olvido(Usuario oUsuario)
        //{
        //    string mensajee;
        //    string remitente ="mi correo";
        //    string passw= "contraseña";
        //    string destino= "correo Ingresado para Solicitar Devolucion";

        //    oUsuario.Correo = oUsuario.Correo;
        //    using (SqlConnection cn = new SqlConnection(conexion))
        //    {
        //        SqlCommand cmd = new SqlCommand("ObtenerDato1", cn);
        //        cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
        //        cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cn.Open();
        //        mensajee = cmd.Parameters["Mensaje"].Value.ToString();

        //    }
        //    if (mensajee != null)
        //    {
        //        var smtpClient = new smtpClient();
        //        smtpClient.Connect("smtp.gmail.com", 587, false);
        //        smtpClient.Authenticate(remitente, password);

        //        // Crear el mensaje de correo electrónico
        //        var message = new MimeMessage();
        //        message.From.Add(new MailboxAddress("Remitente", remitente));
        //        message.To.Add(new MailboxAddress("Destinatario", destino));
        //        message.Subject = "Recuperacion de Contraseña";
        //        message.Body = new TextPart("Querido usuario")
        //        {
        //            Text = "A continuacion se le entrega su contraseña"+mensajee
        //        };

        //        // Enviar el correo electrónico
        //        smtpClient.Send(message);
        //        smtpClient.Disconnect(true);



        //        return RedirectToAction("Index", "Home");

        //    }
        //    else
        //    {
        //        ViewData["Mensaje"] = "Usuario no encontrado";
        //        return View();
        //    }

        //}

    }


}