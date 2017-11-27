# SQLite Repository

Purpose of SQLite Repository is retrive some data from /wwwroot/App_Data/jsonrepo/database.sqlite

## Define Sevice

define in Startup.cs 

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ISQLiteRepository, SQLiteRepositoryImpl>();
    }

}
```

put service referance in model class

```csharp

public class Model {

    private ISQLiteRepository _repo;

    private IList<dynamic> _data;

    public Model{
        _repo=new SQLiteRepositoryImpl();
        _data=repo.getAllData();
    }

    public IList<dynamic> Data{
        get { return _data;}
    }
}

```

and use it in razor page

```csharp
@foreach(var item in Model.Data){
   <p>item.name</p>
}

```