using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSCPortal.Framework;
using System.Data.Common;
namespace PSCPortal.CMS
{
    [Serializable]
    public class VoteAnswerCollection : PSCPortal.Framework.BusinessObjectCollection<VoteAnswerCollection, VoteAnswer>
    {
        private VoteQuestion _parent;
        public VoteQuestion Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
            }
        }

        private VoteAnswerCollection()
            : base()
        {
        }

        protected override DbCommand GetSelectAllCommand()
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            command.CommandText = "SELECT [VoteAnswerId],[VoteAnswerName],[VoteAnswerNumber],[VoteAnswerOrder] FROM dbo.VoteAnswer";
            return command;
        }

        public static VoteAnswerCollection GetVoteAnswerCollection()
        {
            Database database = new Database(ConnectionStringName);
            VoteAnswerCollection result = new VoteAnswerCollection();
            using (DbConnection connection = database.GetConnection())
            {
                DbCommand command = result.GetSelectAllCommand();
                command.Connection = connection;
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    VoteAnswer item = new VoteAnswer(reader);
                    result.Add(item);
                }
            }
            return result;
        }

        public static VoteAnswerCollection GetVoteAnswerCollectionByVoteQuestion(VoteQuestion voteQuestion)
        {
            Database database = new Database(ConnectionStringName);
            DbCommand command = database.GetCommand();
            VoteAnswerCollection result = new VoteAnswerCollection();
            result.Parent = voteQuestion;
            using (DbConnection connection = database.GetConnection())
            {
                command.Connection = connection;
                DbParameter prVoteAnswerId = database.GetParameter();
                prVoteAnswerId.DbType = System.Data.DbType.Guid;
                prVoteAnswerId.Direction = System.Data.ParameterDirection.InputOutput;
                prVoteAnswerId.ParameterName = "@VoteQuestionId";
                prVoteAnswerId.Value = voteQuestion.Id;
                command.Parameters.Add(prVoteAnswerId);
                command.CommandText = "SELECT [VoteAnswerId],[VoteAnswerName],[VoteAnswerNumber],[VoteAnswerOrder] FROM dbo.VoteAnswer WHERE VoteQuestionId=@VoteQuestionId order by VoteAnswerOrder";
                connection.Open();
                DbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    VoteAnswer item = new VoteAnswer(reader);
                    result.Add(item);
                }
            }
            return result;
        }
        public void MoveUp(VoteAnswer voteAnswer)
        {
            VoteAnswer AboveVoteAnswer = this.Where(item => item.Order == voteAnswer.Order - 1).Single();
            voteAnswer.Order -= 1;
            voteAnswer.Update();
            AboveVoteAnswer.Order += 1;
            AboveVoteAnswer.Update();
        }

        public void MoveDown(VoteAnswer voteAnswer)
        {
            VoteAnswer AboveVoteAnswer = this.Where(item => item.Order == voteAnswer.Order + 1).Single();
            voteAnswer.Order += 1;
            voteAnswer.Update();
            AboveVoteAnswer.Order -= 1;
            AboveVoteAnswer.Update();
        }
        protected static new string ConnectionStringName
        {
            get
            {
                return "PSCPortalConnectionString";
            }
        }
    }
}