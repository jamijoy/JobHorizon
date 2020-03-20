using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JobHorizon.Interface;
using JobHorizon.Models;

namespace JobHorizon.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        JobHorizonContext context;
        public CommentRepository()
        {
            context = new JobHorizonContext();
        }
        public IEnumerable<Comment> DeleteCommentByForumId(int forum_id)
        {
            return null;
        }
    }
}