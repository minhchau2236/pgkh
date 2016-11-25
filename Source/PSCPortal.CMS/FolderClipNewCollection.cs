using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
using PSCPortal.CMS;
namespace PSCPortal.CMS
{
    [Serializable]
    public class FolderClipNewCollection : PSCPortal.Framework.BusinessObjectTree<FolderClipNewCollection, FolderClipNew>
    {
        protected FolderClipNewCollection(FolderClipNew root)
            : base(root)
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = @"create table #temp(stt int identity(0,1), FolderId uniqueidentifier, FolderName nvarchar(250), FolderParent uniqueidentifier, CreadDate datetime);
                                    insert into #temp(FolderId, FolderName, FolderParent,CreadDate)
                                    select FolderId, FolderName, FolderParent,FolderCreateDate
                                    from FolderClipNew
                                    where FolderParent is null  
                                    declare @index int;
                                    set @index = 0;
                                    declare @max int;
                                    set @max = (select max(stt) from #temp);
                                    declare @temp uniqueidentifier;
                                    while(@index<=@max)
                                    begin
                                    set @temp = (select FolderId from #temp where stt=@index);
                                    insert into #temp(FolderId, Foldername, FolderParent,CreadDate)
                                    select FolderId, FolderName, FolderParent,FolderCreateDate
                                    from FolderClipNew
                                    where FolderParent=@temp order by FolderCreateDate desc;
                                    set @index=@index +1;
                                    set @max=(select max(stt) from #temp);
                                    end
                                    select FolderId, Foldername, FolderParent
                                    from #temp
	                                where FolderParent is not null;";
            return command;
        }

        public static FolderClipNewCollection GetFolderClipNewCollection()
        {
            Database database = new Database("PSCPortalConnectionString");
            FolderClipNewCollection result = new FolderClipNewCollection(new FolderClipNew());
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid idParent = Guid.Empty;
                    if (reader["FolderParent"].ToString() != string.Empty)
                        idParent = (Guid)reader["FolderParent"];

                    FolderClipNew parent = (FolderClipNew)result.Search(d => ((FolderClipNew)d).Id == idParent);
                    if (parent == null)
                    {
                        result = new FolderClipNewCollection(new FolderClipNew() { Id = idParent });
                        parent = (FolderClipNew)result.Search(d => ((FolderClipNew)d).Id == idParent);
                    }
                    FolderClipNew dir = new FolderClipNew(reader);
                    result.Add(parent, dir);
                }
            }
            return result;
        }
        public static FolderClipNewCollection GetFolderClipNewCollection(Guid Id)
        {
            Database database = new Database("PSCPortalConnectionString");
            FolderClipNewCollection result = new FolderClipNewCollection(new FolderClipNew() { Id = Id });
            
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = database.GetCommand();
                command.CommandText = @"SET NOCOUNT ON
                                        SELECT
		                                        FolderId,	
                                                FolderName,	                                        	
                                                FolderParent,
                                                FolderCreatedDate
                                        FROM FolderClipNew
                                        WHERE
	                                        FolderParent=@FolderParent order by FolderCreatedDate desc";
                command.CommandType = System.Data.CommandType.Text;
                DbParameter parid = database.GetParameter(System.Data.DbType.Guid, "@FolderParent", Id);
                command.Parameters.Add(parid);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid idParent = Guid.Empty;
                    if (reader["FolderParent"].ToString() != string.Empty)
                        idParent = (Guid)reader["FolderParent"];

                    FolderClipNew parent = (FolderClipNew)result.Search(d => ((FolderClipNew)d).Id == idParent);

                    FolderClipNew dir = new FolderClipNew(reader);
                    //PhotoCollection Photolist = PhotoCollection.GetPhotoCollection(dir);
                    //dir.Name = dir.Name + "<span> - ["+ Photolist.Count+" ảnh] </span>";
                    result.Add(parent, dir);
                }
            }
            return result;
        }
        // get tree by topic parent
        protected DbCommand GetSelectAllCommandByParent(FolderClipNew topic)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            #region prFolderClipNew
            DbParameter prFolderClipNew = database.GetParameter(System.Data.DbType.Guid, "@Parent", topic.Id);
            command.Parameters.Add(prFolderClipNew);
            #endregion
            command.CommandText = "FolderClipNew_GetAllByParent";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            return command;
        }

        public static FolderClipNewCollection GetFolderClipNewCollectionByParent(FolderClipNew topic)
        {
            Database database = new Database("PSCPortalConnectionString");
            FolderClipNewCollection result = new FolderClipNewCollection(topic);
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommandByParent(topic);
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Guid idParent = Guid.Empty;
                    if (reader["FolderClipNewParent"].ToString() != string.Empty)
                        idParent = (Guid)reader["FolderClipNewParent"];
                    FolderClipNew parent = (FolderClipNew)result.Search(d => ((FolderClipNew)d).Id == idParent);
                    if (parent == null)
                        parent = new FolderClipNew { Id = Guid.Empty };
                    FolderClipNew dir = new FolderClipNew(reader);
                    result.Add(parent, dir);
                }
            }
            return result;
        }
        //
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }
}
