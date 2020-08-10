﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using project_manage_api.Model.QueryModel;
using project_manage_api.Model.RequestModel;
using project_manage_api.Service;

namespace project_manage_api.Controllers
{
    /// <summary>
    /// 用户任务controller
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private TaskService _service;
        public TaskController(TaskService taskService)
        {
            _service = taskService;
        }

        /// <summary>
        /// 获取我的任务列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public PageResponse<string> getTaskList(QueryProjectOrTaskRequest request)
        {
            var result = new PageResponse<string>();

            try
            {
                result = _service.getTaskList(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 开始任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response beginTask(int taskId)
        {
            var result = new Response();

            try
            {
                _service.beginTask(taskId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 结束任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response endTask(int taskId, int approverId)
        {
            var result = new Response();

            try
            {
                _service.endTask(taskId, approverId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 结束任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response cancelTask(int taskId)
        {
            var result = new Response();

            try
            {
                _service.cancelTask(taskId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 前端展示子任务任务树
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<Dictionary<string, object>> getChilrenTaskTree(int taskId)
        {
            var result = new Response<Dictionary<string, object>>();

            try
            {
                result.Result = _service.getChilrenTaskTree(taskId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 通过id获取task
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<Task> getTaskById(int taskId)
        {
            var result = new Response<Task>();

            try
            {
                result.Result = _service.getTaskById(taskId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 获取项目动态 提交情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<Dictionary<string, object>> getTaskRecordByTaskId(QueryTaskRecordRequest request)
        {
            var result = new Response<Dictionary<string, object>>();

            try
            {
                result.Result = _service.getTaskRecordByTaskId(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 通过id获取任务评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<List<CommentResponse>> getTaskCommentById(int taskId)
        {
            var result = new Response<List<CommentResponse>>();

            try
            {
                result.Result = _service.getTaskCommentById(taskId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 获取项目动态 提交情况
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public Response<CommentResponse> addTaskComment(AddTaskCommentRequest request)
        {
            var result = new Response<CommentResponse>();

            try
            {
                result.Result = _service.addTaskComment(request);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
        
        /// <summary>
        /// 获取最新的5条任务记录
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<List<TaskRecord>> getLatestTaskRecordByTaskId(int taskId)
        {
            var result = new Response<List<TaskRecord>>();

            try
            {
                result.Result = _service.getLatestTaskRecordByTaskId(taskId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 创建或更新任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        [HttpPost]
        public Response<Task> addOrUpdateTask(Task task)
        {
            var result = new Response<Task>();

            try
            {
                result.Result = _service.addOrUpdateTask(task);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}