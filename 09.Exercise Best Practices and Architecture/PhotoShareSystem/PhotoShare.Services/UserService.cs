﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services
{
    public class UserService : IUserService
    {

        private readonly PhotoShareContext context;

        public UserService(PhotoShareContext context)
        {
            this.context = context;
        }

        public TModel ById<TModel>(int id)
            => this.By<TModel>(x => x.Id == id).SingleOrDefault();

        public TModel ByUsername<TModel>(string username)
            => By<TModel>(x => x.Username == username).SingleOrDefault();

        public bool Exists(int id) => this.ById<User>(id) != null;

        public bool Exists(string name) => this.ByUsername<User>(name) != null;

        public void ChangePassword(int userId, string password)
        {
            var user = this.context.Users.Find(userId);

            user.Password = password;

            this.context.SaveChanges();
        }

        public void Delete(string username)
        {
            var user = this.context.Users.SingleOrDefault(x => x.Username == username);

            user.IsDeleted = true;

            this.context.SaveChanges();
        }

        public User Register(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false
            };

            this.context.Add(user);

            this.context.SaveChanges();

            return user;
        }

        public void SetBornTown(int userId, int townId)
        {
            var user = this.context.Users.Find(userId);

            user.BornTownId = townId;

            this.context.SaveChanges();
        }

        public void SetCurrentTown(int userId, int townId)
        {
            var user = this.context.Users.Find(userId);

            user.CurrentTownId = townId;

            this.context.SaveChanges();
        }

        public Friendship AcceptFriend(int userId, int friendId)
        {
            var friendship = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };

            this.context.Friendships.Add(friendship);

            this.context.SaveChanges();

            return friendship;
        }

        public Friendship AddFriend(int userId, int friendId)
        {
            var friendship = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };

            this.context.Friendships.Add(friendship);

            this.context.SaveChanges();

            return friendship;
        }

        private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate)
            => this.context.Users.Where(predicate).AsQueryable().ProjectTo<TModel>();

      
        public User ByUsernameAndPassword(string username, string password)
        {
            var user = this.context.Users.Include(x => x.AlbumRoles).Where(u => u.Username == username && u.Password == password).SingleOrDefault();

            return user;
        }
    }
}