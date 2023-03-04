using EasyEfDb.Tests.Test_Tools.ConfigDbs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EasyEfDb.Tests.Test_Tools;

public interface ILoadConfiguration
{
    public ConfigTestModel? TryLoadConfigurationModel(out string error);
}

public class LoadConfiguration : ILoadConfiguration
{
    private IEnvironmentLoader _environmentLoader = new EnvironmentLoader();
    // write constant for the variable key
    private const string _environmentKey = "ASPNETCORE_ENVIRONMENT_DB_TEST";
    
    
    public ConfigTestModel? TryLoadConfigurationModel(out string error)
    {
        //print init load
        Console.WriteLine("Init load");
        
        // load the environment variable
        var environment = _environmentLoader.GetEnvironmentVariable(_environmentKey);
        
        Console.WriteLine($"Environment variable: {environment}");
        
        // if the environment variable is null, set configModel to null and return error message
        if(environment == null)
        {
            
            Environment.SetEnvironmentVariable(_environmentKey, "InMemory");
            environment = _environmentLoader.GetEnvironmentVariable(_environmentKey);
            
            //error = $"Environment variable {_environmentKey} is null.";
            //return null;
        }
        
        // if the environment variable is not null
        // load the configuration model
        
        // get Path to the config{environment}.json file
        
        // get current path
        var currentPath = Directory.GetCurrentDirectory();
        
        // print current path
        Console.WriteLine($"Current path: {currentPath}");

        currentPath += "/../../../Test_Data/Configs/"; 
        
        //print current path
        Console.WriteLine($"Current path for Configs: {currentPath}");
        
        // get files in current path
        var files = Directory.GetFiles(currentPath);
        
        // print files
        foreach (var item in files)
        {
            Console.WriteLine($"File: {item}");
        }
        
        // get the file that matches the environment variable
        var file = files.FirstOrDefault(f => f.ToLower().Contains(environment.ToLower()));
        
        // if the file is null, set configModel to null and return error message
        if(file == null)
        {
            error = $"File {file} is null.";
            return null;
        }
        
        // read content of the file
        var content = File.ReadAllText(file);
        
        // deserialize the content into the configuration model
        var configModel = JsonConvert.DeserializeObject<ConfigTestModel>(content);


        // if the file is not null, load the configuration model using json
        //var configModel = new ConfigTestModel();
        /*var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(file, optional: true, reloadOnChange: true)
            .Build();*/
        
       
        error = $"no error";
        return configModel;
    }
}

public interface IEnvironmentLoader
{
    public string? GetEnvironmentVariable(string environmentKey);
}

public class EnvironmentLoader : IEnvironmentLoader
{
    public string? GetEnvironmentVariable(string environmentKey)
    {
        return Environment.GetEnvironmentVariable(environmentKey);
    }
}

