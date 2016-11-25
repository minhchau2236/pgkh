using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    public delegate void VoteAnswerDelegate(object sender, VoteAnswerArgs args);
    [Serializable]
    public class VoteAnswerArgs : EventArgs
    {
        private VoteAnswer _voteAnswer;
        public VoteAnswer VoteAnswer
        {
            get
            {
                return _voteAnswer;
            }
        }
        private bool _isEdit;
        public bool IsEdit
        {
            get
            {
                return _isEdit;
            }
        }
        public VoteAnswerArgs(VoteAnswer voteAnswer, bool isEdit)
        {
            _voteAnswer = voteAnswer;
            _isEdit = isEdit;
        }
    }
}