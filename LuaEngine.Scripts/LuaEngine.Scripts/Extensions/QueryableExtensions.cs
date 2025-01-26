using Monq.Core.Paging.Extensions;
using Monq.Core.Paging.Models;
using LuaEngine.Scripts.WebApi.Models;

namespace LuaEngine.Scripts.WebApi.Extensions;

/// <summary>
/// Методы расширения Queryable.
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Выполнить постраничную разбивку с сортировкой по умолчанию при отсутствии параметров сортировки.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="paging">Модель постраничной разбивки.</param>
    /// <returns>Записи после постраничной разбивки.</returns>
    public static IQueryable<ScriptVersion> GetPagingWithDefaultOrder(this IQueryable<ScriptVersion> query, PagingModel paging)
    {
        if (paging.SortCol is null)
        {
            paging.SortCol = nameof(ScriptVersion.ScriptId);
            paging.SortDir = "desc";
        }

        return query.WithPaging(paging, null, x => x.ScriptId);
    }
}
