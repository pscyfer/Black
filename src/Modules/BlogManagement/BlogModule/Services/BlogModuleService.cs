using AutoMapper;
using BlogModule.Domain;
using BlogModule.Repository;
using BlogModule.Services.DTOs.Command.Blog;
using BlogModule.Services.DTOs.Command.BlogCategory;
using BlogModule.Services.DTOs.Command.Shared;
using BlogModule.Services.DTOs.Query.Blog;
using BlogModule.Services.DTOs.Query.BlogCategory;
using Common.Application;
using Microsoft.EntityFrameworkCore;

namespace BlogModule.Services
{
    public class BlogModuleService : IBlogModuleService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogRepository _BlogRepository;
        private readonly IMapper _mapper;
        public BlogModuleService(ICategoryRepository categoryRepository, IBlogRepository articleRepository)
        {
            _categoryRepository = categoryRepository;
            _BlogRepository = articleRepository;
        }

        public async Task<OperationResult> CreateBlogAsync(CreateBlogCommandDto command, CancellationToken cancellationToken)
        {
            try
            {
                var blog = _mapper.Map<Blog>(command);
                if (await _BlogRepository.IsExistAsync(x => x.Slug == blog.Slug, cancellationToken)) return OperationResult.Error("این ادرس موجود است.");
                await _BlogRepository.AddAsync(blog, cancellationToken);
                await _BlogRepository.SaveChangeAsync(cancellationToken);
                return OperationResult.Success();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OperationResult> CreateCategoryAsync(CreateBlogCategoryCommandDto command, CancellationToken cancellationToken)
        {
            try
            {
                var category = _mapper.Map<Category>(command);
                await _categoryRepository.AddAsync(category, cancellationToken);
                return OperationResult.Success();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<OperationResult> DeleteArticleAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var findCategory = await _BlogRepository.GetByIdAsync(id, cancellationToken);
                await _BlogRepository.DeleteAsync(findCategory, cancellationToken);
                await _BlogRepository.SaveChangeAsync(cancellationToken);
                return OperationResult.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResult> DeleteBlogByIdAsync(CommandGetByIdDto command, CancellationToken cancellationToken)
        {
            try
            {
                var findBlog = await _BlogRepository.GetFirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);
                if (findBlog == null) return OperationResult.Error("مقاله پیدا نشد.");

                await _BlogRepository.DeleteAsync(findBlog, cancellationToken);
                await _BlogRepository.SaveChangeAsync(cancellationToken);
                return OperationResult.Success();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OperationResult> DeleteCategoryAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var findCategory = await GetCategoryByIdAsync(Id, cancellationToken);
                if (findCategory != null) { return OperationResult.NotFound(); }
                await _categoryRepository.UpdateAsync(_mapper.Map<Category>(findCategory), cancellationToken);
                await _categoryRepository.SaveChangeAsync(cancellationToken);
                return OperationResult.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResult> EditBlogAsync(EditBlogCommandDto command, CancellationToken cancellationToken)
        {
            try
            {

                var blog = _mapper.Map<Blog>(command);
                if (await _BlogRepository.IsExistAsync(x => x.Id == command.Id, cancellationToken))
                {
                    await _BlogRepository.UpdateAsync(blog, cancellationToken);
                    await _BlogRepository.SaveChangeAsync(cancellationToken);
                    return OperationResult.Success();
                }
                return OperationResult.NotFound();
            }

            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OperationResult> EditCategoryAsync(CreateBlogCategoryCommandDto command, CancellationToken cancellationToken)
        {
            try
            {
                await _categoryRepository.UpdateAsync(_mapper.Map<Category>(command), cancellationToken);

                await _categoryRepository.SaveChangeAsync(cancellationToken);
                return OperationResult.Success();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResult<GetBlogQueryDto>> GetBlogByIdAsync(CommandGetByIdDto command, CancellationToken cancellationToken)
        {
            try
            {
                var operationResult = new OperationResult<GetBlogQueryDto>();
                var findBlog = await _BlogRepository.GetByIdAsync(command.Id, cancellationToken);
                operationResult.Status = OperationResultStatus.NotFound;
                if (findBlog != null) return operationResult;

                operationResult.Status = OperationResultStatus.Success;
                operationResult.Data = _mapper.Map<GetBlogQueryDto>(findBlog);

                return operationResult;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OperationResult<List<GetBlogQueryDto>>> GetBlogsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var operationResult = new OperationResult<List<GetBlogQueryDto>>();
                var getBlog = await _BlogRepository.GetAll(null, cancellationToken).ToListAsync(cancellationToken);

                operationResult.Status = OperationResultStatus.Success;
                operationResult.Data = _mapper.Map<List<GetBlogQueryDto>>(getBlog);
                return operationResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResult<List<GetBlogCategoryQueryDto>>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var operationResult = new OperationResult<List<GetBlogCategoryQueryDto>>();
                var getAllCategories = await _categoryRepository.GetAll(null, cancellationToken).ToListAsync(cancellationToken);
                operationResult.Data = _mapper.Map<List<GetBlogCategoryQueryDto>>(getAllCategories);
                operationResult.Status = OperationResultStatus.Success;
                return operationResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OperationResult<GetBlogCategoryQueryDto>> GetCategoryByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            try
            {
                var operationResult = new OperationResult<GetBlogCategoryQueryDto>();
                var getCategory = await _categoryRepository.GetFirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
                operationResult.Data = _mapper.Map<GetBlogCategoryQueryDto>(getCategory);
                operationResult.Status = OperationResultStatus.Success;
                return operationResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
