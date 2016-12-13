using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace P2
{
    class AccesoADatos
    {
        public string stringdeconexion = "";
        public string stringdeconexionpais = "";
        private Word.Application wordApp;
        private Word.Document aDoc;
        private object missing = Type.Missing;//Missing.Value;

        #region inicializacion

        public AccesoADatos(string path)
        {
            stringdeconexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + "\\Programa2.mdb";
        }

        public void accesapais(string path,string periodo, string pais)
        {
            //stringdeconexionpais = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ConfigurationManager.AppSettings["rutaDatos"] + "\\" + periodo + "\\" + pais + ".mdb";
            stringdeconexionpais = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + "\\Datos\\" + periodo + "\\" + pais + ".mdb";
        }

        public void modificar_tablas()
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;
            try
            {
                sql = "alter table solicitudes add column PaisNacimiento TEXT(50)";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                sql = "alter table solicitudes add column MedioSolicitud TEXT(50)";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
            }
            try
            {
                sql = "alter table solicitudes drop constraint PrimaryKey ";
                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
            }
            conn.Close();

        }

        #endregion inicializacion

        #region perfiles

        public void perfiles(ref ComboBox control)
        {

            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT id,perfil  FROM perfiles ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(((string)linea["perfil"]));
                Application.DoEvents();
                //control.Items.Add(new KeyValuePair<string, string>((linea["id"].ToString()), ((string)linea["perfil"])));
            }

        }

        public void usuarios(ref ListBox control)
        {
            Boolean res = false;

            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT IDuser FROM segUsuarios ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["IDuser"]);
                Application.DoEvents();
            }

        }

        public void usuarios(ref ComboBox control)
        {
            Boolean res = false;

            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT IDuser FROM segUsuarios ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["IDuser"]);
                Application.DoEvents();
            }

        }

        /*
         * eliminar_menu_perfil: elimina una opcion de menu asignada al perfil
         * 
         * */
        public void eliminar_menu_perfil(string menu, string perfil)
        {
            int id_menu = id_de_menu(menu);
            int id_perfil = id_de_perfil(perfil);

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "delete from segPerfiles ";
            sql += " where ";
            sql += "idPerfil=" + id_perfil.ToString() + " and opcionMenu=" + id_menu.ToString();

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }

        /*
         * agregar_menu_perfil: agrega una opcion de menu al perfil
         * 
         * */
        public void agregar_menu_perfil(string menu, string perfil)
        {
            int id_menu = 0;
            int id_perfil = id_de_perfil(perfil);
            if (menu.Contains(">"))
            {
                string[] arr = menu.Split('>');

                if (perfil_menu(arr[0], perfil))
                {
                    id_menu = id_de_menu(arr[1].Trim());
                    insert_menu_perfil(id_menu, id_perfil);
                }
                else
                {
                    id_menu = id_de_menu(arr[0].Trim());
                    insert_menu_perfil(id_menu, id_perfil);
                    id_menu = id_de_menu(arr[1].Trim());
                    insert_menu_perfil(id_menu, id_perfil);
                }
            }
            else
            {
                id_menu = id_de_menu(menu.Trim());
                insert_menu_perfil(id_menu, id_perfil);
            }
        }

        public void insert_menu_perfil(int id_menu, int id_perfil)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "insert into segPerfiles ";
            sql += " (idPerfil,opcionMenu) values (";
            sql += id_perfil.ToString() + ", " + id_menu.ToString() + ")";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public bool perfil_menu(string menu, string perfil)
        {
            bool res = false;
            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT [menu-aplicacion].nombre ";
            sql += "FROM segPerfiles, [menu-aplicacion], perfiles ";
            sql += "WHERE perfiles.perfil='" + perfil + "' and ";
            sql += " perfiles.id=segperfiles.idperfil and ";
            sql += " [menu-aplicacion].id=segPerfiles.opcionMenu and ";
            sql += " [menu-aplicacion].texto='" + menu + "'";


            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();
            Application.DoEvents();
            res = (oDs.Tables[0].Rows.Count > 0);

            return res;
        }

        public bool usuario_tiene_perfil(string usuario)
        {
            bool res = false;
            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT perfil ";
            sql += "FROM segUsuarios ";
            sql += "WHERE IDuser='" + usuario + "' and perfil<>0";


            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();
            Application.DoEvents();
            res = (oDs.Tables[0].Rows.Count > 0);

            return res;
        }

        public int id_de_menu(string menu)
        {
            int res = 0;

            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT [menu-aplicacion].id ";
            sql += "FROM [menu-aplicacion] ";
            sql += "WHERE [menu-aplicacion].texto='" + menu + "'";


            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            DataRow linea = oDs.Tables[0].Rows[0];


            res = (int)linea["ID"];


            return res;
        }

        public int id_de_perfil(string perfil)
        {
            int res = 0;

            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT perfiles.id ";
            sql += "FROM perfiles ";
            sql += "WHERE perfiles.perfil='" + perfil + "'";


            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            if (oDs.Tables[0].Rows.Count > 0)
            {
                DataRow linea = oDs.Tables[0].Rows[0];


                res = (int)linea["ID"];
            }

            return res;
        }

        public void agregar_perfil_a_usuario(string usuario, string perfil)
        {
            int id_perfil = id_de_perfil(perfil);

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "update segUsuarios ";
            sql += " set perfil=" + id_perfil.ToString();
            sql += " where IDuser='" + usuario + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_perfil_a_usuario(string usuario, string perfil)
        {
            int id_perfil = id_de_perfil(perfil);

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "update segUsuarios ";
            sql += " set perfil=0";
            sql += " where IDuser='" + usuario + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void agregar_perfil(string perfil)
        {
            if (id_de_perfil(perfil) == 0)
            {
                OleDbConnection conn;
                conn = new OleDbConnection(stringdeconexion);
                conn.Open();
                String sql;

                sql = "insert into perfiles ";
                sql += " (perfil) values ('";
                sql += perfil + "')";

                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void agregar_usuario(string nombre, string clave)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "INSERT INTO [segUsuarios] (IDuser,clave,nivel,perfil)";
            sql += " VALUES (";
            sql += "'" + nombre + "','" + clave + "',0,0";
            sql += ")";
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        #endregion perfiles

        #region combos_listboxes

        public void paises(ref ComboBox control, ref string db, ref string ubic, string annio)
        {
            Boolean res = false;

            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Pais,NombreBD,Ubica,Periodo FROM Paises where periodo='" + annio + "'", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["Pais"]);
                ubic = linea["Ubica"].ToString();
                ubic += "\\";
                ubic += linea["Periodo"].ToString();
                ubic += "\\";
                db = linea["NombreBD"].ToString();
                Application.DoEvents();
            }

        }

        public void estados(ref ComboBox control,string pais)
        {
            DataSet oDs;

            control.Items.Clear();

            string idpais = id_de_pais(pais);

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Descripcion FROM Estado where IDPais="+idpais, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["Descripcion"]);
                Application.DoEvents();
            }

        }

        public void paises(ref ComboBox control)
        {
            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Nombre FROM Pais", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["Nombre"]);
                Application.DoEvents();
            }

        }

        public void ciudades(ref ComboBox control, string pais)
        {
           
            DataSet oDs;

            control.Items.Clear();

            string idpais = id_de_pais(pais);

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Descripcion FROM Estado where IDPais=" + idpais + "'", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["Descripcion"]);
                Application.DoEvents();
            }

        }

        public void causas(ref ComboBoxWrap control)
        {
            try
            {
                DataSet oDs;

                control.Items.Clear();

                OleDbConnection oConn =
                        new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

                OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT distinct causas FROM Solicitudes ", oConn);
                oConn.Open();
                oDs = new DataSet();
                oCmd.Fill(oDs);
                oConn.Close();

                foreach (DataRow linea in oDs.Tables[0].Rows)
                {

                    control.Items.Add(linea["causas"]);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
            }

        }

        public void ciudades_solicitudes(ref ComboBox control)
        {
            try
            {
                DataSet oDs;

                control.Items.Clear();

                OleDbConnection oConn =
                        new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

                OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT distinct ciudad FROM Solicitudes ", oConn);
                oConn.Open();
                oDs = new DataSet();
                oCmd.Fill(oDs);
                oConn.Close();

                foreach (DataRow linea in oDs.Tables[0].Rows)
                {

                    control.Items.Add(linea["ciudad"]);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
            }

        }

        public void estado_solicitudes(ref ComboBox control)
        {
            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT distinct estado FROM Solicitudes ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["estado"]);
                Application.DoEvents();
            }

        }

        public void parentesco(ref ComboBox control)
        {
            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT distinct concepto FROM Parentesco ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["concepto"]);
                Application.DoEvents();
            }

        }

        public void medio(ref ComboBox control)
        {
            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT distinct descripcion FROM Medio ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["descripcion"]);
                Application.DoEvents();
            }

        }

        public void annios(ref ComboBox control)
        {
            Boolean res = false;

            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            //OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Year FROM año ", oConn);
            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Periodo FROM Periodo ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                //control.Items.Add(linea["Year"]);
                control.Items.Add(linea["Periodo"]);
                Application.DoEvents();
            }

        }

        public void usuarios_por_perfil(ref ListBox control, string perfil)
        {
            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT segUsuarios.IDuser  FROM segUsuarios,perfiles where perfiles.perfil='" + perfil + "' and segUsuarios.perfil = perfiles.id ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["IDuser"]);
                Application.DoEvents();
            }

        }

        public void datos_de_usuario(ref ListBox control)
        {
            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT IDuser  FROM segUsuarios where perfil=null ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["IDuser"]);
                Application.DoEvents();
            }

        }

        #endregion combos_listboxes

        #region menu_aplicacion

        public void agregar_menu(string nombre, string texto)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "INSERT INTO [menu-aplicacion] (submenu,nombre,texto)";
            sql += " VALUES (";
            sql += "0,'" + nombre + "','" + texto + "'";
            sql += ")";
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void agregar_submenu(string nombre, string texto)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "INSERT INTO [menu-aplicacion] (submenu,nombre,texto)";
            sql += " VALUES (";
            sql += "-1,'" + nombre + "','" + texto + "'";
            sql += ")";
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificar_menu(ref MenuStrip menu, int usuario)
        {
            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT [menu-aplicacion].nombre ";
            sql += "FROM segPerfiles, [SegUsuarios], [menu-aplicacion] ";
            sql += "WHERE [SegUsuarios].id=" + usuario + " and ";
            sql += " segPerfiles.idPerfil=[SegUsuarios].perfil and segPerfiles.opcionMenu=[menu-aplicacion].id";

            /*string sql = "SELECT [menu-aplicacion].nombre ";
            sql += "FROM segPerfiles, [Usuario-pais-perfil], [menu-aplicacion] ";
            sql += "WHERE [Usuario-pais-perfil].IDusuario=" + usuario + " and ";
            sql += " segPerfiles.idPerfil=[Usuario-pais-perfil].idperfil and segPerfiles.opcionMenu=[menu-aplicacion].id";*/

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            if (oDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow linea in oDs.Tables[0].Rows)
                {
                    foreach (ToolStripMenuItem elem in menu.Items)
                    {

                        foreach (ToolStripItem item in elem.DropDownItems)
                        {
                            try
                            {
                                if (item.IsOnDropDown)
                                    foreach (ToolStripItem item2 in ((ToolStripMenuItem)item).DropDownItems)
                                    {
                                        if (((string)linea["nombre"]).Equals((string)item2.Name))
                                        {
                                            item2.Visible = true;
                                        }
                                    }
                            }
                            catch { }
                            if (((string)linea["nombre"]).Equals((string)item.Name))
                            {
                                item.Visible = true;
                            }
                        }
                    }

                }
            }
        }


        #endregion menu_aplicacion

        #region usuario_y_conversiones_de_id_a_string

        public Boolean autenticacion(string user, string clave, out int usuario)
        {
            Boolean res = false;

            DataSet oDs;
            usuario = -1;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT id FROM SegUsuarios where IDuser='" + user + "' and Clave='" + clave + "'", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            if (oDs.Tables[0].Rows.Count > 0)
            {
                usuario = (int)oDs.Tables[0].Rows[0]["id"];
                res = true;
            }

            return res;
        }

        public string nombre_de_usuario(int id)
        {
            Boolean res = false;

            DataSet oDs;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT IDuser FROM segUsuarios where id=" + id.ToString(), oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            return oDs.Tables[0].Rows[0]["IDuser"].ToString();

        }

        public void annios(ref DataTable control)
        {
            Boolean res = false;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Periodo FROM Periodo ", oConn);
            oConn.Open();
            control = new DataTable();
            oCmd.Fill(control);
            oConn.Close();
        }

        public bool existe_annio(string annio)
        {
            Boolean res = false;

            DataSet oDs;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            string sql = "SELECT annio FROM [annio] where annio=" + annio;
            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            res = oDs.Tables[0].Rows.Count > 0;

            return res;
        }

        public string id_de_pais(string pais)
        {
            string res = "";

            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT ID ";
            sql += "FROM Pais ";
            sql += "WHERE Pais.Nombre='" + pais + "'";


            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            if (oDs.Tables[0].Rows.Count > 0)
            {
                DataRow linea = oDs.Tables[0].Rows[0];


                res = linea["ID"].ToString();
            }

            return res;
        }

        public string id_de_requisitoria(string concepto)
        {
            string res = "";

            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT Codigo ";
            sql += "FROM Requisitorias ";
            sql += "WHERE Concepto='" + concepto + "'";


            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            if (oDs.Tables[0].Rows.Count > 0)
            {
                DataRow linea = oDs.Tables[0].Rows[0];


                res = linea["Codigo"].ToString();
            }

            return res;
        }

        public string id_de_estado(string estado, string idpais)
        {
            string res = "";

            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT Estado.IDestado ";
            sql += "FROM Estado ";
            sql += "WHERE Estado.IDPais=" + idpais + " AND Estado.Descripcion='" + estado + "'";


            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            if (oDs.Tables[0].Rows.Count > 0)
            {
                DataRow linea = oDs.Tables[0].Rows[0];


                res = (string)linea["IDestado"];
            }

            return res;
        }

        public void id_marca(ref string codigo_marca, string marca)
        {
            /*
             * Este metodo Busca en la tabla marcas el concepto de la marca y busca el codigo 
             * en la base de datos programa2
             * */
            DataSet oDs, oDs1;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            try
            {
                string sql1 = "SELECT Codigo from Marcas where Concepto='" + marca + "'";

                OleDbDataAdapter oCmd = new OleDbDataAdapter(sql1, oConn);
                oConn.Open();
                oDs1 = new DataSet();
                oCmd.Fill(oDs1);
                oConn.Close();
                codigo_marca = oDs1.Tables[0].Rows[0]["Codigo"].ToString();
            }
            catch (Exception ex) { }
            
        }

        #endregion usuario_y_conversiones_de_id_a_string

        #region resolucion

        public void marcas(ref ComboBox control, string codigo)
        {
            /*
             * Este metodo tiene dos efectos:
             * carga los datos dentro del combo box si codigo=""
             * y es capaz de seleccionar una marca dentro del combo box dado el codigo si codigo !=""
             * */
            DataSet oDs;
            try
            {
                if (codigo.Length == 0) control.Items.Clear();
            }
            catch (Exception ex)
            {
                control.Items.Clear();
                codigo = "";
            }

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            string sql = "SELECT concepto FROM marcas ";
            if (codigo.Length > 0) sql += "where codigo='" + codigo + "'";

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            if (codigo.Length == 0)
                foreach (DataRow linea in oDs.Tables[0].Rows)
                {

                    control.Items.Add(linea["concepto"]);
                    Application.DoEvents();
                }
            else
                control.Text = oDs.Tables[0].Rows[0]["concepto"].ToString();

        }

        public void requisitorias(ref ComboBox control, string codigo)
        {
            /*
             * Este metodo tiene dos efectos:
             * carga los datos dentro del combo box si codigo=""
             * y es capaz de seleccionar una requisitoria dentro del combo box dado el codigo si codigo !=""
             * */
            DataSet oDs;

            if (codigo.Length == 0) control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            string sql = "SELECT concepto FROM requisitorias ";
            if (codigo.Length > 0) sql += "where codigo='" + codigo + "'";

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            if (codigo.Length == 0)
                foreach (DataRow linea in oDs.Tables[0].Rows)
                {

                    control.Items.Add(linea["concepto"]);
                    Application.DoEvents();
                }
            else
                control.Text = oDs.Tables[0].Rows[0]["concepto"].ToString();

        }

        public void status(ref ComboBox control, string IdStatus)
        {
            /*
             * Este metodo tiene dos efectos:
             * carga los datos dentro del combo box si codigo=""
             * y es capaz de seleccionar un status dentro del combo box dado el codigo si codigo !=""
             * */
            DataSet oDs;
            try
            {
                if (IdStatus.Length == 0) control.Items.Clear();
            }
            catch (Exception ex)
            {
                control.Items.Clear();
                IdStatus = "";
            }
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            string sql = "SELECT concepto FROM Status ";
            if (IdStatus.Length > 0) sql += "where Codigo='" + IdStatus + "'";

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            if (IdStatus.Length == 0)
                foreach (DataRow linea in oDs.Tables[0].Rows)
                {

                    control.Items.Add(linea["concepto"]);
                    Application.DoEvents();
                }
            else
                control.Text = oDs.Tables[0].Rows[0]["concepto"].ToString();

        }

        public void cuantia_en_euros(ref TextBox control, string periodo, string numexp)
        {
            DataSet oDs;

            control.Text="";
            try
            {
                OleDbConnection oConn =
                        new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                string sql = "SELECT MontoE FROM Resolucion where Periodo='" + periodo + "' and Numexp='" + numexp + "'";
                OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
                oConn.Open();
                oDs = new DataSet();
                oCmd.Fill(oDs);
                oConn.Close();

                foreach (DataRow linea in oDs.Tables[0].Rows)
                {

                    control.Text = linea["MontoE"].ToString();
                    Application.DoEvents();
                }
            }
            catch (Exception ex) { }
        }

        public void cuantia_en_dolares(ref TextBox control, string periodo, string numexp)
        {
            DataSet oDs;

            control.Text = "";
            try
            {
                OleDbConnection oConn =
                        new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                string sql = "SELECT MontoD FROM Resolucion where Periodo='" + periodo + "' and Numexp='" + numexp + "'";
                OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
                oConn.Open();
                oDs = new DataSet();
                oCmd.Fill(oDs);
                oConn.Close();

                foreach (DataRow linea in oDs.Tables[0].Rows)
                {

                    control.Text = linea["MontoD"].ToString();
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            { }
        }

        public void cuantia_en_moneda_local(ref TextBox control, string periodo, string numexp)
        {
            DataSet oDs;

            control.Text = "";
            try
            {
                OleDbConnection oConn =
                        new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                string sql = "SELECT MontoL FROM Resolucion where Periodo='" + periodo + "' and Numexp='" + numexp + "'";
                OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
                oConn.Open();
                oDs = new DataSet();
                oCmd.Fill(oDs);
                oConn.Close();

                foreach (DataRow linea in oDs.Tables[0].Rows)
                {

                    control.Text = linea["MontoL"].ToString();
                    Application.DoEvents();
                }
            }
            catch (Exception ex) { }
        }

        public void fecha_resolucion(ref DateTimePicker control, string periodo, string numexp)
        {
            DataSet oDs;

            control.Text = "";
            try
            {
                OleDbConnection oConn =
                        new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                string sql = "SELECT FResol FROM Resolucion where Periodo='" + periodo + "' and Numexp='" + numexp + "'";
                OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
                oConn.Open();
                oDs = new DataSet();
                oCmd.Fill(oDs);
                oConn.Close();

                foreach (DataRow linea in oDs.Tables[0].Rows)
                {
                    control.Value = Convert.ToDateTime(linea["FResol"].ToString());
                    Application.DoEvents();
                }
            }
            catch (Exception ex) { }
        }

        public bool existe_expediente(string tabla,string periodo, string expediente)
        {
            Boolean res = false;

            DataSet oDs;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "SELECT periodo FROM "+tabla+" where periodo='" + periodo + "' and Numexp='"+expediente+"'";
            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            Application.DoEvents();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            res = oDs.Tables[0].Rows.Count > 0;

            return res;
        }

        private string monto_escrito(string numero,string pais)
        {
            Conv c = new Conv();
            return c.enletras(numero,pais);
        }

        private void guardar_en_tabla_resolucion(string periodo, string expediente,string cuantia_euros, string cuantia_dolares, string cuantia_moneda_local,
            string fecha_resolucion,string usuario,string pais)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;
            if (existe_expediente("resolucion", periodo, expediente))
            {
                //modificar el registro
                sql = "UPDATE Resolucion set MontoE=" + cuantia_euros + ", MontoD=" + cuantia_dolares + ", MontoL=" + cuantia_moneda_local;
                sql += ", MontoEsc='" + monto_escrito(cuantia_moneda_local,pais) + "', FResol='" + fecha_resolucion + "',FecAct='" + DateTime.Now.ToShortDateString() + "',Usuario='" + usuario + "' WHERE Periodo='" + periodo + "' AND Numexp='" + expediente + "'";

                
            }
            else
            {
                //crear el registro
                sql = "INSERT Into Resolucion (Periodo,Numexp,MontoE,MOntoD,MontoL,MontoEsc,FResol,FEcAct,Usuario) values (";
                sql += "'" + periodo + "','" + expediente + "'," + cuantia_euros + "," + cuantia_dolares + "," + cuantia_moneda_local + ",'" + monto_escrito(cuantia_moneda_local,pais) +"','"+ fecha_resolucion + "','" + DateTime.Now.ToShortDateString() + "','" + usuario + "')";
            }
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                Application.DoEvents();
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public bool guardar_resolucion(string periodo, string expediente,string codigo_marca, string fecha_marca,
                string cuantia_euro, string tasa_euro_dolar, string cuantia_dolar, string dolar_moneda_local, string cuantia_moneda_local,
                string fec_resolucion, string codigo_status, string fec_status,
                string fec_fallecimiento, string beneficiario,string usuario,string pais)
        {
            bool res = false;
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            if (existe_expediente("solicitudes",periodo, expediente))
            {
                guardar_en_tabla_resolucion(periodo, expediente, cuantia_euro, cuantia_dolar, cuantia_moneda_local, fec_resolucion,usuario,pais);

                sql = "UPDATE Solicitudes set Marca='" + codigo_marca + "', FecMarca='"+fecha_marca+"',";
                sql += "CambioED="+tasa_euro_dolar+", CambioDB="+dolar_moneda_local+",IdStatus='"+codigo_status+"',FecStatus='"+fec_status;
                sql += "',FecFallece='" + fec_fallecimiento + "',BenefCh='" + beneficiario + "'";
                sql += ", Usuario='" + usuario + "',FecAct='" + DateTime.Now.ToShortDateString() + "'";
                sql += " WHERE Periodo='" + periodo + "' AND NumExp='" + expediente + "'";


                using (OleDbCommand cmd = new OleDbCommand(sql, conn))
                {
                    Application.DoEvents();
                    cmd.ExecuteNonQuery();
                }
                res = true;
            }
            else
            {
                res=false;
            }
            conn.Close();
            return res;
        }

        public void FindAndReplace(object findText, object replaceText)
        {
            this.findAndReplace(wordApp, findText, replaceText);
        }

        private void findAndReplace(Word.Application wordApp, object findText, object replaceText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
            ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms,
            ref forward, ref wrap, ref format, ref replaceText, ref replace,
            ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }

        public void SaveDocument()
        {
            try
            {
                aDoc.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error durante el proceso. Descripcion: " + ex.Message, "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CloseDocument()
        {
            object saveChanges = Word.WdSaveOptions.wdSaveChanges;
            wordApp.Quit(ref saveChanges, ref missing, ref missing);
        }
        
        private DataSet Res(string periodo,Boolean favorables)
        {
            DataSet oDs;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

            string sql = "SELECT Solicitudes.Nombres & ' ' & Solicitudes.Apellidos AS Nombre, Solicitudes.RegEnt, Resolucion.RegSal, Resolucion.FecSal AS FechaSal, Solicitudes.FecEnt AS FechaEnt, Solicitudes.FecMarca AS FechaMar, Resolucion.Pais, Resolucion.Numexp, Solicitudes.Domicilio AS Dir1, Solicitudes.Localidad AS Dir2, Solicitudes.Ciudad & ', ' & Solicitudes.Estado AS Dir3, Solicitudes.NumInscrip, Solicitudes.Pasaporte, Resolucion.Fresol AS FechaRes, Solicitudes.FecIni AS FechaIni, Solicitudes.FecFin AS FechaFin, Resolucion.Causa, Resolucion.Orden, Solicitudes.FecFallece AS FechaFall, Resolucion.Literal, Resolucion.Norma, Resolucion.MontoEsc, Resolucion.MontoE AS MtoEuro, Resolucion.MontoD AS MtoDolar, BenefCh AS BenefCh, Resolucion.MontoL AS MtoLocal ";
                   sql+="FROM Resolucion INNER JOIN Solicitudes ON (Resolucion.Periodo = Solicitudes.Periodo) AND (Resolucion.Numexp = Solicitudes.NumExp) ";
                   sql+="WHERE (((Solicitudes.Periodo)='"+periodo+"')";
            if (favorables)
                sql+=" AND ((Resolucion.Marca)>='01' And (Resolucion.Marca)<='10')) ";
            else
                sql += " AND ((Resolucion.Marca)>'10'))";

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            return oDs;
        }

        private DataSet Res_filtro(int liminf, int limsup, DateTime fechaIni, DateTime fechafin,string periodo, Boolean favorables)
        {
            string fecha1 = fechaIni.Year.ToString()+"/"+fechaIni.Month.ToString()+"/"+fechaIni.Day.ToString();
            string fecha2 = fechafin.Year.ToString()+"/"+fechafin.Month.ToString()+"/"+fechafin.Day.ToString();

            DataSet oDs;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

            string sql = "SELECT Solicitudes.Nombres & ' ' & Solicitudes.Apellidos AS Nombre, Solicitudes.RegEnt, Resolucion.RegSal, Resolucion.FecSal AS FechaSal, Solicitudes.FecEnt AS FechaEnt, Solicitudes.FecMarca AS FechaMar, Resolucion.Pais, Resolucion.Numexp, Solicitudes.Domicilio AS Dir1, Solicitudes.Localidad AS Dir2, Solicitudes.Ciudad & ', ' & Solicitudes.Estado AS Dir3, Solicitudes.NumInscrip, Solicitudes.Pasaporte, Resolucion.Fresol AS FechaRes, Solicitudes.FecIni AS FechaIni, Solicitudes.FecFin AS FechaFin, Resolucion.Causa, Resolucion.Orden, Solicitudes.FecFallece AS FechaFall, Resolucion.Literal, Resolucion.Norma, Resolucion.MontoEsc, Resolucion.MontoE AS MtoEuro, Resolucion.MontoD AS MtoDolar, BenefCh AS BenefCh, Resolucion.MontoL AS MtoLocal ";
            sql += "FROM Resolucion INNER JOIN Solicitudes ON (Resolucion.Periodo = Solicitudes.Periodo) AND (Resolucion.Numexp = Solicitudes.NumExp) ";
            sql += "WHERE (((Solicitudes.Periodo)='" + periodo + "')";
            if (favorables)
                sql += " AND ((Resolucion.Marca)>='01' And (Resolucion.Marca)<='10')) ";
            else
                sql += " AND ((Resolucion.Marca)>'10'))";
            if (liminf > 0)
                sql += " AND (Val( Resolucion.Numexp) >= " + liminf.ToString() + " AND Val( Resolucion.Numexp)<=" + limsup.ToString()+")";
            else
                sql += " AND (Resolucion.Fresol >= #" + fecha1 + "# AND Resolucion.Fresol <= #" + fecha2 + "#)";

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            return oDs;
        }

        public void Resoluciones_favorables(string periodo, String pais,SaveFileDialog d)
        {
            string[] texto = new string[13];
            string[] textoabuscar = new string[13];
            string filedest="";
            string fileName = "";
            DataSet data = Res(periodo,true);
            if (pais.Equals("VENEZUELA"))
            {
                filedest = "Resolucion_favorable.doc";
                fileName = "Resolucion Favorable.doc";
            }
            if (pais.Equals("DOMINICANA"))
            {
                filedest = "Resolucion_favorable RD.doc";
                fileName = "Resolucion Favorable RD.doc";
            }
            if (pais.Equals("COLOMBIA"))
            {
                filedest = "Resolucion_favorable CO.doc";
                fileName = "Resolucion Favorable CO.doc";
            }
            string path = Directory.GetCurrentDirectory()+"\\Documentos\\";
            d.Title = "Indique donde se van a generar los documentos";
            d.FileName = filedest;
            textoabuscar[0] = "<RegSal>";
            textoabuscar[1] = "<FechaSal>";
            textoabuscar[2] = "<Numexp>";
            textoabuscar[3] = "<FechaRes>";
            textoabuscar[4] = "<Nombre>";
            textoabuscar[5] = "<Dir1>";
            textoabuscar[6] = "<Dir2>";
            textoabuscar[7] = "<Dir3>";
            textoabuscar[8] = "<FechaEnt>";
            textoabuscar[9] = "<Pasaporte>";
            textoabuscar[10] = "<Causa>";
            textoabuscar[11] = "<MontoEsc>";
            if (pais.Equals("VENEZUELA"))
                textoabuscar[12] = "<MtoLocal>";
            else
                textoabuscar[12] = "<MtoDolar>";
            if ((d.ShowDialog() == DialogResult.OK))
            {
                foreach (DataRow linea in data.Tables[0].Rows)
                {
                    Application.DoEvents();
                    texto[0] = linea["RegSal"].ToString();
                    try
                    {
                        texto[1] = linea["FechaSal"].ToString().Substring(0, linea["FechaSal"].ToString().Length - 8);
                    }
                    catch (Exception ex)
                    {
                        texto[1] = "";
                    }
                    texto[2] = linea["Numexp"].ToString();
                    try
                    {
                        texto[3] = linea["FechaRes"].ToString().Substring(0, linea["FechaRes"].ToString().Length - 8);
                    }
                    catch (Exception ex)
                    {
                        texto[3] = "";
                    }
                    texto[4] = linea["Nombre"].ToString();
                    texto[5] = linea["Dir1"].ToString();
                    texto[6] = linea["Dir2"].ToString();
                    texto[7] = linea["Dir3"].ToString();
                    try
                    {
                        texto[8] = linea["FechaEnt"].ToString().Substring(0, linea["FechaEnt"].ToString().Length - 8);
                    }
                    catch (Exception ex)
                    {
                        texto[8] = "";
                    }
                    texto[9] = linea["Pasaporte"].ToString();
                    texto[10] = linea["Causa"].ToString();
                    texto[11] = linea["MontoEsc"].ToString();
                    if (pais.Equals("VENEZUELA"))
                        texto[12] = linea["MtoLocal"].ToString();
                    else
                        texto[12] = linea["MtoDolar"].ToString();
                    filedest = d.FileName.Substring(0,d.FileName.Length-4)+ linea["Nombre"] + ".doc";
                    string sourceFile = System.IO.Path.Combine(path, fileName);
                    /*destFile = System.IO.Path.Combine(path, filedest);*/
                    System.IO.File.Copy(sourceFile, filedest, true);

                    wordApp = new Word.Application();

                    if (File.Exists(filedest))
                    {
                        
                        object readOnly = false;
                        object isVisible = true;
                        object destfile = (object)filedest;

                        wordApp.Visible = false;
                        aDoc = wordApp.Documents.Open(ref destfile, ref missing,
                        ref readOnly, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref isVisible, ref missing, ref missing,
                        ref missing, ref missing);
                        aDoc.Activate();

                        for (int i = 0; i < 13; i++)
                        {
                            Application.DoEvents();
                            FindAndReplace(((object)textoabuscar[i]), ((object)texto[i]));
                        }

                        SaveDocument();
                        CloseDocument();
                        //MessageBox.Show("Documento generado satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("El archivo destino no se creó.", "Sin archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void favorables_filtro(int desde, int hasta, 
                                      DateTime fechaini, DateTime fechafin,
                                      string periodo, String pais, SaveFileDialog d)
        {
            string[] texto = new string[13];
            string[] textoabuscar = new string[13];
            string filedest = "";
            string fileName = "";
            DataSet data = Res_filtro(desde,hasta,fechaini,fechafin,periodo, true);
            if (pais.Equals("VENEZUELA"))
            {
                filedest = "Resolucion_favorable.doc";
                fileName = "Resolucion Favorable.doc";
            }
            if (pais.Equals("DOMINICANA"))
            {
                filedest = "Resolucion_favorable RD.doc";
                fileName = "Resolucion Favorable RD.doc";
            }
            if (pais.Equals("COLOMBIA"))
            {
                filedest = "Resolucion_favorable CO.doc";
                fileName = "Resolucion Favorable CO.doc";
            }
            string path = Directory.GetCurrentDirectory() + "\\Documentos\\";
            d.Title = "Indique donde se van a generar los documentos";
            d.FileName = filedest;
            textoabuscar[0] = "<RegSal>";
            textoabuscar[1] = "<FechaSal>";
            textoabuscar[2] = "<Numexp>";
            textoabuscar[3] = "<FechaRes>";
            textoabuscar[4] = "<Nombre>";
            textoabuscar[5] = "<Dir1>";
            textoabuscar[6] = "<Dir2>";
            textoabuscar[7] = "<Dir3>";
            textoabuscar[8] = "<FechaEnt>";
            textoabuscar[9] = "<Pasaporte>";
            textoabuscar[10] = "<Causa>";
            textoabuscar[11] = "<MontoEsc>";
            if (pais.Equals("VENEZUELA"))
                textoabuscar[12] = "<MtoLocal>";
            else
                textoabuscar[12] = "<MtoDolar>";
            if ((d.ShowDialog() == DialogResult.OK))
            {
                foreach (DataRow linea in data.Tables[0].Rows)
                {
                    Application.DoEvents();
                    texto[0] = linea["RegSal"].ToString();
                    texto[1] = linea["FechaSal"].ToString().Substring(0, linea["FechaSal"].ToString().Length - 8);
                    texto[2] = linea["Numexp"].ToString();
                    texto[3] = linea["FechaRes"].ToString().Substring(0, linea["FechaRes"].ToString().Length - 8);
                    texto[4] = linea["Nombre"].ToString();
                    texto[5] = linea["Dir1"].ToString();
                    texto[6] = linea["Dir2"].ToString();
                    texto[7] = linea["Dir3"].ToString();
                    texto[8] = linea["FechaEnt"].ToString().Substring(0, linea["FechaEnt"].ToString().Length - 8);
                    texto[9] = linea["Pasaporte"].ToString();
                    texto[10] = linea["Causa"].ToString();
                    texto[11] = linea["MontoEsc"].ToString();
                    if (pais.Equals("VENEZUELA"))
                        texto[12] = linea["MtoLocal"].ToString();
                    else
                        texto[12] = linea["MtoDolar"].ToString();
                    filedest = d.FileName.Substring(0, d.FileName.Length - 4) + linea["Nombre"] + ".doc";
                    string sourceFile = System.IO.Path.Combine(path, fileName);
                    /*destFile = System.IO.Path.Combine(path, filedest);*/
                    System.IO.File.Copy(sourceFile, filedest, true);

                    wordApp = new Word.Application();

                    if (File.Exists(filedest))
                    {

                        object readOnly = false;
                        object isVisible = true;
                        object destfile = (object)filedest;

                        wordApp.Visible = false;
                        aDoc = wordApp.Documents.Open(ref destfile, ref missing,
                        ref readOnly, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref isVisible, ref missing, ref missing,
                        ref missing, ref missing);
                        aDoc.Activate();

                        for (int i = 0; i < 13; i++)
                        {
                            Application.DoEvents();
                            FindAndReplace(((object)textoabuscar[i]), ((object)texto[i]));
                        }

                        SaveDocument();
                        CloseDocument();
                        //MessageBox.Show("Documento generado satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("El archivo destino no se creó.", "Sin archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void Resoluciones_denegatorias(string periodo, String pais, SaveFileDialog d)
        {
            string[] texto = new string[13];
            string[] textoabuscar = new string[13];
            string filedest = "";
            string fileName = "";
            DataSet data = Res(periodo, false);
            if (pais.Equals("VENEZUELA"))
            {
                filedest = "Resolucion_Denegatoria.doc";
                fileName = "Resolucion Denegatoria.doc";
            }
            if (pais.Equals("DOMINICANA"))
            {
                filedest = "Resolucion_Denegatoria RD.doc";
                fileName = "Resolucion Denegatoria RD.doc";
            }
            if (pais.Equals("COLOMBIA"))
            {
                filedest = "Resolucion_Denegatoria CO.doc";
                fileName = "Resolucion Denegatoria CO.doc";
            }
            string path = Directory.GetCurrentDirectory() + "\\Documentos\\";
            d.Title = "Indique donde se van a generar los documentos";
            d.FileName = filedest;
            textoabuscar[0] = "<RegSal>";
            textoabuscar[1] = "<FechaSal>";
            textoabuscar[2] = "<Numexp>";
            textoabuscar[3] = "<FechaRes>";
            textoabuscar[4] = "<Nombre>";
            textoabuscar[5] = "<Dir1>";
            textoabuscar[6] = "<Dir2>";
            textoabuscar[7] = "<Dir3>";
            textoabuscar[8] = "<FechaEnt>";
            textoabuscar[9] = "<Pasaporte>";
            textoabuscar[10] = "<Causa>";
            textoabuscar[11] = "<Norma>";
            /*if (pais.Equals("VENEZUELA"))
                textoabuscar[12] = "<MtoLocal>";
            else
                textoabuscar[12] = "<MtoDolar>";*/
            if ((d.ShowDialog() == DialogResult.OK))
            {
                foreach (DataRow linea in data.Tables[0].Rows)
                {
                    Application.DoEvents();
                    texto[0] = linea["RegSal"].ToString();
                    try
                    {
                        texto[1] = linea["FechaSal"].ToString().Substring(0, linea["FechaSal"].ToString().Length - 8);
                    }
                    catch (Exception ex)
                    {
                        texto[1] = "";
                    }
                    texto[2] = linea["Numexp"].ToString();
                    try
                    {
                        texto[3] = linea["FechaRes"].ToString().Substring(0, linea["FechaRes"].ToString().Length - 8);
                    }
                    catch (Exception ex)
                    {
                        texto[3] = "";
                    }
                    texto[4] = linea["Nombre"].ToString();
                    texto[5] = linea["Dir1"].ToString();
                    texto[6] = linea["Dir2"].ToString();
                    texto[7] = linea["Dir3"].ToString();
                    try
                    {
                        texto[8] = linea["FechaEnt"].ToString().Substring(0, linea["FechaEnt"].ToString().Length - 8);
                    }
                    catch (Exception ex)
                    {
                        texto[8] = "";
                    }
                    texto[9] = linea["Pasaporte"].ToString();
                    texto[10] = linea["Causa"].ToString();
                    texto[11] = linea["Norma"].ToString();
                    /*if (pais.Equals("VENEZUELA"))
                        texto[12] = linea["MtoLocal"].ToString();
                    else
                        texto[12] = linea["MtoDolar"].ToString();*/
                    filedest = d.FileName.Substring(0, d.FileName.Length - 4) + linea["Nombre"] + ".doc";
                    string sourceFile = System.IO.Path.Combine(path, fileName);
                    /*destFile = System.IO.Path.Combine(path, filedest);*/
                    System.IO.File.Copy(sourceFile, filedest, true);

                    wordApp = new Word.Application();

                    if (File.Exists(filedest))
                    {

                        object readOnly = false;
                        object isVisible = true;
                        object destfile = (object)filedest;

                        wordApp.Visible = false;
                        aDoc = wordApp.Documents.Open(ref destfile, ref missing,
                        ref readOnly, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref isVisible, ref missing, ref missing,
                        ref missing, ref missing);
                        aDoc.Activate();

                        for (int i = 0; i < 12; i++)
                        {
                            Application.DoEvents();
                            FindAndReplace(((object)textoabuscar[i]), ((object)texto[i]));
                        }

                        SaveDocument();
                        CloseDocument();
                        //MessageBox.Show("Documento generado satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("El archivo destino no se creó.", "Sin archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void Resoluciones_desfavorables(int desde, int hasta,
                                      DateTime fechaini, DateTime fechafin,
                                      string periodo, String pais, SaveFileDialog d)
        {
            string[] texto = new string[13];
            string[] textoabuscar = new string[13];
            string filedest = "";
            string fileName = "";
            DataSet data = Res_filtro(desde,hasta,fechaini,fechafin,periodo, false);
            if (pais.Equals("VENEZUELA"))
            {
                filedest = "Resolucion_Denegatoria.doc";
                fileName = "Resolucion Denegatoria.doc";
            }
            if (pais.Equals("DOMINICANA"))
            {
                filedest = "Resolucion_Denegatoria RD.doc";
                fileName = "Resolucion Denegatoria RD.doc";
            }
            if (pais.Equals("COLOMBIA"))
            {
                filedest = "Resolucion_Denegatoria CO.doc";
                fileName = "Resolucion Denegatoria CO.doc";
            }
            string path = Directory.GetCurrentDirectory() + "\\Documentos\\";
            d.Title = "Indique donde se van a generar los documentos";
            d.FileName = filedest;
            textoabuscar[0] = "<RegSal>";
            textoabuscar[1] = "<FechaSal>";
            textoabuscar[2] = "<Numexp>";
            textoabuscar[3] = "<FechaRes>";
            textoabuscar[4] = "<Nombre>";
            textoabuscar[5] = "<Dir1>";
            textoabuscar[6] = "<Dir2>";
            textoabuscar[7] = "<Dir3>";
            textoabuscar[8] = "<FechaEnt>";
            textoabuscar[9] = "<Pasaporte>";
            textoabuscar[10] = "<Causa>";
            textoabuscar[11] = "<Norma>";
            /*if (pais.Equals("VENEZUELA"))
                textoabuscar[12] = "<MtoLocal>";
            else
                textoabuscar[12] = "<MtoDolar>";*/
            if ((d.ShowDialog() == DialogResult.OK))
            {
                foreach (DataRow linea in data.Tables[0].Rows)
                {
                    Application.DoEvents();
                    texto[0] = linea["RegSal"].ToString();
                    try
                    {
                        texto[1] = linea["FechaSal"].ToString().Substring(0, linea["FechaSal"].ToString().Length - 8);
                    }
                    catch (Exception ex)
                    {
                        texto[1] = "";
                    }
                    texto[2] = linea["Numexp"].ToString();
                    try
                    {
                        texto[3] = linea["FechaRes"].ToString().Substring(0, linea["FechaRes"].ToString().Length - 8);
                    }
                    catch (Exception ex)
                    {
                        texto[3] = "";
                    }
                    texto[4] = linea["Nombre"].ToString();
                    texto[5] = linea["Dir1"].ToString();
                    texto[6] = linea["Dir2"].ToString();
                    texto[7] = linea["Dir3"].ToString();
                    try
                    {
                        texto[8] = linea["FechaEnt"].ToString().Substring(0, linea["FechaEnt"].ToString().Length - 8);
                    }
                    catch (Exception ex)
                    {
                        texto[8] = "";
                    }
                    texto[9] = linea["Pasaporte"].ToString();
                    texto[10] = linea["Causa"].ToString();
                    texto[11] = linea["Norma"].ToString();
                    /*if (pais.Equals("VENEZUELA"))
                        texto[12] = linea["MtoLocal"].ToString();
                    else
                        texto[12] = linea["MtoDolar"].ToString();*/
                    filedest = d.FileName.Substring(0, d.FileName.Length - 4) + linea["Nombre"] + ".doc";
                    string sourceFile = System.IO.Path.Combine(path, fileName);
                    /*destFile = System.IO.Path.Combine(path, filedest);*/
                    System.IO.File.Copy(sourceFile, filedest, true);

                    wordApp = new Word.Application();

                    if (File.Exists(filedest))
                    {

                        object readOnly = false;
                        object isVisible = true;
                        object destfile = (object)filedest;

                        wordApp.Visible = false;
                        aDoc = wordApp.Documents.Open(ref destfile, ref missing,
                        ref readOnly, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref isVisible, ref missing, ref missing,
                        ref missing, ref missing);
                        aDoc.Activate();

                        for (int i = 0; i < 12; i++)
                        {
                            Application.DoEvents();
                            FindAndReplace(((object)textoabuscar[i]), ((object)texto[i]));
                        }

                        SaveDocument();
                        CloseDocument();
                        //MessageBox.Show("Documento generado satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("El archivo destino no se creó.", "Sin archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void Justificante(string periodo, String pais, SaveFileDialog d)
        {
            string[] texto = new string[13];
            string[] textoabuscar = new string[13];
            string filedest = "";
            string fileName = "";
            DataSet data = Res(periodo, true);
            if (pais.Equals("VENEZUELA"))
            {
                filedest = "Justificante.doc";
                fileName = "Justificante.doc";
            }
            if (pais.Equals("DOMINICANA"))
            {
                filedest = "JustificanteCD.doc";
                fileName = "JustificanteCD.doc";
            }
            if (pais.Equals("COLOMBIA"))
            {
                filedest = "JustificanteCD.doc";
                fileName = "JustificanteCD.doc";
            }
            string path = Directory.GetCurrentDirectory() + "\\Documentos\\";
            d.Title = "Indique donde se van a generar los documentos";
            d.FileName = filedest;
            textoabuscar[0] = "<Numexp>";
            textoabuscar[1] = "<Nombre>";
            textoabuscar[2] = "<Pasaporte>";
            textoabuscar[3] = "<NumInscrip>";
            textoabuscar[4] = "<MontoEsc>";
            textoabuscar[5] = "<MtoDolar>";
            
            if ((d.ShowDialog() == DialogResult.OK))
            {
                foreach (DataRow linea in data.Tables[0].Rows)
                {
                    Application.DoEvents();
                    texto[0] = linea["Numexp"].ToString();
                    texto[1] = linea["Nombre"].ToString();
                    texto[2] = linea["Pasaporte"].ToString();
                    texto[3] = linea["NumInscrip"].ToString();
                    texto[4] = linea["MontoEsc"].ToString();
                    texto[5] = linea["MtoDolar"].ToString();
                    
                    filedest = d.FileName.Substring(0, d.FileName.Length - 4) + linea["Nombre"] + ".doc";
                    string sourceFile = System.IO.Path.Combine(path, fileName);
                    /*destFile = System.IO.Path.Combine(path, filedest);*/
                    System.IO.File.Copy(sourceFile, filedest, true);

                    wordApp = new Word.Application();

                    if (File.Exists(filedest))
                    {
                        object readOnly = false;
                        object isVisible = true;
                        object destfile = (object)filedest;

                        wordApp.Visible = false;
                        aDoc = wordApp.Documents.Open(ref destfile, ref missing,
                        ref readOnly, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref isVisible, ref missing, ref missing,
                        ref missing, ref missing);
                        aDoc.Activate();

                        for (int i = 0; i < 6; i++)
                        {
                            Application.DoEvents();
                            FindAndReplace(((object)textoabuscar[i]), ((object)texto[i]));
                        }

                        SaveDocument();
                        CloseDocument();
                        //MessageBox.Show("Documento generado satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("El archivo destino no se creó.", "Sin archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        public void Justificante_filtro(int desde, int hasta,
                                      DateTime fechaini, DateTime fechafin,
                                      string periodo, String pais, SaveFileDialog d)
        {
            string[] texto = new string[13];
            string[] textoabuscar = new string[13];
            string filedest = "";
            string fileName = "";
            DataSet data = Res_filtro(desde,hasta,fechaini,fechafin,periodo, true);
            if (pais.Equals("VENEZUELA"))
            {
                filedest = "Justificante.doc";
                fileName = "Justificante.doc";
            }
            if (pais.Equals("DOMINICANA"))
            {
                filedest = "JustificanteCD.doc";
                fileName = "JustificanteCD.doc";
            }
            if (pais.Equals("COLOMBIA"))
            {
                filedest = "JustificanteCD.doc";
                fileName = "JustificanteCD.doc";
            }
            string path = Directory.GetCurrentDirectory() + "\\Documentos\\";
            d.Title = "Indique donde se van a generar los documentos";
            d.FileName = filedest;
            textoabuscar[0] = "<Numexp>";
            textoabuscar[1] = "<Nombre>";
            textoabuscar[2] = "<Pasaporte>";
            textoabuscar[3] = "<NumInscrip>";
            textoabuscar[4] = "<MontoEsc>";
            textoabuscar[5] = "<MtoDolar>";

            if ((d.ShowDialog() == DialogResult.OK))
            {
                foreach (DataRow linea in data.Tables[0].Rows)
                {
                    Application.DoEvents();
                    texto[0] = linea["Numexp"].ToString();
                    texto[1] = linea["Nombre"].ToString();
                    texto[2] = linea["Pasaporte"].ToString();
                    texto[3] = linea["NumInscrip"].ToString();
                    texto[4] = linea["MontoEsc"].ToString();
                    texto[5] = linea["MtoDolar"].ToString();

                    filedest = d.FileName.Substring(0, d.FileName.Length - 4) + linea["Nombre"] + ".doc";
                    string sourceFile = System.IO.Path.Combine(path, fileName);
                    /*destFile = System.IO.Path.Combine(path, filedest);*/
                    System.IO.File.Copy(sourceFile, filedest, true);

                    wordApp = new Word.Application();

                    if (File.Exists(filedest))
                    {
                        object readOnly = false;
                        object isVisible = true;
                        object destfile = (object)filedest;

                        wordApp.Visible = false;
                        aDoc = wordApp.Documents.Open(ref destfile, ref missing,
                        ref readOnly, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref isVisible, ref missing, ref missing,
                        ref missing, ref missing);
                        aDoc.Activate();

                        for (int i = 0; i < 6; i++)
                        {
                            Application.DoEvents();
                            FindAndReplace(((object)textoabuscar[i]), ((object)texto[i]));
                        }

                        SaveDocument();
                        CloseDocument();
                        //MessageBox.Show("Documento generado satisfactoriamente");
                    }
                    else
                    {
                        MessageBox.Show("El archivo destino no se creó.", "Sin archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        #endregion

        #region ciudad

        public void grid_ciudad(ref DataTable dt, ref DataGridView grid)
        {
            try
            {
                dt.Clear();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexion);


            string sql = "SELECT ciudad.IDCiudad as Codigo, ciudad.Descripcion as Nombre, Estado.Descripcion as Estado, Pais.Nombre as Pais ";
            sql+="FROM ciudad,estado,pais ";
            sql += "where Pais.Id = Estado.IDPais and ";
            sql+="Ciudad.IDEstado = Estado.IDEstado";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            dt = new DataTable("ciudades");
            da.Fill(dt);
            grid.DataSource = dt;
        }

        public void agregar_ciudad(string pais, string estado, string idciudad, string nombreciudad)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            string idpais = id_de_pais(pais);
            string idestado = id_de_estado(estado, idpais);


            sql = "INSERT INTO Ciudad ";
            sql += " (IDCiudad,IDEstado,Descripcion) VALUES ('";
            sql += idciudad + "','" + idestado + "','" + nombreciudad + "')";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error. Verifique que el codigo no exista");
                }
            }
            conn.Close();
        }

        public void modificar_ciudad(string pais, string estado, string idciudad, string nombreciudad)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            string idpais = id_de_pais(pais);
            string idestado = id_de_estado(estado, idpais);


            sql = "UPDATE Ciudad set Descripcion='" + nombreciudad + "'";
            sql += " WHERE IDEstado='" + idestado + "' AND IDCiudad='" + idciudad + "'";


            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_ciudad(string pais, string estado, string idciudad)
        {
            string idpais = id_de_pais(pais);
            string id_estado = id_de_estado(estado, idpais);

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "delete from Ciudad ";
            sql += " where ";
            sql += "IDCiudad='" + idciudad + "' and IDEstado='" + id_estado + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }

        #endregion ciudad

        #region registro

        public void nuevo_expediente(ref TextBox control, string periodo)
        {
            ajustar_columna_expediente(periodo);
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "select NumExp from solicitudes where periodo='" + periodo + "' order by numexp desc";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            DataTable dt = new DataTable("solicitudes");
            da.Fill(dt);
            string nuevovalor = "";
            try
            {
                nuevovalor = (int.Parse(dt.Rows[0]["NumExp"].ToString()) + 1).ToString("0000");
            }
            catch (Exception ex)
            {
                nuevovalor = "0001";
            }
            control.Text = nuevovalor;
        }

        public void ajustar_columna_expediente(string periodo)
        {
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "select NumExp from solicitudes where periodo='" + periodo + "' ";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            DataTable dt = new DataTable("solicitudes");
            da.Fill(dt);
            string nuevovalor = "";
            int qty = 0;
            con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

            for (qty = 0; qty < dt.Rows.Count; qty++)
            {
                if (dt.Rows[qty]["NumExp"].ToString().Length < 4)
                    try
                    {
                        con1.Open();
                        nuevovalor = (int.Parse(dt.Rows[qty]["NumExp"].ToString())).ToString("0000");
                        sql = "update solicitudes set NumExp='" + nuevovalor + "' where NumExp='" + dt.Rows[qty]["NumExp"].ToString() + "' and periodo='" + periodo + "'";
                        using (OleDbCommand cmd = new OleDbCommand(sql, con1))
                        {
                            cmd.ExecuteNonQuery();

                        }
                        con1.Close();

                    }
                    catch (Exception ex)
                    {

                    }
            }

        }

        public void ajustar_columna_registro(string periodo)
        {
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "select RegEnt from solicitudes where periodo='" + periodo + "' ";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            DataTable dt = new DataTable("solicitudes");
            da.Fill(dt);
            string nuevovalor = "";
            int qty = 0;
            con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

            for (qty = 0; qty < dt.Rows.Count; qty++)
            {
                if (dt.Rows[qty]["RegEnt"].ToString().Length < 5)
                    try
                    {
                        con1.Open();
                        nuevovalor = (int.Parse(dt.Rows[qty]["RegEnt"].ToString())).ToString("00000");
                        sql = "update solicitudes set RegEnt='" + nuevovalor + "' where RegEnt='" + dt.Rows[qty]["RegEnt"].ToString() + "' and periodo='" + periodo + "'";
                        using (OleDbCommand cmd = new OleDbCommand(sql, con1))
                        {
                            cmd.ExecuteNonQuery();

                        }
                        con1.Close();

                    }
                    catch (Exception ex)
                    {

                    }
            }

        }

        public void nuevo_registro(ref TextBox control, string periodo)
        {
            ajustar_columna_registro(periodo);
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "select RegEnt from solicitudes where periodo='" + periodo + "' order by RegEnt desc";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            DataTable dt = new DataTable("solicitudes");
            da.Fill(dt);
            string nuevovalor = "";
            try
            {
                nuevovalor = (int.Parse(dt.Rows[0]["RegEnt"].ToString()) + 1).ToString("00000");
            }
            catch (Exception ex)
            {
                nuevovalor = "00001";
            }
            control.Text = nuevovalor;
        }

        public bool expediente_valido(string expediente)
        {
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "select NumExp from solicitudes where NumExp='" + expediente + "' order by NumExp desc";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            DataTable dt = new DataTable("solicitudes");
            da.Fill(dt);
            return (dt.Rows.Count > 0);
        }

        public void grid_expedientes(ref DataTable dt, ref DataGridView grid, string periodo, string filtro, int salida=3)
        {
            //Se asume que el registro es de salida si tiene resoluciòn
            try
            {
                dt.Clear();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "";
            if (periodo.Equals("Todos"))
            {
                DataTable totalannios = new DataTable();
                annios(ref totalannios);
                if (totalannios.Rows.Count > 0)
                {
                    DataTable dtlocal = new DataTable();
                    
                    foreach (DataRow r in totalannios.Rows)
                    {
                        if (salida==1) //con resolucion
                        {
                            sql = "select solicitudes.* from solicitudes where solicitudes.periodo='" + r["periodo"] + "' ";
                            sql += "AND solicitudes.numexp in (select resolucion.numexp FROM resolucion where resolucion.periodo='" + periodo + "') ";
                            if (filtro.Length > 0)
                            {
                                sql += " AND ";
                                sql += filtro;
                            }
                            sql += " order by solicitudes.numexp,solicitudes.RegEnt asc";
                        }
                        else if (salida == 2) //sin resolucion
                        {
                            sql = "select solicitudes.* from solicitudes where solicitudes.periodo='" + periodo + "' ";
                            sql += "AND solicitudes.numexp not in (select resolucion.numexp FROM resolucion where resolucion.periodo='" + periodo + "') ";
                            if (filtro.Length > 0)
                            {
                                sql += " AND ";
                                sql += filtro;
                            }
                            sql += " order by solicitudes.numexp,solicitudes.RegEnt asc";
                        }
                        else  if (salida == 3)
                        {
                            sql = "select solicitudes.* from solicitudes where solicitudes.periodo='" + r["periodo"] + "'";
                            if (filtro.Length > 0)
                            {
                                sql += " AND ";
                                sql += filtro;
                            }
                            sql += " order by solicitudes.numexp,solicitudes.RegEnt asc";
                        }
                        else if (salida == 4)   //Resoluciones aprobadas
                        {
                            sql = "select solicitudes.* from solicitudes where solicitudes.periodo='" + r["periodo"] + "'";
                            if (filtro.Length > 0)
                            {
                                sql += " AND ";
                                sql += filtro;
                            }
                            sql += " order by solicitudes.numexp,solicitudes.RegEnt asc";
                        }
                        else if (salida == 5)   //Resoluciones denegadas
                        {
                            sql = "select solicitudes.* from solicitudes where solicitudes.periodo='" + r["periodo"] + "'";
                            if (filtro.Length > 0)
                            {
                                sql += " AND ";
                                sql += filtro;
                            }
                            sql += " order by solicitudes.numexp,solicitudes.RegEnt asc";
                        }
                        OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
                        dtlocal = new DataTable("solicitudes");
                        da.Fill(dtlocal);
                        dt.Merge(dtlocal);
                    }
                    grid.DataSource = dt;
                }
            }
            else
            {
                if (salida==1) //con resolucion
                {
                    sql = "select solicitudes.* from solicitudes where solicitudes.periodo='" + periodo + "' ";
                    sql += " AND solicitudes.numexp in (select resolucion.numexp FROM resolucion where resolucion.periodo='" + periodo + "') ";
                    if (filtro.Length > 0)
                    {
                        sql += " AND ";
                        sql += filtro;
                    }
                    sql += " order by solicitudes.numexp,solicitudes.RegEnt asc";
                }
                else if (salida == 2) //sin resolucion
                {
                    sql = "select solicitudes.* from solicitudes  where solicitudes.periodo='" + periodo + "' ";
                    sql += " AND solicitudes.numexp not in (select resolucion.numexp FROM resolucion where resolucion.periodo='" + periodo + "') ";
                    if (filtro.Length > 0)
                    {
                        sql += " AND ";
                        sql += filtro;
                    }
                    sql += " order by solicitudes.numexp,solicitudes.RegEnt asc";
                }
                else  //todos
                {
                    sql = "select solicitudes.* from solicitudes where solicitudes.periodo='" + periodo + "'";
                    if (filtro.Length > 0)
                    {
                        sql += " AND ";
                        sql += filtro;
                    }
                    sql += " order by solicitudes.numexp,solicitudes.RegEnt asc";
                    //sql = "select * from solicitudes,resolucion where solicitudes.periodo='" + periodo + "' order by solicitudes.numexp,solicitudes.RegEnt asc";
                }   
                OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
                dt = new DataTable("solicitudes");
                da.Fill(dt);
                grid.DataSource = dt;
            }
        }

        public void agregarRegistro(string periodo,string numeroExpediente,string FechaSolicitud,
            string apellidos,string nombres,string pasaporte, string NumeroInscripcion, 
            string FechaInscripcionConsulado,string FechaNacimiento, string lugarNacimiento,
            string RegistroEntrada, string FechaEntrada,string Domicilio, string localidad,
            string ciudad, string Estado, string telefonos, string FechaSalidaEspannia,
            string ProvinciaDeSalida,string PaisDeEmigracion, string profesion,bool Cuentapropia,
            bool autonomo, bool noActivo, bool pensionista, string ingresos, string PaisDeRetorno,
            string ProvinciaDeRetorno, string FechaRetorno, string FechaCese, bool perceptor,
            bool precariedad, bool GastosAsistenciaJuridica, bool GastosAsistenciaSanitaria, 
            bool CausasRetorno, bool FamiliaMonoparental, string causas, string cuantia,
            string NumeroDeFamiliares, string FechaInicio, string FechaFin, string comentario,
            string usuario, string FechaActualizacion,string medioSolicitud,string paisnacimiento)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "INSERT INTO Solicitudes ";
            sql += " (Periodo,NumExp,FSolic,Apellidos,Nombres,Pasaporte,";
            sql += "NumInscrip,FInsCon,FNaci,LugarNac,";
            sql += "RegEnt,FecEnt,Domicilio,Localidad,";
            sql += "Ciudad,Estado,Telefonos,FecSalEsp,";
            sql += "ProvSal,PaisEmi,Profesion,";
            sql += "CuentaAjena,Autonomo,NoActivo,Pensionista,";
            sql += "Ingresos,PaisRetorno,ProvRet,FecRet,";
            sql += "FecCese,Perceptor,Precariedad,Juridica,";
            sql += "Sanitaria,CausasRetorno,Monoparental,Causas,";
            sql += "Cuantia,NroFam,FecIni,FecFin,";
            sql += "Comentario,Usuario,FecAct,PaisNacimiento,MedioSolicitud) VALUES ('";
            sql += periodo + "','" + numeroExpediente + "','" + FechaSolicitud + "','" + apellidos + "','";
            sql += nombres + "','" + pasaporte + "','" + NumeroInscripcion + "','" + FechaInscripcionConsulado + "','";
            sql += FechaNacimiento + "','" + lugarNacimiento + "','" + RegistroEntrada + "','" + FechaEntrada + "','";
            sql += Domicilio + "','" + localidad + "','" + ciudad + "','" + Estado + "','";
            sql += telefonos + "','" + FechaSalidaEspannia + "','" + ProvinciaDeSalida + "','";
            sql += PaisDeEmigracion + "','" + profesion + "',";
            if (Cuentapropia) sql += "1,";
            else sql += "0,";
            if (autonomo) sql += "1,";
            else sql += "0,";
            if (noActivo) sql += "1,";
            else sql += "0,";
            if (pensionista) sql += "1,";
            else sql += "0,";
            sql += ingresos.Replace(",",".") + ",'" + PaisDeRetorno + "','" + ProvinciaDeRetorno + "',";
            sql += "'" + FechaRetorno + "','" + FechaCese + "',";
            if (perceptor) sql += "1,";
            else sql += "0,";
            if (precariedad) sql += "1,";
            else sql += "0,";
            if (GastosAsistenciaJuridica) sql += "1,";
            else sql += "0,";
            if (GastosAsistenciaSanitaria) sql += "1,";
            else sql += "0,";
            if (CausasRetorno) sql += "1,";
            else sql += "0,";
            if (FamiliaMonoparental) sql += "1,";
            else sql += "0,";
            sql += "'" + causas + "'," + cuantia.Replace(",",".") + "," + NumeroDeFamiliares + ",";
            sql += "'" + FechaInicio + "','" + FechaFin + "','" + comentario + "','";
            sql += usuario + "','" + FechaActualizacion + "',";
            sql += "'"+paisnacimiento+"','"+medioSolicitud+"')";
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificarRegistro(string periodo, string numeroExpediente, string FechaSolicitud,
            string apellidos, string nombres, string pasaporte, string NumeroInscripcion,
            string FechaInscripcionConsulado, string FechaNacimiento, string lugarNacimiento,
            string RegistroEntrada, string FechaEntrada, string Domicilio, string localidad,
            string ciudad, string Estado, string telefonos, string FechaSalidaEspannia,
            string ProvinciaDeSalida, string PaisDeEmigracion, string profesion, bool Cuentapropia,
            bool autonomo, bool noActivo, bool pensionista, string ingresos, string PaisDeRetorno,
            string ProvinciaDeRetorno, string FechaRetorno, string FechaCese, bool perceptor,
            bool precariedad, bool GastosAsistenciaJuridica, bool GastosAsistenciaSanitaria,
            bool CausasRetorno, bool FamiliaMonoparental, string causas, string cuantia,
            string NumeroDeFamiliares, string FechaInicio, string FechaFin, string comentario,
            string usuario, string FechaActualizacion,string medioSolicitud,string paisnacimiento)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "UPDATE Solicitudes SET ";
            sql += "FSolic='" + FechaSolicitud + "',Apellidos='" + apellidos + "',Nombres='" + nombres + "',Pasaporte='" + pasaporte + "',";
            sql += "NumInscrip='" + NumeroInscripcion + "',FInsCon='" + FechaInscripcionConsulado + "',FNaci='" + FechaNacimiento + "',LugarNac='" + lugarNacimiento + "',";
            sql += "FecEnt='" + FechaEntrada + "',Domicilio='" + Domicilio + "',Localidad='" + localidad + "',";
            sql += "Ciudad='" + ciudad + "',Estado='" + Estado + "',Telefonos='" + telefonos + "',FecSalEsp='" + FechaSalidaEspannia + "',";
            sql += "ProvSal='" + ProvinciaDeSalida + "',PaisEmi='" + PaisDeEmigracion + "',Profesion='" + profesion + "',";
            sql += "CuentaAjena=";
            if (Cuentapropia) sql += "1,";
            else sql += "0,";
            sql +="Autonomo=";
            if (autonomo) sql += "1,";
            else sql += "0,";
            sql +="NoActivo=";
            if (noActivo) sql += "1,";
            else sql += "0,";
            sql+="Pensionista=";
            if (pensionista) sql += "1,";
            else sql += "0,";
            sql += "Ingresos=" + ingresos.Replace(",",".") + ",PaisRetorno='" + PaisDeRetorno + "',ProvRet='" + ProvinciaDeRetorno + "',FecRet='" + FechaRetorno + "',";
            sql += "FecCese='" + FechaCese + "',";
            sql+="Perceptor=";
            if (perceptor) sql += "1,";
            else sql += "0,";
            sql+="Precariedad=";
            if (precariedad) sql += "1,";
            else sql += "0,";
            sql+="Juridica=";
            if (GastosAsistenciaJuridica) sql += "1,";
            else sql += "0,";
            sql += "Sanitaria=";
                if (GastosAsistenciaSanitaria) sql += "1,";
            else sql += "0,";
            sql+="CausasRetorno=";
            if (CausasRetorno) sql += "1,";
            else sql += "0,";
            sql+="Monoparental=";
            if (FamiliaMonoparental) sql += "1,";
            else sql += "0,";

            sql+="Causas='"+causas+"',";
            sql += "Cuantia=" + cuantia.Replace(",", ".") + ",NroFam=" + NumeroDeFamiliares + ",FecIni='" + FechaInicio + "',FecFin='" + FechaFin + "',";
            sql += "Comentario='"+comentario+"',Usuario='"+usuario+"',FecAct='"+FechaActualizacion+"'";
            sql += ",PaisNacimiento='" + paisnacimiento + "',MedioSolicitud='" + medioSolicitud + "'";

            sql+=" where Periodo='" + periodo + "' AND NumExp='" + numeroExpediente + "' AND RegEnt='" + RegistroEntrada + "'";
            
            
            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminarRegistro(string periodo, string expediente, string entrada)
        {
            
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "delete from Solicitudes";
            sql += " where ";
            sql += " Periodo='" + periodo + "' AND NumExp='" + expediente + "' AND RegEnt='" + entrada + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        #endregion registro

        #region pais_en_periodo

        public string crear_bd_pais_periodo(string pais, String periodo,string rutadatos, string rutaapp)
        {
            string res = "Problemas en la creación del archivo del pais.";
            string sourceFile = rutaapp + "\\pais.mdb";
            
            if (!existe_pais_en_periodo(pais, periodo))
            {
                try
                {
                    //si existe la carpeta para el periodo, la usamos, caso contrario, la creamos
                    rutadatos += "\\" + periodo;
                    if (!System.IO.Directory.Exists(rutadatos))
                    {
                        System.IO.Directory.CreateDirectory(rutadatos);
                    }
                    string destFile = rutadatos + "\\" + pais + ".mdb";
                    System.IO.File.Copy(sourceFile, destFile, true);
                    res = "";
                }
                catch (Exception ex)
                {
                    res += ex.Message;
                }
                //copiamos el archivo access en la carpeta del periodo
                

            }
            return res;
        }

        public string eliminar_bd_pais_periodo(string pais, String periodo, string rutadatos)
        {
            string res = "Problemas en la eliminación del archivo del pais.";

            try
            {
                string destFile = rutadatos + "\\" + periodo + "\\" + pais + ".mdb";
                System.IO.File.Delete(destFile);
                res = "";
            }
            catch (Exception ex)
            {
                res += ex.Message;
            }
            return res;
        }

        public bool existe_pais_en_periodo(string pais, String periodo)
        {
            bool res = false;
            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT Pais ";
            sql += "FROM Paises ";
            sql += "WHERE Pais='" + pais + "' and periodo='"+periodo+"'";


            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();
            Application.DoEvents();
            res = (oDs.Tables[0].Rows.Count > 0);

            return res;

        }

        public void agregar_pais_en_periodo(string pais, String periodo,
            string demarcacion,string consejeria,string numexp,
            double maximo, double cambioLAR, double cambio,
            double Ejecutado1, double DolarLocal1, double Ejecutado2, double DolarLocal2,
            double Ejecutado3, double DolarLocal3, double Ejecutado4, double DolarLocal4,
            string usuario, string fecha)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "INSERT INTO paises ";
            sql += " (Pais,Periodo,Demarcacion,Consejeria,";
            sql += "Numexp,maximo,CambioLAR,cambio,";
            sql += "Ejecutado1,DolarLocal1,Ejecutado2,DolarLocal2,";
            sql += "Ejecutado3,DolarLocal3,Ejecutado4,DolarLocal4,";
            sql += "Usuario,FecAct) VALUES ('";
            sql += pais + "','" + periodo + "','" + demarcacion + "','" + consejeria + "','";
            sql += numexp + "','" + maximo.ToString() + "','" + cambioLAR.ToString() + "','" + cambio.ToString() + "','";
            sql += Ejecutado1.ToString() + "','" + DolarLocal1.ToString() + "','" + Ejecutado2.ToString() + "','" + DolarLocal2.ToString() + "','";
            sql += Ejecutado3.ToString() + "','" + DolarLocal3.ToString() + "','" + Ejecutado4.ToString() + "','" + DolarLocal4.ToString() + "','";
            sql += usuario.ToString() + "','" + fecha.ToString() + "'";
            sql += ")";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificar_pais_en_periodo(string pais, String periodo,
            string demarcacion,string consejeria,string numexp,
            double maximo, double cambioLAR, double cambio,
            double Ejecutado1, double DolarLocal1, double Ejecutado2, double DolarLocal2,
            double Ejecutado3, double DolarLocal3, double Ejecutado4, double DolarLocal4,
            string usuario, string fecha)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "UPDATE paises ";
            sql += " SET demarcacion='" + demarcacion + "',consejeria='" + consejeria + "',Numexp='" + numexp + "',maximo='" + maximo.ToString() + "',cambioLAR='" + cambioLAR.ToString() + "',";
            sql += "cambio='" + cambio.ToString() + "',Ejecutado1='" + Ejecutado1.ToString() + "',DolarLocal1='" + DolarLocal1.ToString() + "',";
            sql += "Ejecutado2='" + Ejecutado2.ToString() + "',DolarLocal2='" + DolarLocal2.ToString() + "',";
            sql += "Ejecutado3='" + Ejecutado3.ToString() + "',DolarLocal3='" + DolarLocal3.ToString() + "',";
            sql += "Ejecutado4='" + Ejecutado4.ToString() + "',DolarLocal4='" + DolarLocal4.ToString() + "',";
            sql += "Usuario='" + usuario + "',FecAct='" + fecha.ToString() + "'";
            sql += " WHERE Periodo='" + periodo + "' AND Pais='"+pais+"'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_pais_de_periodo(string periodo,string pais)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "delete from paises ";
            sql += " where ";
            sql += "Periodo='" + periodo.ToString() + "' AND Pais='"+pais+"'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        #endregion pais_en_periodo

        #region periodo

        

        public void agregar_periodo(string periodo,String orden,
            double Libramiento1,double Ejecutado1,double EuroDolar1,
            double DolarLocal1,double Libramiento2,double Ejecutado2,double EuroDolar2,
            double DolarLocal2,double Libramiento3,double Ejecutado3,double EuroDolar3,
            double DolarLocal3,double Libramiento4,double Ejecutado4,double EuroDolar4,
            double DolarLocal4,string activo,Boolean cerrado,string usuario,string fecha)
        {
            
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "INSERT INTO periodo ";
            sql += " (Periodo,Orden,Libramiento1,Ejecutado1,EuroDolar1,DolarLocal1,";
            sql += "Libramiento2,Ejecutado2,EuroDolar2,DolarLocal2,";
            sql += "Libramiento3,Ejecutado3,EuroDolar3,DolarLocal3,";
            sql += "Libramiento4,Ejecutado4,EuroDolar4,DolarLocal4,";
            sql += "Activo,Cerrado,Usuario,FecAct) VALUES ('";
            sql += periodo + "','" + orden + "','" + Libramiento1.ToString() + "','" + Ejecutado1.ToString() + "','";
            sql += EuroDolar1.ToString() + "','" + DolarLocal1.ToString() +"','"+ Libramiento2.ToString() + "','" + Ejecutado2.ToString() + "','";
            sql += EuroDolar2.ToString() + "','" + DolarLocal2.ToString() +"','"+ Libramiento3.ToString() + "','" + Ejecutado3.ToString() + "','";
            sql += EuroDolar3.ToString() + "','" + DolarLocal3.ToString() +"','"+ Libramiento4.ToString() + "','" + Ejecutado4.ToString() + "','";
            sql += EuroDolar4.ToString() + "','" + DolarLocal4.ToString() + "','" + activo + "',";
            if (cerrado) sql += "1,";
            else sql += "0,";
            sql += usuario.ToString() + ",'" + fecha.ToString() + "'";
            sql += ")";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificar_periodo(string periodo, String orden,
            double Libramiento1, double Ejecutado1, double EuroDolar1,
            double DolarLocal1, double Libramiento2, double Ejecutado2, double EuroDolar2,
            double DolarLocal2, double Libramiento3, double Ejecutado3, double EuroDolar3,
            double DolarLocal3, double Libramiento4, double Ejecutado4, double EuroDolar4,
            double DolarLocal4, string activo, Boolean cerrado, string usuario, string fecha)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "UPDATE periodo ";
            sql += " SET Orden='" + orden + "',Libramiento1='" + Libramiento1.ToString() + "',Ejecutado1='" + Ejecutado1.ToString() + "',EuroDolar1='" + EuroDolar1.ToString() + "',DolarLocal1='" + DolarLocal1.ToString() + "',";
            sql += "Libramiento2='" + Libramiento2.ToString() + "',Ejecutado2='" + Ejecutado2.ToString() + "',EuroDolar2='" + EuroDolar2.ToString() + "',DolarLocal2='" + DolarLocal2.ToString() + "',";
            sql += "Libramiento3='" + Libramiento3.ToString() + "',Ejecutado3='" + Ejecutado3.ToString() + "',EuroDolar3='" + EuroDolar3.ToString() + "',DolarLocal3='" + DolarLocal3.ToString() + "',";
            sql += "Libramiento4='" + Libramiento4.ToString() + "',Ejecutado4='" + Ejecutado4.ToString() + "',EuroDolar4='" + EuroDolar4.ToString() + "',DolarLocal4='" + DolarLocal4.ToString() + "',";
            sql += "Activo='" + activo + "',Cerrado=";
            if (cerrado) sql += "1,";
            else sql += "0,";
            sql += "Usuario=" + usuario.ToString() + ",FecAct='" + fecha.ToString() + "'";
            sql += " WHERE Periodo='"+periodo+"'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_periodo(string periodo)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "delete from periodo ";
            sql += " where ";
            sql += "Periodo='" + periodo.ToString()+"'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

#endregion periodo
        
        #region familiares

        public void guardar_familiar(string periodo, string expediente,
            string apellidos, string nombres, string edad, string ingresos,
            string parentesco, string IncripConsular,
            string usuario, string fechaact)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "insert into Familiares ";
            sql += " (Periodo,Numexp,Apellidos,Nombres,Edad,Ingresos,Parentesco,IncripConsular,Usuario,FecAct) values ('";
            sql += periodo + "','" + expediente + "','" + apellidos + "','" + nombres + "','" + edad + "',";
            sql += ingresos + ",'" + parentesco + "','" + IncripConsular + "','" + usuario + "','" + fechaact + "')";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificar_familiar(string periodo, string expediente,
            string apellidos, string nombres, string edad, string ingresos,
            string parentesco, string IncripConsular,
            string usuario, string fechaact)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "update Familiares set ";
            sql += " Apellidos='" + apellidos + "',Nombres='" + nombres + "',Edad='"+edad+"',";
            sql +=  "Ingresos="+ingresos+",Parentesco='"+parentesco+"',IncripConsular='"+IncripConsular+"',";
            sql += "Usuario='" + usuario + "',FecAct='" + fechaact + "'";
            sql += "where Periodo='"+periodo+"' and Numexp='"+expediente+"'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_familiar(string periodo, string expediente, string apellidos, string nombres)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "delete from Familiares";
            sql += " where ";
            sql += " Periodo='" + periodo + "' AND NumExp='" + expediente + "' AND Apellidos='" + apellidos + "' AND Nombres='"+nombres+"'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        #endregion 

        #region ingresos

        public void guardar_ingreso(string periodo, string expediente,
            string entidad, string causa, string fecha, string ingresos,
            string usuario, string fechaact)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "insert into Ingresos ";
            sql += " (Periodo,Numexp,Entidad,Causa,Fecha,Ingresos,Usuario,FecAct) values ('";
            sql += periodo + "','" + expediente + "','" + entidad + "','" + causa + "','" + fecha + "',";
            sql += ingresos + ",'" + usuario + "','" + fechaact + "')";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificar_ingreso(string periodo, string expediente,
            string entidad, string causa, string fecha, string ingresos,
            string usuario, string fechaact)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "update Ingresos set ";
            sql += " Entidad='" + entidad + "',Causa='" + causa + "',Fecha='" + fecha + "',";
            sql += "Ingresos=" + ingresos + ",";
            sql += "Usuario='" + usuario + "',FecAct='" + fechaact + "'";
            sql += "where Periodo='" + periodo + "' and Numexp='" + expediente + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_ingreso(string periodo, string expediente, string entidad, string causa)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "delete from Ingresos";
            sql += " where ";
            sql += " Periodo='" + periodo + "' AND NumExp='" + expediente + "' AND Entidad='" + entidad + "' AND Causa='" + causa + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        #endregion 

        #region nuevo_periodo

        public void duplicar_entrada(string periodo)
        {
            int res = 0;

            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT RegEnt10.* into RegEnt" + periodo.Substring(2);
            sql += " FROM RegEnt10 ";

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();


            sql = "delete from RegEnt" + periodo.Substring(2);

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void duplicar_salida(string periodo)
        {
            int res = 0;

            DataSet oDs;
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            int i = 0;

            string sql = "SELECT RegSal10.* into RegSal" + periodo.Substring(2);
            sql += " FROM RegSal10 ";

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();


            sql = "delete from RegSal" + periodo.Substring(2);

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }       

        #endregion nuevo_periodo   

        #region ficha

        public void grid_fichas(ref DataTable dt, ref DataGridView grid, string periodo)
        {
            try
            {
                dt.Clear();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            
            
                string sql = "select * from InfSocial ";
                OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
                dt = new DataTable("InfSocial");
                da.Fill(dt);
                grid.DataSource = dt;
            
        }

        #endregion

        #region status

        public void grid_status(ref DataTable dt, ref DataGridView grid, string periodo)
        {
            try
            {
                dt.Clear();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);


            string sql = "select * from solicitudes where periodo='" + periodo + "' order by numexp,RegEnt  asc";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            dt = new DataTable("solicitudes");
            da.Fill(dt);
            grid.DataSource = dt;

        }

        public void grid_manstatus(ref DataTable dt, ref DataGridView grid)
        {
            try
            {
                dt.Clear();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexion);
            string sql = "select Codigo,Concepto from Status ";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            dt = new DataTable("status");
            da.Fill(dt);
            grid.DataSource = dt;
        }

        public void agregar_status(string codigo, string concepto)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;
            
            sql = "INSERT INTO Status ";
            sql += " (Codigo,Concepto) VALUES ('";
            sql += codigo + "','" + concepto + "')";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificar_status(string codigo, string concepto)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;
            
            sql = "UPDATE Status set Concepto='" + concepto + "'";
            sql += " WHERE Codigo='" + codigo + "'";


            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_status(string codigo)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "delete from Status ";
            sql += " where ";
            sql += "codigo='" + codigo + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }

        #endregion

        #region reporte_Hoja_de_instruccion

        public void datos_personales_por_numexp(string periodo, string numexp1,string numexp2 ,
                                     ref DataTable resultado)
        //ref string nombre, ref string pasaporte,ref string ingreso, ref string edad, ref string inscripcion_consular)
        {
            numexp1=numexp1.PadLeft(4,'0');
            numexp2=numexp2.PadLeft(4,'0');
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "SELECT Nombres+' '+Apellidos as nombre,Pasaporte,Ingresos,FNaci,FInsCon,NumExp FROM Solicitudes ";
            sql += "WHERE periodo='" + periodo + "' and NumExp between '" + numexp1 + "' and '"+numexp2+"'";
            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            //control = new DataTable();
            oCmd.Fill(resultado);
            oConn.Close();

        }

        public void datos_personales_por_fecha(string periodo, string fecha1, string fecha2,
                                                 ref DataTable resultado)
        {
            fecha1 = fecha1.Substring(3, 2) + "/" + fecha1.Substring(0, 2) + "/" + fecha1.Substring(6, 4);
            fecha2 = fecha2.Substring(3, 2) + "/" + fecha2.Substring(0, 2) + "/" + fecha2.Substring(6, 4);
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "SELECT Nombres+' '+Apellidos as nombre,Pasaporte,Ingresos,FNaci,FInsCon,NumExp FROM Solicitudes ";
            sql += "WHERE periodo='" + periodo + "' and FecEnt between #" + fecha1 + "# and #" + fecha2 + "#";
            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            //control = new DataTable();
            oCmd.Fill(resultado);
            oConn.Close();

        }

        public void datos_familiares_por_registro(string periodo, string numexp, ref DataTable resultado)
        {
            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            string sql = "SELECT Apellidos+' '+Nombres as nombre,Parentesco,Ingresos FROM Familiares ";
            sql += "WHERE periodo='" + periodo + "' and NumExp='" + numexp + "'";
            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            //control = new DataTable();
            oCmd.Fill(resultado);
            oConn.Close();
        }

        #endregion reporte_Hoja_de_instruccion

        #region pais

        public void agregar_pais(string descripcion)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "INSERT INTO Pais ";
            sql += " (Nombre) VALUES ('" + descripcion + "')";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificar_pais(int id, string descripcion)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "UPDATE Pais set Nombre='" + descripcion + "' ";
            sql += " WHERE Id=" + id.ToString();


            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_pais(int id)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "delete from Pais ";
            sql += " where ";
            sql += "id=" + id.ToString();

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        #endregion

        #region estado

        public void grid_estado(ref DataTable dt, ref DataGridView grid)
        {
            try
            {
                dt.Clear();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexion);
            string sql = "select pais.nombre,estado.idestado,estado.descripcion from pais,estado where estado.idpais=pais.id";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            dt = new DataTable("estado");
            da.Fill(dt);
            grid.DataSource = dt;
        }

        public void agregar_estado(string pais, string idestado, string estado)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            string idpais = id_de_pais(pais);


            sql = "INSERT INTO Estado ";
            sql += " (IDPais,IDEstado,Descripcion) VALUES ('";
            sql += idpais + "','" + idestado + "','" + estado + "')";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificar_estado(string pais, string idestado, string estado)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            string idpais = id_de_pais(pais);

            sql = "UPDATE Estado set Descripcion='" + estado + "'";
            sql += " WHERE IDPais=" + idpais + " AND IDEstado='" + idestado + "'";


            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_estado(string pais, string idestado)
        {
            string idpais = id_de_pais(pais);

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "delete from Estado ";
            sql += " where ";
            sql += "IDPais=" + idpais + " and IDEstado='" + idestado + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();

        }

        #endregion estado

        #region pago

        public void cuentas(ref ComboBox control)
        {
            DataSet oDs;

            control.Items.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT distinct Cuenta FROM Resolucion ", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            foreach (DataRow linea in oDs.Tables[0].Rows)
            {

                control.Items.Add(linea["Cuenta"]);
                Application.DoEvents();
            }

        }

        public void titular(ref TextBox control, string exp, string periodo)
        {
            DataSet oDs;

            control.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT titular FROM Resolucion where numexp='"+exp+"' and periodo='"+periodo+"'", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            /*foreach (DataRow linea in oDs.Tables[0].Rows[0])
            {*/
            try
            {
                control.Text = oDs.Tables[0].Rows[0]["titular"].ToString();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
            }
            //}

        }

        public void titular_en_expediente(ref TextBox control, string exp, string periodo)
        {
            DataSet oDs;

            control.Clear();

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Nombres,Apellidos FROM Solicitudes where numexp='" + exp + "' and periodo='" + periodo + "'", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();

            /*foreach (DataRow linea in oDs.Tables[0].Rows[0])
            {*/
            try
            {
                control.Text = oDs.Tables[0].Rows[0]["Nombres"].ToString() + " " + oDs.Tables[0].Rows[0]["Apellidos"].ToString();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
            }
            //}

        }


        public void grid_pagos(ref DataTable dt, ref DataGridView grid, string periodo)
        {
            try
            {
                dt.Clear();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
            {
                string sql = "select Numexp,Titular as Beneficiario, Cheque as Nª_Cheque,Fpago as Fecha_de_pago,CuentaDp as Cuenta_a_depositar,Deposito as Nº_Deposito,BancoDp,Format(MontoD,\"Standard\") as Monto_Local,Fdepo as Fecha_deposito,Pagado from resolucion where periodo='" + periodo + "' order by numexp asc";
                OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
                dt = new DataTable("resolucion");
                da.Fill(dt);
                grid.DataSource = dt;
            }
        }

        public void agregar_pago(string expediente, string periodo, string titular,
                                 string cheque, string fecha_pago, string numero_cuenta,
                                 string deposito, string banco, string monto, string pagado)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            sql = "INSERT INTO Resolucion ";
            sql += " (Periodo,Numexp,Titular,Cheque,FPago,CuentaDp,Deposito,BancoDp,MontoD,Pagado) ";
            sql+=" VALUES ('" + periodo + "','"+expediente+"','"+titular+"','"+cheque+"',";
            sql+="'"+fecha_pago+"','"+numero_cuenta+"','"+deposito+"','"+banco+"',"+monto+","+pagado+")";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void modificar_pago(string expediente, string periodo, string titular,
                                 string cheque, string fecha_pago, string numero_cuenta,
                                 string deposito, string banco, string monto, string pagado)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            String sql;

            monto = monto.Replace(',', '.');

            sql = "UPDATE Resolucion SET ";
            sql += " Titular='" + titular + "',Cheque='" + cheque + "',";
            sql += "FPago='" + fecha_pago + "',CuentaDp='" + numero_cuenta + "',Deposito='" + deposito + "',";
            sql += "BancoDp='" + banco + "',MontoD=" + monto + ",Pagado="+pagado;
            sql += " WHERE Periodo='" + periodo + "' AND Numexp='" + expediente + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void eliminar_pago(string expediente, string periodo)
        {

            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "delete from Resolucion ";
            sql += " where ";
            sql += "periodo='" + periodo+"' and numexp='"+expediente+"'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        #endregion pago

        #region marca

        public void id_marca_en_solicitud( string periodo,string expediente,ref string codigo_marca, ref string marca)
        {
            /*
             * Este metodo Busca en la tabla solicitud el codigo de la marca y busca el concepto 
             * en la tabla marcas de la base de datos programa2
             * */
            DataSet oDs,oDs1;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            OleDbConnection oConn1 =
                    new System.Data.OleDb.OleDbConnection(stringdeconexionpais);

            string sql = "SELECT Marca FROM Solicitudes ";
            sql += "where Periodo='" + periodo + "'";
            sql += " and Numexp='" + expediente + "'";

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn1);
            oConn1.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn1.Close();

            try
            {
                if (oDs.Tables[0].Rows[0]["Marca"] != null)
                {
                    codigo_marca = oDs.Tables[0].Rows[0]["Marca"].ToString();
                    string sql1 = "SELECT Concepto from Marcas where Codigo='" + oDs.Tables[0].Rows[0]["Marca"].ToString()+ "'";

                    OleDbDataAdapter oCmd1 = new OleDbDataAdapter(sql1, oConn);
                    oConn.Open();
                    oDs1 = new DataSet();
                    oCmd1.Fill(oDs1);
                    oConn.Close();
                    marca = oDs1.Tables[0].Rows[0]["Concepto"].ToString();
                }

            }
            catch (Exception ex) { }
        }

        public void requisitorias_en_solicitud(string id_requisitoria, ref ComboBox combo)
        {
            DataSet oDs;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);
            string sql = "SELECT Concepto FROM Requisitorias ";
            sql += "where Codigo='" + id_requisitoria + "'";

            OleDbDataAdapter oCmd = new OleDbDataAdapter(sql, oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();
            try
            {
                combo.Text = oDs.Tables[0].Rows[0]["Concepto"].ToString();
            }
            catch (Exception ex) 
            {
                combo.Text = "";
            }
            Application.DoEvents();
        }

        public void modificar_requisitoria(string periodo, string expediente, string req1, string req2, string req3, string req4, string fecha_notificacion)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexionpais);
            conn.Open();
            
            String sql;

            sql = "UPDATE Solicitudes set Deses1='" + id_de_requisitoria(req1) + "',Deses2='" + id_de_requisitoria(req2) + "',Deses3='" + id_de_requisitoria(req3) + "',Deses4='" + id_de_requisitoria(req4) + "'";
            sql += " , FNoti='" + fecha_notificacion + "' ";
            sql += " WHERE periodo='" + periodo + "' AND Numexp='" + expediente + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void grid_marca(ref DataTable dt, ref DataGridView grid, string periodo)
        {
            try
            {
                dt.Clear();
            }
            catch (Exception ex)
            {
                dt = new DataTable();
            }
            System.Data.OleDb.OleDbConnection con1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);


            string sql = "select Numexp as Num_Expediente, Fsolic as Solicitud, Apellidos, Nombres, ";
            sql +="Pasaporte, Numinscrip as Num_insc_Consular,Marca,";
            sql += "FecMarca as Fecha_Marca,Deses1,Deses2,Deses3,Deses4,FNoti from solicitudes where periodo='" + periodo + "' order by numexp,RegEnt  asc";
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con1);
            dt = new DataTable("solicitudes");
            da.Fill(dt);
            grid.DataSource = dt;

        }

        public void modificar_marca_en_expediente(string periodo, string expediente, string codigo_marca)
        {
            OleDbConnection conn;
            conn = new OleDbConnection(stringdeconexion);
            conn.Open();
            String sql;

            sql = "UPDATE Solicitudes set Marca='" + codigo_marca + "'";
            sql += " WHERE periodo=" + periodo + " AND Numexp='" + expediente + "'";

            using (OleDbCommand cmd = new OleDbCommand(sql, conn))
            {
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        #endregion marca

        #region estadisticas

        public void cuantitativo(ref DataGridView d, string periodo)
        {
            //vamos a obtener los paises para ese período
            
            DataSet oDs;
            DataSet oDs1;
            OleDbConnection oConn1;
            OleDbDataAdapter oCmd1;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Pais FROM Paises where periodo='" + periodo + "'", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();
            string[] pais = new string[200];
            string[] cantidad_solicitudes = new string[200];
            string[] solicitudes_favorables = new string[200];
            string[] solicitudes_desfavorables = new string[200];
            string[] porcentaje_solicitudes_favorables = new string[200];
            string[] porcentaje_solicitudes_desfavorables = new string[200];
            string[][] filas = new string[200][];
            string total_solicitudes = "0";
            string total_favorables = "0";
            string total_desfavorables = "0";
            string tot_desfavorable = "0";
            int indice = 0;
            foreach (DataRow linea in oDs.Tables[0].Rows)
            {
                try
                {
                    Application.DoEvents();
                    //para cada pais, obtenemos los datos a agregar al datagridview
                    pais[indice] = linea["Pais"].ToString();
                    accesapais(Application.StartupPath, periodo, pais[indice]);

                    oConn1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                    oCmd1 = new OleDbDataAdapter("SELECT count(*) FROM Solicitudes where periodo='" + periodo + "'", oConn1);
                    oConn1.Open();
                    oDs1 = new DataSet();
                    oCmd1.Fill(oDs1);
                    oConn1.Close();
                    cantidad_solicitudes[indice] = oDs1.Tables[0].Rows[0][0].ToString();
                    total_solicitudes = (int.Parse(total_solicitudes) + int.Parse(cantidad_solicitudes[indice])).ToString();
                    oConn1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                    oCmd1 = new OleDbDataAdapter("SELECT count(*) FROM Solicitudes where periodo='" + periodo + "' and marca='01'", oConn1);
                    oConn1.Open();
                    oDs1 = new DataSet();
                    oCmd1.Fill(oDs1);
                    Application.DoEvents();
                    oConn1.Close();
                    solicitudes_favorables[indice] = oDs1.Tables[0].Rows[0][0].ToString();
                    total_favorables = (int.Parse(total_favorables) + int.Parse(solicitudes_favorables[indice])).ToString();
                    tot_desfavorable = "0";
                    oConn1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                    oCmd1 = new OleDbDataAdapter("SELECT count(*) FROM Solicitudes where periodo='" + periodo + "' and (marca<>'01' or marca = null)", oConn1);
                    oConn1.Open();
                    oDs1 = new DataSet();
                    oCmd1.Fill(oDs1);
                    Application.DoEvents();
                    oConn1.Close();
                    solicitudes_desfavorables[indice] = oDs1.Tables[0].Rows[0][0].ToString();
                    total_desfavorables = (int.Parse(total_desfavorables) + int.Parse(solicitudes_desfavorables[indice])).ToString();
                    Application.DoEvents();
                    oConn1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                    oCmd1 = new OleDbDataAdapter("SELECT count(*) FROM Solicitudes WHERE periodo='" + periodo + "' AND numexp not in(SELECT numexp FROM Solicitudes WHERE periodo='" + periodo + "' AND (marca<>'01' or marca='01'))", oConn1);
                    oConn1.Open();
                    oDs1 = new DataSet();
                    oCmd1.Fill(oDs1);
                    oConn1.Close();
                    tot_desfavorable = oDs1.Tables[0].Rows[0][0].ToString();
                    if (!(tot_desfavorable.Equals(solicitudes_desfavorables[indice])))
                    {
                        solicitudes_desfavorables[indice] = (int.Parse(solicitudes_desfavorables[indice]) + int.Parse(tot_desfavorable)).ToString();
                        total_desfavorables = (int.Parse(total_desfavorables) + int.Parse(tot_desfavorable)).ToString();
                    }
                    indice++;
                }
                catch (Exception ex) { }
            }
            indice=0;
            foreach (DataRow linea in oDs.Tables[0].Rows)
            {
                Application.DoEvents();
                if (int.Parse(total_favorables) > 0)
                    porcentaje_solicitudes_favorables[indice] = Math.Round((decimal)(double.Parse(solicitudes_favorables[indice]) * 100 / double.Parse(total_favorables)), 2).ToString();
                else
                    porcentaje_solicitudes_favorables[indice] = "0";
                if (int.Parse(total_desfavorables) > 0)
                    porcentaje_solicitudes_desfavorables[indice] = Math.Round((decimal)(double.Parse(solicitudes_desfavorables[indice]) * 100 / double.Parse(total_desfavorables)), 2).ToString();
                else
                    porcentaje_solicitudes_desfavorables[indice] = "0";
                indice++;
            }
            indice=0;
            foreach (DataRow linea in oDs.Tables[0].Rows)
            {
                Application.DoEvents();
                filas[indice] = new string[6];
                filas[indice][0]=pais[indice];
                filas[indice][1]=cantidad_solicitudes[indice];
                filas[indice][2]=solicitudes_favorables[indice];
                filas[indice][3]=porcentaje_solicitudes_favorables[indice];
                filas[indice][4]=solicitudes_desfavorables[indice];
                filas[indice][5]=porcentaje_solicitudes_desfavorables[indice];
                indice++;
            }
            filas[indice] = new string[6];
            filas[indice][0]="TOTALES";
            filas[indice][1]=total_solicitudes;
            filas[indice][2]=total_favorables;
            //filas[indice][3]=porcentaje_solicitudes_favorables[indice];
            filas[indice][4]=total_desfavorables;
            //filas[indice][5]=porcentaje_solicitudes_desfavorables[indice];
            foreach (string[] row in filas)
            {
                Application.DoEvents();
                if (row != null)
                    d.Rows.Add(row);
                else
                    break;
            }
        }

        public void administrativo(ref DataGridView d, string periodo)
        {
            //vamos a obtener los paises para ese período

            DataSet oDs;
            DataSet oDs1;
            OleDbConnection oConn1;
            OleDbDataAdapter oCmd1;

            OleDbConnection oConn =
                    new System.Data.OleDb.OleDbConnection(stringdeconexion);

            OleDbDataAdapter oCmd = new OleDbDataAdapter("SELECT Pais FROM Paises where periodo='" + periodo + "'", oConn);
            oConn.Open();
            oDs = new DataSet();
            oCmd.Fill(oDs);
            oConn.Close();
            string[] pais = new string[200];
            /*string[] cantidad_solicitudes = new string[200];*/
            string[] solicitudes_favorables = new string[200];
            string[] euros = new string[200];
            string[] dolares = new string[200];
            string[] moneda_local = new string[200];
            string[][] filas = new string[200][];
            string total_moneda_local = "0";
            string total_favorables = "0";
            string total_euros = "0";
            string total_dolares = "0";
            int indice = 0;
            foreach (DataRow linea in oDs.Tables[0].Rows)
            {
                try
                {
                    Application.DoEvents();
                    //para cada pais, obtenemos los datos a agregar al datagridview
                    pais[indice] = linea["Pais"].ToString();
                    accesapais(Application.StartupPath, periodo, pais[indice]);

                    /*oConn1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                    oCmd1 = new OleDbDataAdapter("SELECT count(*) FROM Solicitudes where periodo='" + periodo + "'", oConn1);
                    oConn1.Open();
                    oDs1 = new DataSet();
                    oCmd1.Fill(oDs1);
                    oConn1.Close();
                    cantidad_solicitudes[indice] = oDs1.Tables[0].Rows[0][0].ToString();
                    total_solicitudes = (int.Parse(total_solicitudes) + int.Parse(cantidad_solicitudes[indice])).ToString();*/
                    oConn1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                    oCmd1 = new OleDbDataAdapter("SELECT count(*) FROM Solicitudes where periodo='" + periodo + "' and marca='01'", oConn1);
                    oConn1.Open();
                    oDs1 = new DataSet();
                    oCmd1.Fill(oDs1);
                    Application.DoEvents();
                    oConn1.Close();
                    solicitudes_favorables[indice] = oDs1.Tables[0].Rows[0][0].ToString();
                    total_favorables = (int.Parse(total_favorables) + int.Parse(solicitudes_favorables[indice])).ToString();
                    oConn1 = new System.Data.OleDb.OleDbConnection(stringdeconexionpais);
                    oCmd1 = new OleDbDataAdapter("SELECT Monto,CambioED,CambioDB FROM Solicitudes where periodo='" + periodo +"'", oConn1);
                    oConn1.Open();
                    oDs1 = new DataSet();
                    oCmd1.Fill(oDs1);
                    Application.DoEvents();
                    oConn1.Close();
                    euros[indice] = "0";
                    dolares[indice] = "0";
                    moneda_local[indice] = "0";
                    foreach (DataRow linea_interna in oDs1.Tables[0].Rows)
                    {
                        euros[indice] = (double.Parse(euros[indice])+double.Parse(linea_interna[0].ToString())).ToString();
                        if (double.Parse(linea_interna[1].ToString())>0)
                            dolares[indice] = (double.Parse(dolares[indice])+(double.Parse(euros[indice]) / double.Parse(linea_interna[1].ToString()))).ToString();
                        moneda_local[indice] = (double.Parse(moneda_local[indice])+(double.Parse(euros[indice]) * double.Parse(linea_interna[2].ToString()))).ToString();
                        Application.DoEvents();
                    }
                    total_euros = (double.Parse(total_euros) + double.Parse(euros[indice])).ToString();
                    total_dolares = (double.Parse(total_dolares) + double.Parse(dolares[indice])).ToString();
                    total_moneda_local = (double.Parse(total_moneda_local) + double.Parse(moneda_local[indice])).ToString();

                    indice++;
                }
                catch (Exception ex) { }
            }
            indice = 0;
            
            foreach (DataRow linea in oDs.Tables[0].Rows)
            {
                Application.DoEvents();
                filas[indice] = new string[6];
                filas[indice][0] = pais[indice];
                filas[indice][1] = solicitudes_favorables[indice];
                filas[indice][2] = euros[indice];
                filas[indice][3] = dolares[indice];
                filas[indice][4] = moneda_local[indice];
                indice++;
            }
            filas[indice] = new string[6];
            filas[indice][0] = "TOTALES";
            filas[indice][1] = total_favorables;
            filas[indice][2] = total_euros;
            filas[indice][3] = total_dolares;
            filas[indice][3] = total_moneda_local;
            
            foreach (string[] row in filas)
            {
                Application.DoEvents();
                if (row != null)
                    d.Rows.Add(row);
                else
                    break;
            }
        }

        #endregion estadisticas


    }
}
