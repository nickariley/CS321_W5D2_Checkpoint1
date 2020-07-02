﻿using System;
using System.Collections.Generic;
using CS321_W5D2_BlogAPI.Core.Models;

namespace CS321_W5D2_BlogAPI.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IUserService _userService;

        public PostService(IPostRepository postRepository, IBlogRepository blogRepository, IUserService userService)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
            _userService = userService;
        }

        public Post Add(Post newPost)
        {
            // TODO: Prevent users from adding to a blog that isn't theirs
            //     Use the _userService to get the current users id.
            //     You may have to retrieve the blog in order to check user id
            // TODO: assign the current date to DatePublished

            var currentUserId = _userService.CurrentUserId;

            var currentBlog = _blogRepository.Get(newPost.BlogId);

            if(currentUserId != currentBlog.UserId)
            {
                throw new Exception("You cannot access a blog that does not belong to you");
            }

            newPost.DatePublished = DateTime.Now;


            return _postRepository.Add(newPost);
        }

        public Post Get(int id)
        {
            return _postRepository.Get(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll();
        }
        
        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            return _postRepository.GetBlogPosts(blogId);
        }

        public void Remove(int id)
        {
            var post = this.Get(id);
            // TODO: prevent user from deleting from a blog that isn't theirs
            var currentUserId = _userService.CurrentUserId;

            if (post.Blog.UserId != currentUserId)
            {
                throw new Exception("You cannot delete a blog that does not belong to you");
            }

            _postRepository.Remove(id);
        }

        public Post Update(Post updatedPost)
        {
            // TODO: prevent user from updating a blog that isn't theirs
            var currentUserId = _userService.CurrentUserId;

            if (currentUserId != updatedPost.Blog.UserId)
            {
                throw new Exception("You cannot update a blog that does not belong to you");
            }

            return _postRepository.Update(updatedPost);
        }

    }
}
