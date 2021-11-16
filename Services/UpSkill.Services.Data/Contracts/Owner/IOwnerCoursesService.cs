﻿namespace UpSkill.Services.Data.Contracts.Owner
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using UpSkill.Common;
    using UpSkill.Web.ViewModels.Course;
    using UpSkill.Web.ViewModels.Owner;

    public interface IOwnerCoursesService
    {
        Task RequestCourseAsync(RequestCourseViewModel model);

        Task<Result> EnableCourseAsync(int courseId, string userId);

        Task<Result> DisableCourseAsync(int courseId, string userId);

        Task<IEnumerable<TModel>> GetActiveCoursesAsync<TModel>(string id);

        Task<IEnumerable<TModel>> GetAvailableCoursesAsync<TModel>(string id);
    }
}