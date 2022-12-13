﻿using Tasky.Dtos.Request;
using Tasky.Dtos.Response;
using Tasky.Models;

namespace Tasky.Interfaces.Services
{
    public interface ITaskListService
    {
        IEnumerable<TaskListResponseDto> GetAllLists();
        TaskListResponseDto GetTaskListById(int id);
        TaskListResponseDto CreateTaskList(TaskListRequestDto taskList);
        TaskListResponseDto UpdateTaskList(int id, TaskListRequestDto taskList);
        bool DeleteTaskList(int id);
    }
}