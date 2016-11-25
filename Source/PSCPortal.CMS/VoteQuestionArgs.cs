using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using PSCPortal.Framework;
namespace PSCPortal.CMS
{
    public delegate void VoteQuestionDelegate(object sender, VoteQuestionArgs args);
    [Serializable]
    public class VoteQuestionArgs : EventArgs
    {
        private VoteQuestion _voteQuestion;
        public VoteQuestion VoteQuestion
        {
            get
            {
                return _voteQuestion;
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
        public VoteQuestionArgs(VoteQuestion voteQuestion, bool isEdit)
        {
            _voteQuestion = voteQuestion;
            _isEdit = isEdit;
        }
    }
}