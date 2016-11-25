using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
using System.Web;

namespace PSCPortal.CMS
{
    public class FolderClipNew : PSCPortal.Framework.BusinessObjectHierarchical<FolderClipNew>
    {
        #region Properties
        private Guid _id = new Guid();
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
        
        private DateTime _datetime;
        public DateTime Datetime
        {
            get
            {
                return _datetime;
            }
            set
            {
                _datetime = value;
            }
        }


        #endregion

        #region Constructions
        public FolderClipNew()
            : base()
        {
        }

        public FolderClipNew(DbDataReader reader)
            : base(reader)
        {
        }
        #endregion
        #region Abstract Methods
        protected override void MappingData(System.Data.Common.DbDataReader reader)
        {
            _id = (Guid)reader["FolderId"];
            _name = (string)reader["FolderName"];
        }
        protected override System.Data.Common.DbCommand GetInsertCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region AlbumId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@FolderId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region AlbumName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@FolderName", _name);
            command.Parameters.Add(prName);
            #endregion
           
            
            #region AlbumParentId
            DbParameter prParentId = database.GetParameter(System.Data.DbType.Guid, "@FolderParent", ((FolderClipNew)_parent).Id);
            command.Parameters.Add(prParentId);
            #endregion

            #region Datetime
            DbParameter prdate = database.GetParameter(System.Data.DbType.DateTime, "@FolderCreateDate", _datetime);
            command.Parameters.Add(prdate);
            #endregion

            #region Command Insert Data
            command.CommandText = @"INSERT INTO [dbo].[FolderClipNew]
                                    (
                                        FolderId,	
                                        FolderName,	                                        	
                                        FolderParent,
                                        FolderCreateDate 
                                    )
                                    VALUES
                                    (
                                        @FolderId,	
                                        @FolderName,	                                       
                                        @FolderParent,
                                        @FolderCreateDate                                         
                                    )";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetUpdateCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region AlbumId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@FolderId", _id);
            command.Parameters.Add(prId);
            #endregion
            #region AlbumName
            DbParameter prName = database.GetParameter(System.Data.DbType.String, "@FolderName", _name);
            command.Parameters.Add(prName);
            #endregion
            #region AlbumParentId
            DbParameter prParentId = database.GetParameter(System.Data.DbType.Guid, "@FolderParent", ((FolderClipNew)_parent).Id);
            command.Parameters.Add(prParentId);
            #endregion            
          
            #region Command Update Data
            command.CommandText = @"UPDATE [dbo].[FolderClipNew]
                                    SET
                                        FolderName=@FolderName,	
                                        FolderParent=@FolderParent	
                                    WHERE
                                        FolderId=@FolderId";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            return command;
        }

        protected override System.Data.Common.DbCommand GetDeleteCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region Command Delete Data
            #region AlbumId
            DbParameter prId = database.GetParameter(System.Data.DbType.Guid, "@FolderId", _id);
            command.Parameters.Add(prId);
            #endregion

            #region Command Delete Data
            command.CommandText = @"create table #temp(stt int identity(0,1),FolderId uniqueidentifier);
                                    insert into #temp (FolderId) values(@FolderId);
                                    declare @index int;
                                    set @index = 0;
                                    declare @max int;
                                    set @max = 0;
                                    declare @temp uniqueidentifier;
                                    while(@index<=@max)
                                    begin
                                    set @temp = (select FolderId from #temp where stt=@index);
                                    insert into #temp (FolderId)
                                    select FolderId
                                    from FolderClipNew
                                    where FolderParent=@temp;
                                    set @index = @index + 1;
                                    set @max = (select max(stt) from #temp);
                                    end
                                    delete FolderClipNew where FolderId in (select FolderId from #temp);";
            command.CommandType = System.Data.CommandType.Text;
            #endregion

            #endregion

            return command;
        }

      
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(FolderClipNew)
                && ((FolderClipNew)obj)._id == _id
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
