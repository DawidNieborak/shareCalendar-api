using MongoDB.Driver;
using shareCalendar_api.Entities;

namespace shareCalendar_api.Repositories.Implementation;

public class UserRepositoryImpl : IUserRepository
{
    private const string _databaseName = "calendar-api-db";
    private const string _collectionName = "UserCollection";
    private readonly IMongoCollection<User> _repositoryCollection;
    private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;
        
    public UserRepositoryImpl(IMongoClient mongoClient) 
    {
        IMongoDatabase database = mongoClient.GetDatabase(_databaseName);
        _repositoryCollection = database.GetCollection<User>(_collectionName);
    }
    
    public async Task<User> GetUserAsync(Guid id)
    {
        var filter = filterBuilder.Eq(item => item.Id, id);
        return await _repositoryCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        await _repositoryCollection.InsertOneAsync(user);
    }

    public async Task UpdateUserAsync(User user)
    {
        var existingUser = filterBuilder.Eq(item => item.Id, user.Id);
        await _repositoryCollection.ReplaceOneAsync(existingUser, user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var filter = filterBuilder.Eq(item => item.Id, id);
        await _repositoryCollection.DeleteOneAsync(filter);
    }
}