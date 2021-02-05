﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.CmsKit.Public.Blogs;

namespace Volo.CmsKit.Public.HttpApi.Volo.CmsKit.Public.Blogs
{
    [RemoteService(Name = CmsKitPublicRemoteServiceConsts.RemoteServiceName)]
    [Area("cms-kit")]
    [Route("api/cms-kit-public/blog-posts")]
    public class BlogPostPublicController : CmsKitPublicControllerBase, IBlogPostPublicAppService
    {
        protected IBlogPostPublicAppService BlogPostPublicAppService { get; }

        public BlogPostPublicController(IBlogPostPublicAppService blogPostPublicAppService)
        {
            BlogPostPublicAppService = blogPostPublicAppService;
        }

        [HttpGet]
        public Task<BlogPostPublicDto> GetAsync(string blogUrlSlug, string blogPostUrlSlug)
        {
            return BlogPostPublicAppService.GetAsync(blogUrlSlug, blogPostUrlSlug);
        }

        [HttpGet]
        [Route("{id}/cover-image")]
        public Task<RemoteStreamContent> GetCoverImageAsync(Guid id)
        {
            Response.Headers.Add("Content-Disposition", $"inline;filename=\"{id}\"");
            Response.Headers.Add("Accept-Ranges", "bytes");
            Response.Headers.Add("Cache-Control", "max-age=120");
            Response.ContentType = "image";
            return BlogPostPublicAppService.GetCoverImageAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<BlogPostPublicDto>> GetListAsync(string blogUrlSlug, PagedAndSortedResultRequestDto input)
        {
            return BlogPostPublicAppService.GetListAsync(blogUrlSlug, input);
        }
    }
}
