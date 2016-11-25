using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;

namespace PSCPortal.CMS
{
     [Serializable]
    public class LinkWebsite : PSCPortal.Framework.BusinessObject<LinkWebsite>
    {
        #region Properties
        private Guid _id;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _name = value;
            }
        }

        private string _link = string.Empty;
        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                _link = value;
            }
        }

        private Guid _SubDomainId;
        public Guid SubDomainId
        {
            get
            {
                return _SubDomainId;
            }
            set
            {
                _SubDomainId = value;
            }
        }

        //private int _order;
        //public int Order
        //{
        //    get
        //    {
        //        return _order;
        //    }
        //    set {
        //        _order = value;
        //    }
        //}

        #endregion

        #region Constructions
        public LinkWebsite()
            : base()
        {
        }

        public LinkWebsite(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["DataId"];
            _name = (string)reader["Name"];
            _link = (string)reader["Link"];
            _SubDomainId = (Guid)reader["SubDomainId"];
            //_order = (int)reader["LinkOrder"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Id
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@DataId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region Name
            DbParameter prName = database.GetParameter();
            prName.DbType = System.Data.DbType.String;
            prName.Direction = System.Data.ParameterDirection.InputOutput;
            prName.ParameterName = "@Name";
            prName.Value = _name;
            command.Parameters.Add(prName);
            #endregion

            #region link
            DbParameter prLink = database.GetParameter(System.Data.DbType.String, "@Link", _link);
            command.Parameters.Add(prLink);
            #endregion
            
            #region SubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _SubDomainId);
            command.Parameters.Add(prSubDomainId);
            #endregion
            //#region order
            //DbParameter prOrder = database.GetParameter(System.Data.DbType.Int32, "@Order", _order);
            //command.Parameters.Add(prOrder);
            //#endregion


            #region Command Insert Data
            command.CommandText = "INSERT INTO [LinkWebsite] ([DataId],[Name],[Link],[SubDomainId]) VALUES (@DataId,@Name,@Link,@SubDomainId)";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Id
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@DataId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion
            #region Name
            DbParameter prName = database.GetParameter();
            prName.DbType = System.Data.DbType.String;
            prName.Direction = System.Data.ParameterDirection.InputOutput;
            prName.ParameterName = "@Name";
            prName.Value = _name;
            command.Parameters.Add(prName);
            #endregion

            #region link
            DbParameter prLink = database.GetParameter(System.Data.DbType.String, "@Link", _link);
            command.Parameters.Add(prLink);
            #endregion

            #region SubDomainId
            DbParameter prSubDomainId = database.GetParameter(System.Data.DbType.Guid, "@SubDomainId", _SubDomainId);
            command.Parameters.Add(prSubDomainId);
            #endregion
            //#region order
            //DbParameter prOrder = database.GetParameter(System.Data.DbType.Int32, "@Order", _order);
            //command.Parameters.Add(prOrder);
            //#endregion

            #region Command Update Data
            command.CommandText = "UPDATE [LinkWebsite] SET [Name] = @Name, [Link]=@Link, [SubDomainId]=@SubDomainId WHERE [DataId] = @DataId";
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region Id
            DbParameter prId = database.GetParameter();
            prId.DbType = System.Data.DbType.Guid;
            prId.Direction = System.Data.ParameterDirection.InputOutput;
            prId.ParameterName = "@DataId";
            prId.Value = _id;
            command.Parameters.Add(prId);
            #endregion

            command.CommandText = "DELETE [LinkWebsite] WHERE [DataId] = @DataId";
            #endregion

            return command;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(LinkWebsite)
                && ((LinkWebsite)obj)._id == _id
               )
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            int hashCode = 0;
            hashCode += _id.GetHashCode();
            return hashCode;
        }
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
        #endregion
    }
}
