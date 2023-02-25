using MongoDB.Driver;
using shareCalendar_api.Entities;

namespace shareCalendar_api.Repositories.Implementation;

public class BlankCalendarRepositoryImpl : IBlankCalendarRepository
{
    private const string _databaseName = "calendar-api-db";
    private const string _collectionName = "BlankCalendarCollection";
    private readonly IMongoCollection<CalendarItemBlank> _repositoryCollection;
    private readonly FilterDefinitionBuilder<CalendarItemBlank> filterBuilder = Builders<CalendarItemBlank>.Filter;

    public BlankCalendarRepositoryImpl(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
        _repositoryCollection = database.GetCollection<CalendarItemBlank>(_collectionName);
    }
    
    public async Task<ICollection<CalendarItemBlank>> GetAllBlankCalendarItems(Guid id)
    {
        var filter = filterBuilder.Eq(item => item.userId, id);
        return await _repositoryCollection.Find(filter).ToListAsync();
    }

    public async Task PostBlankCalendarItem(CalendarItemBlank calendarItemBlank)
    {
        await _repositoryCollection.InsertOneAsync(calendarItemBlank);
    }
}