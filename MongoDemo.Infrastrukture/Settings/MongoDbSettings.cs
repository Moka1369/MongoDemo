namespace MongoDemo.Infrastrukture.Settings;

public class MongoDbSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = "MongoDemoDb";
    public string CustomersCollectionName { get; set; } = "Customers";
}