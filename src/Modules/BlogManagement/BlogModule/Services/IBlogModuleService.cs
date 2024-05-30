using BlogModule.Services.DTOs.Command.Blog;
using BlogModule.Services.DTOs.Command.BlogCategory;
using BlogModule.Services.DTOs.Command.Shared;
using BlogModule.Services.DTOs.Query.Blog;
using BlogModule.Services.DTOs.Query.BlogCategory;
using Common.Application;

namespace BlogModule.Services
{
    public interface IBlogModuleService
    {
        #region Category
        Task<OperationResult<List<GetBlogCategoryQueryDto>>> GetCategoriesAsync(CancellationToken cancellationToken);
        Task<OperationResult<GetBlogCategoryQueryDto>> GetCategoryByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<OperationResult> CreateCategoryAsync(CreateBlogCategoryCommandDto command, CancellationToken cancellationToken);
        Task<OperationResult> EditCategoryAsync(CreateBlogCategoryCommandDto command, CancellationToken cancellationToken);
        Task<OperationResult> DeleteCategoryAsync(Guid Id, CancellationToken cancellationToken);
        #endregion

        #region Article
        Task<OperationResult<List<GetBlogQueryDto>>> GetBlogsAsync(CancellationToken cancellationToken);
        Task<OperationResult> CreateBlogAsync(CreateBlogCommandDto command, CancellationToken cancellationToken);
        Task<OperationResult> EditBlogAsync(EditBlogCommandDto command, CancellationToken cancellationToken);
        Task<OperationResult<GetBlogQueryDto>> GetBlogByIdAsync(CommandGetByIdDto command, CancellationToken cancellationToken);
        Task<OperationResult> DeleteBlogByIdAsync(CommandGetByIdDto command, CancellationToken cancellationToken);
        Task<OperationResult> DeleteArticleAsync(Guid id, CancellationToken cancellationToken);
        #endregion
    }
}
