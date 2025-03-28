﻿using LuaEngine.Scripts.WebApi.Models.Filters;
using LuaEngine.Scripts.WebApi.Models;
using Monq.Core.Paging.Models;

namespace LuaEngine.Scripts.WebApi.Services.Abstracts;

/// <summary>
/// Интерфейс сервиса скриптов-префильтров.
/// </summary>
public interface IPrefilterScriptService
{
    /// <summary>
    /// Получить коллекцию скриптов-префильтров.
    /// </summary>
    /// <param name="pagingModel">Модель постраничной разбивки.</param>
    /// <param name="filter">Фильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Коллекция скриптов-префильтров.</returns>
    public Task<IEnumerable<PrefilterScript>> GetAllAsync(PagingModel pagingModel, PrefilterScriptFilter filter, CancellationToken token);

    /// <summary>
    /// Получить скрипт-префильтр по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-префильтр.</returns>
    public Task<PrefilterScript?> GetAsync(Guid id, CancellationToken token);

    /// <summary>
    /// Добавить скрипт-префильтр.
    /// </summary>
    /// <param name="prefilterScript">Скрипт-префильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-префильтр.</returns>
    public Task<PrefilterScript?> AddAsync(PrefilterScript prefilterScript, CancellationToken token);

    /// <summary>
    /// Обновить скрипт-префильтр.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="prefilterScript">Скрипт-префильтр.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns>Скрипт-префильтр.</returns>
    public Task<PrefilterScript?> UpdateAsync(Guid id, PrefilterScript prefilterScript, CancellationToken token);

    /// <summary>
    /// Удалить скрипт-префильтр по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор скрипта.</param>
    /// <param name="token">Токен отмены.</param>
    /// <returns><see langword="true"/>, если скрипт удалён успешно, иначе <see langword="false"/>.</returns>
    public Task<bool> DeleteAsync(Guid id, CancellationToken token);
}
