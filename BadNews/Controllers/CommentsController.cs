using System;
using System.Linq;
using BadNews.Models.Comments;
using BadNews.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers
{
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsRepository commentsRepository;

        public CommentsController(CommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        [HttpGet("api/news/{id}/comments")]
        public ActionResult<CommentsDto> GetCommentsForNews(Guid newsId)
        {
            var comments = commentsRepository.GetComments(newsId);
            var commentsDto = new CommentsDto()
            {
                NewsId = newsId,
                Comments = comments
                    .Select(c => new CommentDto
                    {
                        User = c.User, Value = c.Value
                    })
                    .ToArray()
            };
            return commentsDto;
        }
    }
}