﻿using EasyNetApps.Core.Reflection.UserEntityInterface;
using Microsoft.EntityFrameworkCore;

namespace EasyNetApps.DbActions
{
    public static class GetQueries
    {
        public static IQueryable<T> ShallowSet<T>(this DbContext dbContext)
            where T : ProjectModel
        {
            var classOverview = typeof(T).GetClassOverview();
            IQueryable<T> query = dbContext.Set<T>();
            foreach (var property in classOverview.PropertiesOfUserClass)
            {
                query = query.Include(property.Name);
            }
            return query;
        }
        public static IQueryable<ProjectModel> ShallowSet(this DbContext dbContext, Type type)
        {
            var classOverview = type.GetClassOverview();
            IQueryable<ProjectModel> query = dbContext.Set(type);
            foreach (var property in classOverview.PropertiesOfUserClass)
            {
                query = query.Include(property.Name);
            }
            return query;
        }
        public static IQueryable<T> DeepSet<T>(this DbContext dbContext)
            where T : ProjectModel
        {
            var classOverview = typeof(T).GetClassOverview();

            IQueryable<T> query = dbContext.Set<T>();
            foreach (var property in classOverview.PropertiesOfUserClass)
            {
                query = query.Include(property.Name);
            }
            foreach (var collection in classOverview.SubClassCollectionProperties)
            {
                var properties = collection.GenericParameter.GetClassOverview().PropertiesOfUserClass;
                foreach (var property in properties)
                {
                    query = query.Include($"{collection.Property.Name}.{property.Name}");
                }
            }
            return query;
        }

        public static IQueryable<ProjectModel> Set(this DbContext dbContext, Type type)
        {
            var method = dbContext.GetType().GetMethods().Single(p =>
                            p.Name == nameof(DbContext.Set) && p.ContainsGenericParameters && !p.GetParameters().Any());

            method = method.MakeGenericMethod(type);

            var result = (IQueryable<ProjectModel>)method.Invoke(dbContext, null);
            return result;
        }

    }
}
