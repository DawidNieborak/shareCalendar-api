using MongoDB.Bson;
using MongoDB.Driver;
using shareCalendar_api.Entities;

namespace shareCalendar_api.Repositories.Implementation;

public class CalendarRepositoryImpl : ICalendarRepository
{
    private const string _databaseName = "calendar-api-db";
    private const string _collectionName = "CalendarCollection";
    private readonly IMongoCollection<CalendarItem> _repositoryCollection;
    private readonly FilterDefinitionBuilder<CalendarItem> filterBuilder = Builders<CalendarItem>.Filter;
        
    public CalendarRepositoryImpl(IMongoClient mongoClient) 
    {
        IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
        _repositoryCollection = database.GetCollection<CalendarItem>(_collectionName);
    }

    public async Task<CalendarItem> GetCalendarAsync(Guid id)
    {
        var filter = filterBuilder.Eq(item => item.Id, id);
        return await _repositoryCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<CalendarItem>> GetAllCalendarsAsync(Guid id)
    {
        return await _repositoryCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task CreateCalendarAsync(CalendarItem calendar)
    {
        await _repositoryCollection.InsertOneAsync(calendar);
    }

    public async Task UpdateCalendarAsync(CalendarItem calendar)
    {
        var filter = filterBuilder.Eq(existingItem => existingItem.Id, calendar.Id);
        await _repositoryCollection.ReplaceOneAsync(filter, calendar);
    }

    public async Task DeleteCalendarAsync(Guid id)
    {
        var filter = filterBuilder.Eq(item => item.Id, id);
        await _repositoryCollection.DeleteOneAsync(filter);
    }
}