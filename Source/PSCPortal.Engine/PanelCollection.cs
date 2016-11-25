using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.Engine
{
    [Serializable]
    public class PanelCollection : List<Panel>
    {
        public PanelCollection()
            : base()
        {
        }
        public Panel Search(Panel.PANEL pn)
        {
            return Find(p => p.Id == (int)pn);
        }
        public static PanelCollection GetPanelCollection()
        {
            PanelCollection result = new PanelCollection();
            Database database = new Database("PSCPortalConnectionString");
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand(connection);
                command.CommandText = "Panel_GetAll";
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();
                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new Panel(reader));
                    }
                }
            }
            return result;
        }

    }
}
