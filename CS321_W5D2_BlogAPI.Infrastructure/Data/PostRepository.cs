using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _dbContext;

        public PostRepository(AppDbContext dbContext) 
        {  
            _dbContext = dbContext;
        }

        public Post Get(int id)
        {
            // TODO: Implement Get(id). Include related Blog and Blog.User
            return _dbContext.Posts
                .Include(p=>p.Blog)
                .ThenInclude(b=>b.User)
                .SingleOrDefault(p=>p.Id == id);
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            // TODO: Implement GetBlogPosts, return all posts for given blog id
            // TODO: Include related Blog and AppUser
            
            return _dbContext.Posts
                .Include(p => p.Blog)
                .ThenInclude(b => b.User)
                .Where(p => p.BlogId == blogId);
        }

        public Post Add(Post Post)
        {
            // TODO: add Post
            _dbContext.Posts.Add(Post);
            _dbContext.SaveChanges();
            return Post;
        }

        public Post Update(Post Post)
        {
            // TODO: update Post
            var currentPost = _dbContext.Posts.Find(Post.Id);

            if (currentPost == null) return null;

            _dbContext.Entry(currentPost)
                .CurrentValues
                .SetValues(Post);

            _dbContext.Posts.Update(currentPost);
            _dbContext.SaveChanges();

            return currentPost;
        }

        public IEnumerable<Post> GetAll()
        {
            // TODO: get all posts
            return _dbContext.Posts
                .Include(p => p.Blog);
                
        }

        public void Remove(int id)
        {
            // TODO: remove Post
            var currentPost = _dbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (currentPost != null)
            {
                _dbContext.Posts.Remove(currentPost);
                _dbContext.SaveChanges();
            }
        }

    }
}
