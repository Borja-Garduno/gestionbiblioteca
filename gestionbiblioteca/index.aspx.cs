using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace gestionbiblioteca
{
    public partial class index : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            cargaDatos();
        }

        private void cargaDatos()
        {
            SqlConnection conn = null;
            try
            {
                string cadenaConexion = ConfigurationManager.ConnectionStrings["GESTLIBRERIAConnectionString"].ConnectionString;
                string SQL = "getAllUsuarios";
                conn = new SqlConnection(cadenaConexion);
                conn.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter dAdapter = new SqlDataAdapter(SQL, conn);
                dAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                dAdapter.Fill(ds);
                dt = ds.Tables[0];

                grdv_Usuarios.DataSource = dt;
                grdv_Usuarios.DataBind();
            }
            catch (SqlException ex)
            {
                Console.Error.Write("Excepcion SELECT: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void grdv_Usuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string comand = e.CommandName;
            int index = Convert.ToInt32(e.CommandArgument);
            string codigo = grdv_Usuarios.DataKeys[index].Value.ToString();
            int id = Int32.Parse(codigo);

            switch (comand)
            {
                case "editUsuario":
                    {
                        lblIdUsuario.Text = codigo;
                        SqlConnection conn = null;
                        SqlDataReader reader = null;
                        try
                        {
                            string SQL = "getByIdUsuario";
                            string cadenaConexion = ConfigurationManager.ConnectionStrings["GESTLIBRERIAConnectionString"].ConnectionString;
                            conn = new SqlConnection(cadenaConexion);
                            SqlCommand cmd = new SqlCommand(SQL, conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@codUsuario", id);
                            conn.Open();
                            reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    txtNombreUsuario.Text = reader["nombre"].ToString();
                                    txtApellidosUsuario.Text = reader["apellidos"].ToString();
                                    txtfNacimientoUsuario.Text = reader["fNacimiento"].ToString();
                                    txtUsernameUsuario.Text = reader["username"].ToString();
                                    txtPasswordUsuario.Text = reader["password"].ToString();
                                    //txtBorradoUsuario.Text = reader["borrado"].ToString();
                                }
                            }
                            else
                            {
                                Console.WriteLine("no se han encontrado registro");
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.Error.Write(ex.Message);
                        }
                        finally
                        {
                            reader.Close();
                            conn.Close();
                        }

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script>");
                        sb.Append("$('#crearEditarModal').text('Editar Usuario');");
                        sb.Append("$('#editModal').modal('show');");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MostrarEditar", sb.ToString(), false);
                    }
                    break;

                case "deleteUsuario":
                    {

                        txtIdUsuario.Text = codigo;
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script>");
                        sb.Append("$('#deleteConfirm').modal('show')");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ConfirmarBorrado", sb.ToString(), false);
                    }
                    break;

            }
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            string codigo = lblIdUsuario.Text;
            string nombre = txtNombreUsuario.Text;
            string apellidos = txtApellidosUsuario.Text;
            string fNacimiento = txtfNacimientoUsuario.Text;
            string username = txtUsernameUsuario.Text;
            string password = txtPasswordUsuario.Text;
            string borrado = txtBorradoUsuario.Text;

            string cadenaConexion = ConfigurationManager.ConnectionStrings["GESTLIBRERIAConnectionString"].ConnectionString;
            int cod;

            string SQL = "INSERT INTO usuarios(nombre, apellidos, fNacimiento, username, password, borrado) VALUES(" + nombre + ", " + apellidos + ", " + fNacimiento + ", " + username + ", " + password + ", " + 0 + ")";
            if (Int32.TryParse(codigo, out cod) && cod > -1)
            {
                SQL = "UPDATE usuario SET nombre = UPPER(" + nombre + "), apellidos = UPPER(" + apellidos + "), fNacimiento = " + fNacimiento + ", username = UPPER(" + username + "), password = " + password + ", borrado = " + borrado + " WHERE codUsuario =" + codigo;
            }

            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(cadenaConexion);
                conn.Open();
                SqlCommand sqlcmm = new SqlCommand();
                sqlcmm.Connection = conn;
                sqlcmm.CommandText = SQL;
                sqlcmm.CommandType = CommandType.Text;
                // sqlcmm.CommandType = CommandType.StoredProcedure;
                sqlcmm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.Error.Write("Excepcion Guardar: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            cargaDatos();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script>");
            sb.Append("$('#editModal').modal('hide')");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OcultarCreate", sb.ToString(), false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = null;
            string cadenaConexion = ConfigurationManager.ConnectionStrings["GESTLIBRERIAConnectionString"].ConnectionString;

            string codigo = txtIdUsuario.Text;
            string SQL = "DELETE FROM usuario WHERE codUsuario=" + codigo;
            try
            {
                conn = new SqlConnection(cadenaConexion);
                conn.Open();
                SqlCommand sqlcmm = new SqlCommand();
                sqlcmm.Connection = conn;
                sqlcmm.CommandText = SQL;
                sqlcmm.CommandType = CommandType.Text;
                // sqlcmm.CommandType = CommandType.StoredProcedure;
                sqlcmm.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.Error.Write("Excepcion DELETE: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
            cargaDatos();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script>");
            sb.Append("$('#deleteConfirm').modal('hide')");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OcultarCreate", sb.ToString(), false);
        }

        protected void btncrearUsuario_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            lblIdUsuario.Text = "-1";
            txtNombreUsuario.Text = "";
            sb.Append(@"<script>");
            sb.Append("$('#editModal').modal('show')");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MostrarCreate", sb.ToString(), false);
        }
    }
}
