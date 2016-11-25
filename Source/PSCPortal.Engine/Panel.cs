using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.Engine
{
    [Serializable]
    public class Panel
    {
        public enum PANEL
        {
            Top = 0,
            Left,
            Center,
            Right,
            Bottom
        }
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
        }

        private Panel()
        {
        }
        internal Panel(DbDataReader reader)
        {
            _id = (int)reader["PanelId"];
            _name = (string)reader["PanelName"];
        }
        public override string ToString()
        {
            return Parse(_id).ToString();
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Panel) && ((Panel)obj)._id == _id)
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        public static Panel GetPanel(Panel.PANEL pn)
        {
            Panel result = null;
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                #region PanelId
                DbParameter prPanelId = database.GetParameter(System.Data.DbType.Int32, "@PanelId", (int)pn);
                command.Parameters.Add(prPanelId);
                #endregion

                command.CommandText = "Panel_GetById";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        result = new Panel(reader);
                }
            }
            return result;
        }
        public static Panel.PANEL Parse(int id)
        {
            Panel.PANEL result = Panel.PANEL.Top;
            switch (id)
            {
                case (int)Panel.PANEL.Top:
                    result = Panel.PANEL.Top;
                    break;
                case (int)Panel.PANEL.Left:
                    result = Panel.PANEL.Left;
                    break;
                case (int)Panel.PANEL.Center:
                    result = Panel.PANEL.Center;
                    break;
                case (int)Panel.PANEL.Right:
                    result = Panel.PANEL.Right;
                    break;
                case (int)Panel.PANEL.Bottom:
                    result = Panel.PANEL.Bottom;
                    break;
            }
            return result;
        }
    }    
}