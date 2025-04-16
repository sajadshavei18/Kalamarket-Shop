using System;
using System.Collections.Generic;
using System.Text;

namespace Kalamarket.Core.Viewmodel
{
    public class ShowCommentForProductViewmodel
    {
        public string username { get; set; }
        public DateTime CreateDate { get; set; }
        public byte Recomment { get; set; }

        public string commentTitle { get; set; }

        public string CommentDescription { get; set; }

        public int Commentlike { get; set; }
        public int commentDislike { get; set; }


    }
}
