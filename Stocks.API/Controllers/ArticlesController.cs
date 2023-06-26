using Microsoft.AspNetCore.Mvc;
using Stocks.Hexagone.UseCases.Stocks.Commands;
using Stocks.Hexagone.UseCases.Stocks.Queries;

namespace Stocks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : ControllerBase
    {
        private readonly AddArticleCommandHandler _addArticleCommandHandler;
        private readonly GetAllArticlesQueryHandler _getAllArticlesQueryHandler;
        private readonly DeleteArticleCommandHandler _deleteArticleCommandHandler;
        private readonly GetArticleByReferenceQueryHandler _getByReferenceArticleQueryHandler;
        private readonly GetArticleByNameQueryHandler _getByNameArticleQueryHandler;
        private readonly GetArticleByIntervalQueryHandler _getByIntervalArticleQueryHandler;
        private readonly UpdateArticleCommandHandler  _updateArticleCommandHandler;

        public ArticlesController(
            AddArticleCommandHandler addArticleCommandHandler,
            GetAllArticlesQueryHandler getAllArticlesQueryHandler,
            DeleteArticleCommandHandler deleteArticleCommandHandler,
            GetArticleByReferenceQueryHandler getByReferenceArticleQueryHandler,
            GetArticleByNameQueryHandler getByNameArticleQueryHandler,
            GetArticleByIntervalQueryHandler getByIntervalArticleQueryHandler,
            UpdateArticleCommandHandler updateArticleCommandHandler)
        {
            _addArticleCommandHandler = addArticleCommandHandler;
            _getAllArticlesQueryHandler = getAllArticlesQueryHandler;
            _deleteArticleCommandHandler = deleteArticleCommandHandler;
            _getByReferenceArticleQueryHandler = getByReferenceArticleQueryHandler;
            _getByNameArticleQueryHandler = getByNameArticleQueryHandler;
            _getByIntervalArticleQueryHandler = getByIntervalArticleQueryHandler;
            _updateArticleCommandHandler = updateArticleCommandHandler;
        }

        [HttpGet]
        public IActionResult GetAllArticles()
        {
            var result = _getAllArticlesQueryHandler.Handle();
            return Ok(result.Data);
        }


        [HttpGet("reference")]
        public IActionResult GetArticleByReference([FromQuery] GetArticleByReferenceQuery query)
        {
            var result = _getByReferenceArticleQueryHandler.Handle(query);
            if (result.IsError())
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Data);
        }

        [HttpGet("interval")]
        public IActionResult GetArticleByInterval([FromQuery] GetArticleByIntervalQuery query)
        {
            var result = _getByIntervalArticleQueryHandler.Handle(query);
            return Ok(result.Data);
        }

        [HttpGet("name")]
        public IActionResult GetArticleByName([FromQuery] GetArticleByNameQuery query)
        {
            var result = _getByNameArticleQueryHandler.Handle(query);
            if (result.IsError())
            {
                return BadRequest(new { result.Message });
            }

            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult AddArticle(AddArticleCommand command)
        {
            var result = _addArticleCommandHandler.Handle(command);
            if(result.IsError())
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }


        [HttpPut]
        public IActionResult UpdateArticle(UpdateArticleCommand command)
        {
            var result = _updateArticleCommandHandler.Handle(command);
            if (result.IsError())
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }

        [HttpDelete]
        public IActionResult DeleteArticleByReference(DeleteArticleCommand command)
        {
            var result = _deleteArticleCommandHandler.Handle(command);
            if (result.IsError())
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new { result.Message });
        }
    }
}