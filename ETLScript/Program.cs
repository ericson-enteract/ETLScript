using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization; 

namespace ETLScript
{
    class Program
    {
        static String TOKEN = "AAAAAAITEghMBACgYLOK2ZAqbMUQyp3gp0szaZAkI1YWBqi70J6XL9pMwJv8t2GqmGyDpzSPHM5zUWwZCBeXZAFKWps5dPvpdAiqci7puOwZDZD";

        static void Main(string[] args)
        {
            //String coneccion = "server=10.100.15.18\\sql2008_pruebas;User ID=mafabd03ba_componentes;Password=m@reigu@;database=mafabd03ba;Connection Reset=FALSE;Application Name=empleador-mafas23;Min Pool Size=25;Max Pool Size=3000;MultipleActiveResultSets=true;";
            //System.Data.SqlClient.SqlConnection cnnmafabd03gc = new System.Data.SqlClient.SqlConnection(coneccion);
            String valor = "";
            try
            {
                String jsonPost = facebook();
                Console.WriteLine(jsonPost);
                JavaScriptSerializer serializer = new JavaScriptSerializer();

                FacebookPost.RootObject post = new FacebookPost.RootObject();
                post = serializer.Deserialize<FacebookPost.RootObject>(jsonPost);
                
                FacebookPost.From elemento = null;
                
                //cnnmafabd03gc.Open();
                //int rta = validarAfiliacionAsofondos(valor, valor, cnnmafabd03gc);
            }
            catch{
            }
            
            
        }



        /// <summary>
        /// valida si un cotizante esta afiliado a asofondos y retorna el código de estado de la administradora si lo está  o retorna cero si no lo está
        /// </summary>
        public static int validarAfiliacionAsofondos(FacebookPost post, string no_id, SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable("Empleados");
            int valor = 0;
            try
            {
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parametro = new SqlParameter();
                cmd.Parameters.Add(new SqlParameter("@@nombre", post));
                
                cmd.CommandText = "procAdministradoras_asofondos_estadoSELECT";
                cmd.CommandTimeout = 0;
                da.SelectCommand = cmd;
                valor = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo realizar la consulta, Intentelo nuevamente", ex);
            }
            return valor;

        }

        static String facebook()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://graph.facebook.com/115605765132094?fields=posts.fields(message,comments.fields(message,from))&access_token="+TOKEN);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.Accept = "application/json";

            string text;
            var response = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
            Console.WriteLine(text);
            return text;
        }
    }
}
